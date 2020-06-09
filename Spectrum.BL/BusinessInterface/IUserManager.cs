using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spectrum.Models;
namespace Spectrum.BL.BusinessInterface
{
    public interface IUserManager:IGenericManager<UserModel>
    {

        AuthUserModel IsUserExist(string UserId, string SiteCode);
        AuthUserModel IsIdCardExist(string IdCard);
        AuthUserModel IsIdCardExistForOtherUser(string IdCard, string UserId);
        IList<AuthUserModelEdit> GetUserListEdit();
        AuthUserModel GetUserById(string userId);
        MstSalesPersonModel GetSalesPersonById(string userId);
        AuthUserSiteRoleMapModel GetRolesDataById(string userId);
        bool UpdateUser(UserModel user, bool flagRoleAddEdit, bool flagSalesAddEdit);
        bool AddUser(UserModel user);
    }
}
