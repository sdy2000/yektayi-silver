using Core.DTOs;
using DataLayer.Entities.Users;
using DataLayer.Entities.Wallets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.Services.Interfaces
{
    public interface IUserService
    {
        int AddUser(User user);
        int UpdateUser(User user);
        bool DeleteUser(int userId);
        int GetUserIdByUserName(string userName);
        User GetUserById(int userId);
        User GetUserByUserName(string userName);
        User GetUserByEmail(string email);
        User GetUserByActiveCode(string activeCode);
        bool IsExistUserName(string userName);
        bool IsExistEmail(string email);
        List<SelectListItem> GetGender();
        string GetUserNameByUserId(int userId);
        string SaveOrUpdateImg(IFormFile newImg, string oldImg = "Defult.png");

        #region USER ACCOUNT 

        User LoginUser(LoginViewModel login);
        bool ActiveAccount(string activeCode);


        #endregion

        #region USER PANEL

        InformationUserViewModel GetUserInformation(string userName);   
        SideBarUserPanelViewModel GetSideBarUserPanelData(string userName);
        EditProfileViewModel GetDataForEditProfile(string userName);
        int EditProfile(EditProfileViewModel editProfile,string userName);
        bool CompareOldPassword(string oldPassword,string userName);
        bool ChengPassword(string newPassword, string userName);

        #endregion

        #region WALLET

        int AddWallet(Wallet wallet);
        int UpdateWallet(Wallet wallet);
        Wallet GetWalletByWalletId(int walletId);
        List<WalletViewModel> GetUserWallet(string userName);
        int BalanceUserWallet(string userName);
        int ChargeWallet(string userName, int amount, string description, bool isPay = false);

        #endregion

        #region ADMIN PANEL

        UserForAdminViewModel GetUserForAdmin(int pageId = 1, string filterUserName = "",
            string filterEmail = "", int genderId = 0, int take = 10);
        int AddUserFromAdminPanel(CreateUserForAdminViewModel createUser);
        EditUserForAdminViewModel GetUserForEditInAdmin(int userId);
        int EditUserFromAdminPanel(EditUserForAdminViewModel editUser);
        DeleteUserForAdminViewModel GetUserForDeleteFromAdmin(int userId);

        #endregion
    }
}
