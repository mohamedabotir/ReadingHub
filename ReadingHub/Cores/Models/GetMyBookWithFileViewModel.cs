namespace ReadingHub.Cores.Models
{
    public class GetMyBookWithFileViewModel
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public string BookName { get; set; }
        public string BookMimeType { get; set; }
        public string PhotoUrl { get; set; }
        public int PageNumbers { get; set; }
    }
}
