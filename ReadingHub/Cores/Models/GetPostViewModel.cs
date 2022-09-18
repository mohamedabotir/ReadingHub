namespace ReadingHub.Cores.Models
{
    public class GetPostViewModel
    {
        public int Id { get; set; }
        public string PostContent { get; set; }
        public string UserName { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime PostTime { get; set; }
    }
}
