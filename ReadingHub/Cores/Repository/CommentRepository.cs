using AutoMapper;
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
 
        public CommentRepository(IApplicationDbContext context, IMapper mapper,IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }
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
    }
}
