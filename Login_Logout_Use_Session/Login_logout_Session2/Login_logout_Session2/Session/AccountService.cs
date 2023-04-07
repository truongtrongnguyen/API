using Login_Logout_Session.Model;

namespace Login_Logout_Session.Session
{
    public class AccountService : IAccountServices
    {
        private List<Account> _accounts;
        public AccountService()
        {
            _accounts = new List<Account>()
            {
                new Account (){UserName = "m123", Password = "123"},
                new Account (){UserName = "m456", Password = "456"},

            };
        }
        public Account LogIn(string username, string password)
        {
            return _accounts.SingleOrDefault(x => x.UserName == username && x.Password == password);
        }
    }
}
