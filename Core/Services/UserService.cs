using Core.Convertors;
using Core.DTOs;
using Core.Generators;
using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Core.Security;
using Microsoft.EntityFrameworkCore;
using DataLayer.Entities.Wallets;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private YektayiSilverContext _contex;
        public UserService(YektayiSilverContext contex)
        {
            _contex = contex;
        }

        public User GetUserByUserName(string userName)
        {
            return _contex.Users.SingleOrDefault(u => u.UserName == userName);
        }

        public int AddUser(User user)
        {
            try
            {
                _contex.Users.Add(user);
                _contex.SaveChanges();

                return user.UserId;
            }
            catch
            {
                return 0;
            }
        }

        public int UpdateUser(User user)
        {
            try
            {
                _contex.Users.Update(user);
                _contex.SaveChanges();

                return user.UserId;
            }
            catch
            {
                return 0;
            }
        }

        public bool DeleteUser(int userId)
        {
            User user = GetUserById(userId);
            try
            {
                user.IsDelete = true;
                UpdateUser(user);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public int GetUserIdByUserName(string userName)
        {
            return _contex.Users
                .SingleOrDefault(u => u.UserName == userName)
                .UserId;
        }

        public User GetUserById(int userId)
        {
            return _contex.Users.Find(userId);
        }

        public User GetUserByEmail(string email)
        {
            return _contex.Users.SingleOrDefault(u => u.Email == email);
        }

        public User GetUserByActiveCode(string activeCode)
        {
            return _contex.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
        }

        public bool IsExistUserName(string userName)
        {
            return _contex.Users.Any(u => u.UserName == userName);
        }

        public bool IsExistEmail(string email)
        {
            return _contex.Users.Any(u => u.Email == email);
        }

        public List<SelectListItem> GetGender()
        {
            return _contex.UserGenders
                .Select(g => new SelectListItem()
                {
                    Text = g.GenderTitle,
                    Value = g.GenderId.ToString()
                })
                .ToList();
        }

        public string GetUserNameByUserId(int userId)
        {
            return _contex.Users.SingleOrDefault(u => u.UserId == userId).UserName;
        }


        public string SaveOrUpdateImg(IFormFile newImg, string oldImg = "Defult.png")
        {
            if (newImg != null)
            {
                if (newImg.IsImage())
                {
                    string imgPath = "";
                    string thumbPath = "";
                    if (oldImg != "Defult.png")
                    {
                        imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar/image", oldImg);
                        if (File.Exists(imgPath))
                            File.Delete(imgPath);
                        thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar/thumb", oldImg);
                        if (File.Exists(thumbPath))
                            File.Delete(thumbPath);

                    }

                    string newImgName = NameGenerator.GeneratorUniqCode() + Path.GetExtension(newImg.FileName);
                    imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar/image", newImgName);
                    using (var stream = new FileStream(imgPath, FileMode.Create))
                    {
                        newImg.CopyTo(stream);
                    }

                    #region RESIZE IMAGE

                    ImageConvertor imgResize = new ImageConvertor();
                    thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar/thumb", newImgName);

                    imgResize.Image_resize(imgPath, thumbPath, 170);

                    #endregion

                    return newImgName;
                }
                else
                {
                    return "Defult.png";
                }
            }
            else if (oldImg != "Defult.png")
            {
                return oldImg;
            }
            else
            {
                return "Defult.png";
            }
        }


        // // // // // // // // // // USER ACCOUNT


        public User LoginUser(LoginViewModel login)
        {
            string email = FixedText.FixedEmail(login.Email);
            string password = PasswordHelper.EncodePasswordMd5(login.Password);

            return _contex.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
        }

        public bool ActiveAccount(string activeCode)
        {
            User user = GetUserByActiveCode(activeCode);

            if (user == null || user.IsActive || user.IsDelete)
                return false;

            user.IsActive = true;
            user.ActiveCode = NameGenerator.GeneratorUniqCode();
            UpdateUser(user);

            return true;
        }



        // // // // // // // // // // USER ACCOUNT


        public InformationUserViewModel GetUserInformation(string userName)
        {
            return _contex.Users
                .Where(u => u.UserName == userName)
                .Include(u => u.UserGender)
                .Select(u => new InformationUserViewModel()
                {
                    UserName = u.UserName,
                    Email = u.Email,
                    FirstAndLastName = u.FirstName + u.LastName,
                    RegisterDate = u.RegisterDate,
                    PhonNumber = u.PhonNumber,
                    UesrGender = u.UserGender.GenderTitle,
                    Wallet = BalanceUserWallet(userName)
                })
                .Single();
        }

        public SideBarUserPanelViewModel GetSideBarUserPanelData(string userName)
        {
            User user = GetUserByUserName(userName);

            SideBarUserPanelViewModel sideBarUserPanel = new SideBarUserPanelViewModel()
            {
                UserName = user.UserName,
                RegisterDate = user.RegisterDate,
                UserAvatar = user.UserAvatar
            };

            return sideBarUserPanel;
        }

        public EditProfileViewModel GetDataForEditProfile(string userName)
        {
            User user = GetUserByUserName(userName);

            EditProfileViewModel editProfile = new EditProfileViewModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                GenderId = user.GenderId,
                PhonNumber = user.PhonNumber,
                UserAvatar = user.UserAvatar
            };

            return editProfile;
        }

        public int EditProfile(EditProfileViewModel editProfile, string userName)
        {
            User user = GetUserByUserName(userName);

            if (FixedText.FixedEmail(editProfile.Email) != user.Email)
                user.IsActive = false;

            user.Email = editProfile.Email;
            user.FirstName = editProfile.FirstName;
            user.LastName = editProfile.LastName;
            user.PhonNumber = editProfile.PhonNumber;
            user.UserAvatar = SaveOrUpdateImg(editProfile.UserImage, editProfile.UserAvatar);
            user.GenderId = (editProfile.GenderId == 0) ? null : editProfile.GenderId;

            return UpdateUser(user);
        }

        public bool CompareOldPassword(string oldPassword, string userName)
        {
            string hashOldPas = PasswordHelper.EncodePasswordMd5(oldPassword);
            return _contex.Users
                .Any(u => u.Password == hashOldPas && u.UserName == userName);
        }

        public bool ChengPassword(string newPassword, string userName)
        {
            User user = GetUserByUserName(userName);
            string hashNewPas = PasswordHelper.EncodePasswordMd5(newPassword);

            user.Password = hashNewPas;

            UpdateUser(user);
            return true;
        }



        // // // // // // // // // // WALLET



        public int AddWallet(Wallet wallet)
        {
            _contex.Wallets.Add(wallet);
            _contex.SaveChanges();

            return wallet.WalletId;
        }

        public int UpdateWallet(Wallet wallet)
        {
            _contex.Wallets.Update(wallet);
            _contex.SaveChanges();

            return wallet.WalletId;
        }


        public Wallet GetWalletByWalletId(int walletId)
        {
            return _contex.Wallets.Find(walletId);
        }

        public List<WalletViewModel> GetUserWallet(string userName)
        {
            int userId = GetUserIdByUserName(userName);

            return _contex.Wallets
                .Where(w => w.UserId == userId)
                .Include(w => w.WalletType)
                .Select(w => new WalletViewModel()
                {
                    Type = w.TypeId,
                    Amount = w.Amount,
                    Description = w.Description,
                    DateTime = w.CreateDate,
                    IsPay = w.IsPay
                }).ToList();
        }

        public int BalanceUserWallet(string userName)
        {
            int userId = GetUserIdByUserName(userName);

            var enter = _contex.Wallets
                .Where(w => w.TypeId == 1 && w.UserId == userId && w.IsPay)
                .Select(w => w.Amount)
                .ToList();

            var exit = _contex.Wallets
                .Where(w => w.TypeId == 2 && w.UserId == userId && w.IsPay)
                .Select(w => w.Amount)
                .ToList();

            return (enter.Sum() - exit.Sum());
        }

        public int ChargeWallet(string userName, int amount, string description, bool isPay = false)
        {
            Wallet wallet = new Wallet()
            {
                UserId = GetUserIdByUserName(userName),
                Amount = amount,
                Description = description,
                CreateDate = DateTime.Now,
                IsPay = isPay,
                TypeId = 1
            };

            return AddWallet(wallet);
        }



        // // // // // // // // // // ADMIN PANEL


        public UserForAdminViewModel GetUserForAdmin(int pageId = 1, string filterUserName = "",
            string filterEmail = "", int genderId = 0, int take = 10)
        {
            IQueryable<User> result = _contex.Users.Include(u => u.UserGender);

            if (!string.IsNullOrEmpty(filterUserName))
            {
                result = result.Where(u => u.UserName == filterUserName);
            }
            if (!string.IsNullOrEmpty(filterEmail))
            {
                result = result.Where(u => u.Email == filterEmail);
            }
            if (genderId != 0)
            {
                result = result.Where(u => u.GenderId == genderId);
            }

            //SHOW USER IN PAGE
            int skip = (pageId - 1) * take;

            UserForAdminViewModel userList = new UserForAdminViewModel()
            {
                Users = result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList(),
                CurrentPage = pageId,
                PageCount = result.Count() / take
            };


            return userList;
        }

        public int AddUserFromAdminPanel(CreateUserForAdminViewModel createUser)
        {
            User user = new User();

            user.UserName = createUser.UserName;
            user.Email = FixedText.FixedEmail(createUser.Email);
            user.Password = PasswordHelper.EncodePasswordMd5(createUser.Password);
            user.FirstName = createUser.FirstName;
            user.LastName = createUser.LastName;
            user.PhonNumber = createUser.PhonNumber;
            user.IsActive = (createUser.IsActive == 1) ? true : false;
            user.ActiveCode = NameGenerator.GeneratorUniqCode();
            user.GenderId = (user.GenderId != 0) ? user.GenderId : null;
            user.UserAvatar = SaveOrUpdateImg(createUser.UserAvatar);
            user.RegisterDate = DateTime.Now;


            AddUser(user);

            if (createUser.AddWallet != 0)
            {
                ChargeWallet(user.UserName, createUser.AddWallet.Value, "شارژ کیف پول توسط ادمین", true);
            }

            return user.UserId;
        }

        public EditUserForAdminViewModel GetUserForEditInAdmin(int userId)
        {
            return _contex.Users
                .Where(u => u.UserId == userId)
                .Include(u=>u.UserRoles)
                .Select(u => new EditUserForAdminViewModel()
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    PhonNumber = u.PhonNumber,
                    GenderId = u.GenderId,
                    IsActive = (u.IsActive == true) ? 1 : 2,
                    OldUserAvatarName = u.UserAvatar,
                    RegisterDate = u.RegisterDate,
                    UserRoles=u.UserRoles.Select(u=>u.RoleId).ToList()
                }).Single();
        }

        public int EditUserFromAdminPanel(EditUserForAdminViewModel editUser)
        {
            User user = GetUserById(editUser.UserId);

            user.UserName = editUser.UserName;
            user.Email = FixedText.FixedEmail(editUser.Email);
            user.FirstName = editUser.FirstName;
            user.LastName = editUser.LastName;
            user.PhonNumber = editUser.PhonNumber;
            user.IsActive = (editUser.IsActive == 1) ? true : false;
            user.GenderId = (editUser.GenderId != 0) ? editUser.GenderId : null;
            user.UserAvatar = SaveOrUpdateImg(editUser.UserAvatar, editUser.OldUserAvatarName);
            if (!string.IsNullOrEmpty(editUser.Password))
                user.Password = PasswordHelper.EncodePasswordMd5(editUser.Password);


            UpdateUser(user);

            if (editUser.AddWallet != 0)
            {
                ChargeWallet(user.UserName, editUser.AddWallet.Value, "شارژ کیف پول توسط ادمین", true);
            }

            return user.UserId;
        }

        public DeleteUserForAdminViewModel GetUserForDeleteFromAdmin(int userId)
        {
            return _contex.Users
                .Where(u => u.UserId == userId)
                .Include(u => u.UserGender)
                .Select(u => new DeleteUserForAdminViewModel()
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    PhonNumber = u.PhonNumber,
                    Gender = (u.GenderId != null) ? u.UserGender.GenderTitle : null,
                    RegisterDate = u.RegisterDate,
                    UserAvatar = u.UserAvatar
                }).Single();
        }
    }
}
