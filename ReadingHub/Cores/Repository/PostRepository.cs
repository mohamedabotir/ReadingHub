using AutoMapper;
using ReadingHub.Cores.Validations.Exceptions;
using ReadingHub.Persistence.Abstract;
using ReadingHub.Persistence.Models;
using ReadingHub.Unit.Abstracts.Repository;

namespace ReadingHub.Cores.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        public PostRepository(IMapper mapper,IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
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
    }
}
