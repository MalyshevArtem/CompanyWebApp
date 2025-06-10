using DataAccess.Models;
using CompanyWebAppUI.Models;

namespace CompanyWebAppUI.ViewModels
{
    public class UserViewModel
    {
        private readonly User _user;

        public UserViewModel(User user)
        {
            _user = user;
        }

        public UserModel GetUserModel()
        {
            return new UserModel
            {
                Id = _user.Id,
                WorkerId = _user.WorkerId,
                Temp = _user.Temp
            };
        }
    }
}
