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
    public class PostRepository : IPostRepository
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        private readonly IUserService _userService;
        public PostRepository(IUserService userService,IMapper mapper,IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
            _userService = userService;
        }
        public Task<int> Post(string Content)
        {
            var add = _context.Posts.Add(new Post() { PostContent = Content });
           _context.Complete();
            if (add.Entity.Id < 0) { 
             GuardException.CanNotCreate(true,nameof(Post));
            }
            
            return Task.FromResult(add.Entity.Id);
        }

        public Task<bool> EditPost(PostViewModel model)
        {
            var post = checkOwnerAndAvailability(model.Id);
            if(post is null)
                return Task.FromResult(false);

            _context.Posts.Update(_mapper.Map<PostViewModel,Post>(model));

            _context.Complete();

            return Task.FromResult(true);
        }

        public Task<bool> DeletePost(int postId)
        {
            var post = checkOwnerAndAvailability(postId);
            if(post is null)
                return Task<bool>.FromResult(false);
            _context.Posts.Remove(post);

            _context.Complete();

            return Task<bool>.FromResult(true);
        }
        private Post checkOwnerAndAvailability(int id) { 
            var post = _context.Posts.FirstOrDefault(e => e.UserId == _userService.GetUserId()&&e.Id==id);

            return post;
        }

        public Task<bool> UpdatePost(PostViewModel model)
        {
            var post = _context.Posts.FirstOrDefault(e => e.Id == model.Id);
            if (post is null ||post.UserId != _userService.GetUserId())
                return Task.FromResult(false);
            post.PostContent = model.PostContent;

            _context.Posts.Update(post);

            _context.Complete();

            return Task.FromResult(true);
        
        }

        public Task<IEnumerable<GetPostViewModel>> GetPosts(int page=0)
        {
            var posts = _context.Posts.Skip(page).Take(5)
                .Include(e=>e.User).AsEnumerable();

            var result = _mapper.Map<IEnumerable<Post>, IEnumerable<GetPostViewModel>>(posts);

            return Task.FromResult(result);
        }

        public Task<IEnumerable<GetPostViewModel>> GetMyPosts(int page = 0)
        {
            var posts = _context.Posts.Where(e => e.UserId == _userService.GetUserId()).Take(5).AsEnumerable();
            var result = _mapper.Map<IEnumerable<Post>, IEnumerable<GetPostViewModel>>(posts);

            return Task.FromResult(result);

        }
    }
}
