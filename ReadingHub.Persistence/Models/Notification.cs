using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingHub.Persistence.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public User Sender { get; set; }
        public string SenderId { get; set; }
        public User Receiver { get; set; }
        public string ReceiverId { get; set; }

        public NotificationType Type { get; set; }
    }
    public enum NotificationType { 
    Comment=1,Message
    };
}
