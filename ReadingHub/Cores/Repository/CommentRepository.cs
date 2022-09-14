using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReadingHub.Cores.Models;
using ReadingHub.Cores.Validations.Exceptions;
using ReadingHub.Persistence.Abstract;
using ReadingHub.Persistence.Models;
using ReadingHub.Unit.Abstracts;
using ReadingHub.Unit.Abstracts.Repository;

namespace ReadingHub.Cores.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ISharedService _sharedService;
 
        public CommentRepository(ISharedService service,IApplicationDbContext context, IMapper mapper,IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
            _sharedService = service;
        }
        //Book Comment
        public Task<int> Comment(CommentViewModel comment)
        {
            var addResult = _context.Comments.Add(_mapper.Map<CommentViewModel,Comment>(comment));
            _context.Complete();
            if(addResult.Entity.Id<0)
                Task.FromResult(0);

            return  Task.FromResult(addResult.Entity.Id);
        
        }

        public Task<bool> DeleteComment(int commentId)
        {
            var comment = _context.Comments.FirstOrDefault(c => c.Id == commentId && c.UserId == _userService.GetUserId());

            GuardException.NotFound(comment,nameof(comment));

            _context.Comments.Remove(comment);
            _context.Complete();
            return Task.FromResult(true);
        }

        public Task<ProfileViewModel> GetUserInformation(string id)
        {
           var result =_mapper.Map<User,ProfileViewModel>(_context.Users.First(e => e.Id == id));

            result.PictureUrl= _sharedService.GetAuthorPicture(id);

        return Task.FromResult(result);
        }

        public Task GetBookComments(int bookId) {
            var comments = _context.BookComments.Where(e => e.BookId == bookId).Include(e => e.Comment).ThenInclude(e=>e.User).AsEnumerable();
            var result= _mapper.Map<IEnumerable<Comment>, IEnumerable<BookCommentViewModel>>(comments.Select(e=>e.Comment));
         
            return Task.FromResult(result);
        
        }
    }
}
