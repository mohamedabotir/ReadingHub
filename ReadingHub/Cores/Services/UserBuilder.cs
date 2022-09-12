using ReadingHub.Persistence.Models;

namespace ReadingHub.Cores.Services
{
    public class UserBuilder
    {
        protected User _user;
        public UserBuilder()
        {
            _user = new User();
        }

        public UserBuilder(User user) {
            _user = user;
        }

        public UserBuilder Myname(string name) { 
        _user.UserName = name;
            return this;
        }

        public UserBuilder Email(string email) { 
        _user.Email = email;
            return this;
        }
        public UserBuilder Address(string address) { 
        _user.Address = address;
            return this;
        }

        public UserBuilder PhoneNumber(string PhoneNumber) { 
        _user.PhoneNumber = PhoneNumber;
            return this;
        }
        public User Build() {
            return _user;
        }


    }
}
