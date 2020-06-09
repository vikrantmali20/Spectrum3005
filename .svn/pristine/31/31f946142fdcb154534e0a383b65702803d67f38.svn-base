using System.Collections.Generic;
using System.Linq;
using Spectrum.BL.BusinessInterface;
using Spectrum.BL.Mappers;
using Spectrum.DAL;
using Spectrum.Models;
using Spectrum.DAL.RepositoryInterfaces;
using AutoMapper;
using System.Data;

namespace Spectrum.BL
{
    public class ArticleHierarchyManager : GenericManager<ArticleTreeModel>, IArticleHierarchyManager
    {
        public ArticleHierarchyManager()
        {
            this.commonRepository = new CommonRepository();
            this.articleHierarchyRepository = new ArticleHierarchyRepository();

            Mapper.CreateMap<ArticleTreeModel, MstArticleTree>();
            Mapper.CreateMap<MstArticleTree, ArticleTreeModel>();

            Mapper.CreateMap<ArticleTreeLevelModel, MstArticleTreeLevel>();
            Mapper.CreateMap<MstArticleTreeLevel, ArticleTreeLevelModel>();

            Mapper.CreateMap<ArticleNodeModel, MstArticleNode>();
            Mapper.CreateMap<MstArticleNode, ArticleNodeModel>();

            Mapper.CreateMap<ArticleTreeNodeMap, ArticleTreeNodeMapModel>();
            Mapper.CreateMap<ArticleTreeNodeMapModel, ArticleTreeNodeMap>();

            //Mapper.CreateMap<MstTransaction, MstTransactionModel>();
            //Mapper.CreateMap<MstTransactionModel, MstTransaction>();

        }

        private ICommonRepository commonRepository;
        private IArticleHierarchyRepository articleHierarchyRepository;

        #region InsertUpdateDelete
      
        public bool SaveTree(ArticleTreeModel articleTreeModel, List<ArticleTreeLevelModel> articleTreeLevelModel, ref string autoTreeCode)
        {
            bool tranResult = false;

            int nextNo = commonRepository.GetNextID(CommonModel.SiteCode, "AT");
            // ... Format Article code into 15 digit ...
            articleTreeModel.TreeCode = "ATC"
                                        + CommonModel.SiteCode.ToString().Substring(CommonModel.SiteCode.ToString().Length - 3, 3)
                                        + string.Format("{0}", nextNo.ToString().PadLeft(9, '0'));

            autoTreeCode = articleTreeModel.TreeCode;

            articleTreeModel.ToAddOrModifyEntity(true);
            var articleTree = Mapper.Map(articleTreeModel, new MstArticleTree());

            for (int rowLevel = 0; rowLevel < articleTreeLevelModel.Count; rowLevel++)
            {
                articleTreeLevelModel[rowLevel].TreeCode = autoTreeCode;
                articleTreeLevelModel[rowLevel].ToAddOrModifyEntity(true);
            }

            List<MstArticleTreeLevel> articleTreeLevellist = (from p in articleTreeLevelModel select Mapper.Map(p, new MstArticleTreeLevel())).ToList();
            articleTree.MstArticleTreeLevel = articleTreeLevellist;

            tranResult = this.articleHierarchyRepository.SaveTree(articleTree);
            return tranResult;
        }

        public bool UpdateTree(ArticleTreeModel articleTreeModel, List<ArticleTreeLevelModel> articleTreeLevelModel)
        {
            bool tranResult = false;
            var articleTree = articleHierarchyRepository.GetTreeById(articleTreeModel.TreeCode);
           
            articleTree.ToAddOrModifyEntity(false);
            articleTree.TreeName = articleTreeModel.TreeName;

            IList<MstArticleTreeLevel> articleTreeLevelOldList =
                this.articleHierarchyRepository.GetTreeLevels(articleTreeModel.TreeCode);


            for (int newindex = 0; newindex < articleTreeLevelModel.Count; newindex++)
            {
                var treeLevel = articleTreeLevelOldList.Where(me => me.LevelCode == articleTreeLevelModel[newindex].LevelCode).FirstOrDefault();
                if (treeLevel != null)
                {//Update Case
                    articleTreeLevelModel[newindex].ToAddOrModifyEntity(false);
                    articleTreeLevelModel[newindex].CreatedAt = treeLevel.CREATEDAT;
                    articleTreeLevelModel[newindex].CreatedBy = treeLevel.CREATEDBY;
                    articleTreeLevelModel[newindex].CreatedOn = treeLevel.CREATEDON;
                }
                else
                {//new Case
                    articleTreeLevelModel[newindex].ToAddOrModifyEntity(true);
                }
            }
            var articleTreeLevelNewlist = (from p in articleTreeLevelModel 
                select Mapper.Map(p, new MstArticleTreeLevel())
                ).ToList();

            articleTree.MstArticleTreeLevel = articleTreeLevelNewlist;

            tranResult = this.articleHierarchyRepository.UpdateTree(articleTree);

            return tranResult;
        }

