namespace ReadingHub.Cores.Models
{
    public class GetBooksViewModel
    {
         
        public int? Id { get; set; }
        public string AuthorId { get; set; }

        public string Description { get; set; }

        public string BookName { get; set; }
        public string BookMimeType { get; set; }
    }
}
