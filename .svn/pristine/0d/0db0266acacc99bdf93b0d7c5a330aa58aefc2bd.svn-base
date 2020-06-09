using System.Linq;
using Spectrum.BL.Mappers;
using Spectrum.BL.BusinessInterface;
using Spectrum.DAL;
using Spectrum.DAL.RepositoryInterfaces;
using Spectrum.Models;
using AutoMapper;
using System.Collections.Generic;
using Spectrum.DAL.CustomEntities;

namespace Spectrum.BL
{
    public class UserManager : GenericManager<UserModel>, IUserManager
    {
        public UserManager()
        {
            this.userRepository = new UserRepository();
            Mapper.CreateMap<AuthUsers, AuthUserModel>();
            Mapper.CreateMap<AuthUserModel, AuthUsers>();
            Mapper.CreateMap<AuthUsers, AuthUserModelEdit>();
            Mapper.CreateMap<AuthUserModelEdit, AuthUsers>();
            Mapper.CreateMap<MstSalesPerson, MstSalesPersonModel>();
            Mapper.CreateMap<MstSalesPersonModel, MstSalesPerson>();
            Mapper.CreateMap<AuthUserSiteRoleMap, AuthUserSiteRoleMapModel >();
            Mapper.CreateMap<AuthUserSiteRoleMapModel, AuthUserSiteRoleMap>();
        }

        private IUserRepository userRepository;

        public AuthUserModel IsUserExist(string UserId, string SiteCode)
        {
            var User = this.userRepository.IsUserExist(UserId,SiteCode);
            var userModel = Mapper.Map(User, new AuthUserModel());
            return userModel;
        }

        public AuthUserModel IsIdCardExist(string IdCard)
        {
            var User = this.userRepository.IsIdCardExist(IdCard);
            var userModel = Mapper.Map(User, new AuthUserModel());
            return userModel;
        }

        public AuthUserModel IsIdCardExistForOtherUser(string IdCard, string UserId)
        {
            var User = this.userRepository.IsIdCardExistForOtherUser(IdCard,UserId);
            var userModel = Mapper.Map(User, new AuthUserModel());
            return userModel;
        }
        public IList<AuthUserModelEdit> GetUserListEdit()
        {
            var userList = this.userRepository.GetUserListEdit().ToList();

            var userModelList = (from t in userList
                                 select Mapper.Map(t, new AuthUserModelEdit())).ToList();


            return userModelList;
        }

        public AuthUserModel GetUserById(string userId)
        {
            var userList = this.userRepository.GetUserById(userId);
            var userModel = Mapper.Map(userList, new AuthUserModel());
            return userModel;
        }
        public MstSalesPersonModel GetSalesPersonById(string userId)
        {
            var salesPersonDetails = this.userRepository.GetSalesPersonById(userId);
            var salesPersonDetailsModel = Mapper.Map(salesPersonDetails, new MstSalesPersonModel());
            return salesPersonDetailsModel;
        }
        public AuthUserSiteRoleMapModel GetRolesDataById(string userId)
        {
            var roleDetails = this.userRepository.GetRolesDataById(userId);
            var roleDetailsModel = Mapper.Map(roleDetails, new AuthUserSiteRoleMapModel());
            return roleDetailsModel;
        }

        public bool UpdateUser(UserModel userModel, bool flagRoleAddEdit, bool flagSalesAddEdit)
        {
            
            var user = Mapper.Map(userModel.AuthUserModel , new AuthUsers());
            var userRole = Mapper.Map(userModel.AuthUserSiteRoleMapModel, new AuthUserSiteRoleMap());
            var salesPerson = Mapper.Map(userModel.MstSalesPersonModel, new MstSalesPerson());
            return this.userRepository.UpdateUser(user, userRole, salesPerson, flagRoleAddEdit, flagSalesAddEdit);
        }
        public bool AddUser(UserModel userModel)
        {

            var user = Mapper.Map(userModel.AuthUserModel, new AuthUsers());
            var userRole = Mapper.Map(userModel.AuthUserSiteRoleMapModel, new AuthUserSiteRoleMap());
            var salesPerson = Mapper.Map(userModel.MstSalesPersonModel, new MstSalesPerson());
            return this.userRepository.AddUser(user, userRole, salesPerson);
        }
    }
}
