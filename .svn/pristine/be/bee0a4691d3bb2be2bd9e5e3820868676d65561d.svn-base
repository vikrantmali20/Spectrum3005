using Spectrum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Spectrum.DAL.RepositoryInterfaces
{
    public interface IArticleHierarchyRepository
    {
        bool SaveTree(MstArticleTree mstArticleTree);
        bool SaveNode(MstArticleNode mstArticleNode);
         IQueryable<ItemHierarchy> GetItemHierarchyList();
         int GetChildArticleNoCount(string parentNodecode);
         int GetActiveArticlesNoCount(String treeNodeMap);
        String GetArticleHierarchyString(string treeNodeMap, bool firstentry);

        int GetMaxTreeLevelCode(string treeCode);

        string GetTreeLevelName(string treeCode, int levelCode);

        IList<MstArticleTreeLevel> GetTreeLevels(string treeCode);

        MstArticleNode GetArticleByID(object articleNode);
        bool UpdateTree(MstArticleTree articleTree);
        MstArticleTree GetTreeById(string treeCode);
        bool UpdateNode(MstArticleNode articleNode);
        bool DeleteNode(string nodeCode);
        int GetTreeActiveArticlesNoCount(string treeCode);
        bool DeleteTree(string treeCode);
        //code added by irfan on 27/3/2018
        IQueryable<MstTransaction> GetRoleHierarchyList(string sitecode);
        IQueryable<MstTransaction> GetUserRoleMap(string role);
        bool AddAuthUserSite(DataTable dtNodes, string username);
        string GetTransCode(string TransactionCode);
        bool isUserExits(string UserName);
        IQueryable<MstTransaction> GetNodeTableForUpdate(string user);
        IQueryable<MstTransaction> GetNodeTableForNewUpdate(string user);
        bool CheckUserStatus(string userCode, string user);
        bool CheckUserNewStatus(string userCode, string user);
        bool UpdateAuthUserSite(DataTable dtNodes, string username);
        string GetSiteEmilId(string sitecode);
        IQueryable<DefaultConfig> GetUsernamePassword();
        //====================================================
    }
}
