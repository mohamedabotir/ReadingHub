using ReadingHub.Persistence.Models;

namespace ReadingHub.Cores.Models
{
    public class CommunicationViewModel
    {
        public string SenderId  { get; set; }

        public string ReceiverId { get; set; }

        public NotificationType Type { get; set; }
    }
}
