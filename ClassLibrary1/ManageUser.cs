using System.Threading.Tasks;
using ClassLibrary2;

namespace ClassLibrary1
{
    public class ManageUser
    {
        public async Task<User> GetUser(string id)
        {
            return new User();
        }

        public async Task<User> GetUserByMail(string mail)
        {
            return new User();
        }
    }
}