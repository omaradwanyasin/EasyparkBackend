using Easypark_Backend.Business.Dtos;
using Easypark_Backend.Data.Repository;

namespace Easypark_Backend.Business.Manager.Implementaion
{
    public class UserManager
    {
        public bool IsUser() //example not real methoed;
        {
            var userloger = new UserLoggerRepo();
            if (userloger.SignInUser())
            {
                return true;
            }
            else
                Console.WriteLine("there is erorr");
            return false;
        }
        public UserDto getusername()
        {
            UserDto user = new UserDto();   
            user.name = "methed ";
            return user;
        }
    }
}
