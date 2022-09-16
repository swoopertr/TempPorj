using DataLayer.Db;
using Repository.Repository;


namespace DataLayer.Managers
{
    public class UserManager : MsSql<User>
    {
        public UserManager() :base()
        {

        }
        public User GetUserByEmailAndPassword(string email, string password)
        {
            return ItemQuery($"select * from Users where Email='{email}' and Password='{password}'");
        }
    }
}