        public bool SaveNode(ArticleNodeModel articleNodeModel, ArticleTreeNodeMapModel articleTreeNodeMapModel, ref string autoNodeCode)
        {
            bool tranResult = false;

            int nextNo = commonRepository.GetNextID(CommonModel.SiteCode, "AN");
            // ... Format Article code into 15 digit ...
            articleNodeModel.Nodecode = "ANC"
                                        + CommonModel.SiteCode.ToString().Substring(CommonModel.SiteCode.ToString().Length - 3, 3)
                                        + string.Format("{0}", nextNo.ToString().PadLeft(9, '0'));

            autoNodeCode = articleNodeModel.Nodecode;
            articleTreeNodeMapModel.Nodecode = articleNodeModel.Nodecode;

            articleNodeModel.ToAddOrModifyEntity(true);
            articleTreeNodeMapModel.ToAddOrModifyEntity(true);

            var articleNode = Mapper.Map(articleNodeModel, new MstArticleNode());
            var articleTreeNodeMap = Mapper.Map(articleTreeNodeMapModel, new ArticleTreeNodeMap());

            articleNode.ArticleTreeNodeMap = articleTreeNodeMap;

            tranResult = this.articleHierarchyRepository.SaveNode(articleNode);
            return tranResult;
        }
        
        public bool UpdateNode(ArticleNodeModel articleNodeModel)
        {
            bool tranResult = false;
            articleNodeModel.ToAddOrModifyEntity(true);

            var articleNode = this.articleHierarchyRepository.GetArticleByID(articleNodeModel.Nodecode);
            Mapper.Map(articleNodeModel, articleNode);

            tranResult = this.articleHierarchyRepository.UpdateNode(articleNode);
            return tranResult;
        }

        public bool DeleteTree(string treeCode)
        {
            return this.articleHierarchyRepository.DeleteTree(treeCode);
        }

        public bool DeleteNode(string nodeCode)
        {
            return this.articleHierarchyRepository.DeleteNode(nodeCode);
        }

        #endregion
        
        #region TreeMethods

     

        public IQueryable<ItemHierarchy> GetItemHierarchyList()
        {
            return this.articleHierarchyRepository.GetItemHierarchyList();
        }
        //code is added by irfan on 27/3/2018
        public IQueryable<MstTransaction> GetRoleHierarchyList(string sitecode)
        {
            return this.articleHierarchyRepository.GetRoleHierarchyList(sitecode);
        }
        public IQueryable<MstTransaction> GetUserRoleMap(string role)
        {
            return this.articleHierarchyRepository.GetUserRoleMap(role);
        }
        public string GetTransCode(string TransactionCode)
        {
            return this.articleHierarchyRepository.GetTransCode(TransactionCode);
        }
        public bool isUserExits(string UserName)
        {
            return this.articleHierarchyRepository.isUserExits(UserName);
        }
        public IQueryable<MstTransaction> GetNodeTableForUpdate(string user)
        {
            return this.articleHierarchyRepository.GetNodeTableForUpdate(user);
        }
        public IQueryable<MstTransaction> GetNodeTableForNewUpdate(string user)
        {
            return this.articleHierarchyRepository.GetNodeTableForNewUpdate(user);
        }
        public bool CheckUserStatus(string userCode, string user)
        {
            return this.articleHierarchyRepository.CheckUserStatus(userCode, user);
        }
        public bool CheckUserNewStatus(string userCode, string user)
        {
            return this.articleHierarchyRepository.CheckUserNewStatus(userCode, user);
        }
        public bool AddAuthUserSite(DataTable dtNodes, string username)
        {
            return this.articleHierarchyRepository.AddAuthUserSite(dtNodes, username);
        }
       public bool UpdateAuthUserSite(DataTable dtNodes, string username)
       {
           return this.articleHierarchyRepository.UpdateAuthUserSite(dtNodes, username);
       }
       public string GetSiteEmilId(string sitecode)
       {
           return this.articleHierarchyRepository.GetSiteEmilId(sitecode);
       }
       public IQueryable<DefaultConfig> GetUsernamePassword()
       {
           return this.articleHierarchyRepository.GetUsernamePassword();
       }
        //=====================================================================

        public int GetMaxTreeLevelCode(string treeCode)
        {
            return this.articleHierarchyRepository.GetMaxTreeLevelCode(treeCode);
        }

        public string GetTreeLevelName(string treeCode, int levelCode)
        {
            return this.articleHierarchyRepository.GetTreeLevelName(treeCode, levelCode);
        }

        public IList<ArticleTreeLevelModel> GetTreeLevels(string treeCode)
        {
            var articleTreeLevelList = this.articleHierarchyRepository.GetTreeLevels(treeCode);

            return (from p in articleTreeLevelList
                    select Mapper.Map(p, new ArticleTreeLevelModel())
                    ).ToList();

        }

        #endregion
        
        #region NodeMethods

        public int GetChildArticleNoCount(string parentNodecode)
        {
            return this.articleHierarchyRepository.GetChildArticleNoCount(parentNodecode);
        }

        public int GetActiveArticlesNoCount(string nodecode)
        {
            return this.articleHierarchyRepository.GetActiveArticlesNoCount(nodecode);
        }

        public string GetArticleHierarchyString(string treeNodeMap, bool firstentry)
        {
            return this.articleHierarchyRepository.GetArticleHierarchyString(treeNodeMap, firstentry);
        }

        public int GetTreeChildArticleNoCount(string treeCode)
        {
            return this.articleHierarchyRepository.GetTreeActiveArticlesNoCount(treeCode);
        }

        #endregion


    }
}
