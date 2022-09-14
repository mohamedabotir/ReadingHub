
namespace ReadingHub.Cores.Models
{
    public class EditProfileViewModel
    {
        public string UserName
        {
            get;
            set;
        }

        public string Address { get; set; }

        public string PhoneNumber
        {
            get;
            set;
        }
        
        public IFormFile? Photo { get; set; }

    }
}
