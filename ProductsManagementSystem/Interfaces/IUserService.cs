using ProductsManagementSystem.Models.VM.User;

namespace ProductsManagementSystem.Interfaces
{
    public interface IUserService
    {
        public Task UpdatePersonalInfo(UserInfoUpdateViewModel userInfoUpdateViewModel);
        public Task ChangeUserName(UserUserNameChangeViewModel userUserNameChangeViewModel);
        public Task PasswordChange(UserPasswordChangeViewModel userPasswordChangeViewModel);
        public Task DeleteUser(UserDeleteViewModel userDeleteViewModel);
    }
}