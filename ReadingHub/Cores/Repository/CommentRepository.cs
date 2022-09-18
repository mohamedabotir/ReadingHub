using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReadingHub.Cores.Models;
using ReadingHub.Cores.Validations.Exceptions;
using ReadingHub.Persistence.Abstract;
using ReadingHub.Persistence.Models;
using ReadingHub.Unit.Abstracts;
using ReadingHub.Unit.Abstracts.Repository;
using System.Collections;

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
            var checkBook = _context.Books.FirstOrDefault(e=>e.Id == comment.BookId);
            if (checkBook is null)
                return Task.FromResult(-1);
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

        public IEnumerable<BookAndPostCommentViewModel> GetBookComments(int bookId) {
            var check = _context.Posts.FirstOrDefault(e => e.Id == bookId);

            if (check is null)
                return Enumerable.Empty<BookAndPostCommentViewModel>();
            var comments = _context.BookComments.Where(e => e.BookId == bookId).Include(e => e.Comment).ThenInclude(e=>e.User).AsEnumerable();
            var result= _mapper.Map<IEnumerable<Comment>, IEnumerable<BookAndPostCommentViewModel>>(comments.Select(e=>e.Comment));
         
            return result;
        
        }

        public Task<int> PostComment(CommentViewModel model)
        {
            var checkPost = _context.Posts.FirstOrDefault(e=>e.Id == model.BookId);
            if (checkPost is null)
                return Task.FromResult(-1);
            model.CommentType = CommentType.PostComment;
            var result = _context.Comments.Add(_mapper.Map<CommentViewModel,Comment>(model));

            _context.Complete();

            return Task.FromResult(result.Entity.Id);
        }


        public IEnumerable<BookAndPostCommentViewModel> GetPostComments(int postId)
        {
            var check = _context.Posts.FirstOrDefault(e=>e.Id == postId);
            if (check is null) 
              return Enumerable.Empty<BookAndPostCommentViewModel>();

            var comments = _context.PostComments.Where(e => e.Id == postId).Include(e => e.Comment).ThenInclude(e => e.User).AsEnumerable();
            var result = _mapper.Map<IEnumerable<Comment>, IEnumerable<BookAndPostCommentViewModel>>(comments.Select(e => e.Comment));

            return result;

        }

        public Task<bool> DeletePostComment(int postId)
        {
            var post = _context.Comments.FirstOrDefault(e=>e.Id==postId);
           
            if (post is null ||post.UserId != _userService.GetUserId())
            {
               
                return Task.FromResult(false);
            }
            _context.Comments.Remove(post);
            _context.Complete();

            return Task.FromResult(true);
        }
    }
}
