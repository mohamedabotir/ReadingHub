namespace ReadingHub.Unit.Abstracts.Repository
{
    public interface IPostRepository
    {
        Task<int> Post(string Content);
    }
}
