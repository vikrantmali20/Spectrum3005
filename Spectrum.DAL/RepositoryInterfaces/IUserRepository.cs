using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spectrum.DAL.RepositoryInterfaces
{
    public interface IUserRepository:IGenericRepository<AuthUsers>
    {
         AuthUsers IsUserExist(string UserId, string SiteCode);
         AuthUsers IsIdCardExist(string IdCard);
         IList<AuthUsers> GetUserListEdit();
         AuthUsers GetUserById(string userId);
         MstSalesPerson GetSalesPersonById(string userId);
         AuthUserSiteRoleMap GetRolesDataById(string userId);
         bool UpdateUser(AuthUsers user, AuthUserSiteRoleMap userRole, MstSalesPerson salesPerson, bool flagRoleAddEdit, bool flagSalesAddEdit);
         bool AddUser(AuthUsers user, AuthUserSiteRoleMap userRole, MstSalesPerson salesPerson);
         AuthUsers IsIdCardExistForOtherUser(string IdCard, string UserId);
    }
}
