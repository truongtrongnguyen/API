using Login_Logout_Session.Model;

namespace Login_Logout_Session.Session
{
    public interface IAccountServices
    {
        public Account LogIn(string username, string password);
    }
}
