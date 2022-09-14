namespace ReadingHub.Cores.Models
{
    public class UserViewModel
    {
        public   string UserName
        {
            get;
            set;
        }

        public string Address { get; set; }



        public   string Email
        {
            get;
            set;
        }


        public   string Password
        {
            get;
            set;
        }











        public   string PhoneNumber
        {
            get;
            set;
        }




        public IFormFile Photo { get; set; }
        public string PhotoUrl { get; set; }


    }
}
