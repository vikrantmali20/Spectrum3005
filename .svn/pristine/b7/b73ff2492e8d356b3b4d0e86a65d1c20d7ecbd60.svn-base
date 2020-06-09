using Spectrum.DAL;
using Spectrum.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace Spectrum.BL.BusinessInterface
{
  public interface IArticleHierarchyManager
  {
      bool SaveTree(ArticleTreeModel articleTreeModel, List<ArticleTreeLevelModel> articleTreeLevelModel,
          ref string autoTreeCode);

      
      bool SaveNode(ArticleNodeModel articleNodeModel,ArticleTreeNodeMapModel atnmModel, ref string autoNodeCode);
      bool UpdateNode(ArticleNodeModel articleNodeModel);

      bool UpdateTree(ArticleTreeModel articleTreeModel, List<ArticleTreeLevelModel> articleTreeLevelModel);
      
      IQueryable<ItemHierarchy> GetItemHierarchyList();
      string GetArticleHierarchyString(string treeNodeMap, bool firstentry);
      int GetChildArticleNoCount( string parentNodecode);

      int GetMaxTreeLevelCode(string treeCode);
      string GetTreeLevelName(string treeCode, int levelCode);

      IList<ArticleTreeLevelModel> GetTreeLevels(string p);
      bool DeleteNode(string nodeCode);
      int GetTreeChildArticleNoCount(string treeCode);
      bool DeleteTree(string treeCode);
      int GetActiveArticlesNoCount(string nodecode);
      //code added by irfan on 27/3/2018
      IQueryable<MstTransaction> GetRoleHierarchyList(string sitecode);
      IQueryable<MstTransaction> GetUserRoleMap(string role);
      string GetTransCode(string TransactionCode);
      bool isUserExits(string UserName);
      bool CheckUserStatus(string userCode, string user);
      bool CheckUserNewStatus(string userCode, string user);
      IQueryable<MstTransaction> GetNodeTableForUpdate(string user);
      IQueryable<MstTransaction> GetNodeTableForNewUpdate(string user);
      bool AddAuthUserSite(DataTable dtNodes, string username);
      bool UpdateAuthUserSite(DataTable dtNodes, string username);
      string GetSiteEmilId(string sitecode);
      IQueryable<DefaultConfig> GetUsernamePassword();
      //=======================================================
  }
}
