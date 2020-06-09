using System.Data;
using System.Data.SqlClient;
using Spectrum.DAL.RepositoryInterfaces;
using Spectrum.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using EntityState = System.Data.Entity.EntityState;

namespace Spectrum.DAL
{
  public class ArticleHierarchyRepository:GenericRepository<MstArticleNode>, IArticleHierarchyRepository
    {

        #region InsertUpdateDelete

        //code is added by irfan on 27/03/2018

      
       public bool SaveTree(MstArticleTree mstArticleTree)
        {
            bool tranResult = false;
            using (var dbContextTransaction = Context.Database.BeginTransaction())
            {
                try
                {
                    Context.MstArticleTree.Add(mstArticleTree);
                    Context.SaveChanges();

                    this.UpdateNextID(CommonModel.SiteCode, "AT");
                    dbContextTransaction.Commit();
                    tranResult = true;

                }
            catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    Logging.Logger.Log(ex, Logging.Logger.LogingLevel.Error);
                    throw;
                }
            }
            return tranResult;
        }

        public bool UpdateTree(MstArticleTree mstArticleTree)
        {
            bool tranResult = false;
            using (var dbContextTransaction = Context.Database.BeginTransaction())
            {
                try
                {
                    Context.Entry<MstArticleTree>(mstArticleTree).State = EntityState.Modified;
                    //--- Context.MstSupplier.Remove(mstArticleTree.MstArticleTreeLevel);
                    //--- Context.Entry<MstArticleTreeLevel>(mstArticleTree.MstArticleTreeLevel).State = EntityState.Deleted;
                    Context.SaveChanges();
                    dbContextTransaction.Commit();
                    tranResult = true;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    Logging.Logger.Log(ex, Logging.Logger.LogingLevel.Error);
                    throw ;
                }
            }
            return tranResult;
        }

        public bool SaveNode(MstArticleNode mstArticleNode)
        {
            bool tranResult = false;
            using (var dbContextTransaction = Context.Database.BeginTransaction())
            {
                try
                {
                    Context.MstArticleNode.Add(mstArticleNode);
                    Context.SaveChanges();

                    this.UpdateNextID(CommonModel.SiteCode, "AN");
                    dbContextTransaction.Commit();
                    tranResult = true;

                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    Logging.Logger.Log(ex, Logging.Logger.LogingLevel.Error);
                    throw ;
                }
            }
            return tranResult;
        }

        public bool UpdateNode(MstArticleNode mstArticleNode)
        {
            bool tranResult = false;
            using (var dbContextTransaction = Context.Database.BeginTransaction())
            {
                try
                {
                    Context.Entry<MstArticleNode>(mstArticleNode).State = EntityState.Modified;

                    Context.SaveChanges();

                    dbContextTransaction.Commit();
                    tranResult = true;

                }
                
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    Logging.Logger.Log(ex, Logging.Logger.LogingLevel.Error);
                    throw ;
                }
            }
            return tranResult;
        }

        public bool DeleteNode(string nodeCode)
        {
            bool tranResult = false;
            using (var dbContextTransaction = Context.Database.BeginTransaction())
            {
                try
                {
                    var nodeArticle = (Context.MstArticleNode.Where(me => me.Nodecode == nodeCode)).FirstOrDefault();
                    //var nodeArticleTreeNodeMap = (Context.ArticleTreeNodeMap.Where(me => me.Nodecode == nodeCode)).FirstOrDefault();
                    //nodeArticle.ArticleTreeNodeMap = nodeArticleTreeNodeMap;

                    //Context.ArticleTreeNodeMap.Remove(nodeArticleTreeNodeMap);
                    //Context.Entry<ArticleTreeNodeMap>(nodeArticleTreeNodeMap).State = EntityState.Deleted;

                    Context.MstArticleNode.Remove(nodeArticle);
                    Context.Entry<MstArticleNode>(nodeArticle).State = EntityState.Deleted;
                    Context.SaveChanges();

                    dbContextTransaction.Commit();
                    tranResult = true;

                }
                
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    Logging.Logger.Log(ex, Logging.Logger.LogingLevel.Error);
                    throw ;
                }
            }
            return tranResult;
        }

        public bool DeleteTree(string treeCode)
        {
            bool tranResult = false;
            using (var dbContextTransaction = Context.Database.BeginTransaction())
            {
                try
                {
                    var treeArticle = (Context.MstArticleTree.Where(me => me.TreeCode == treeCode)).FirstOrDefault();
                    Context.MstArticleTree.Remove(treeArticle);
                    Context.Entry<MstArticleTree>(treeArticle).State = EntityState.Deleted;
                    Context.SaveChanges();//code added by vipul for issue id 2258
                    dbContextTransaction.Commit();
                    tranResult = true;

                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    Logging.Logger.Log(ex, Logging.Logger.LogingLevel.Error);
                    throw ;
                }
            }
            return tranResult;
        }
 
	#endregion

        #region TreeMethods
      //code is added by irfan on 27/03/2018
        public IQueryable<MstTransaction> GetRoleHierarchyList(string sitecode)
        {
            try
            {
                //  return (Context.MstTransaction.OrderBy(a=>a.MainFunction).Distinct()).AsQueryable();
                return (from mst in Context.MstTransaction
                        join sat in Context.SiteAllowedTransactions on mst.TransactionCode equals sat.TransactionCode
                        where ((bool)sat.STATUS == true) && ((bool)mst.STATUS == true) && ((bool)sat.Active == true) && sat.SiteCode.Equals(sitecode)
                        select mst).AsQueryable();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<MstTransaction> GetUserRoleMap(string role)
        {
            try
            {
                //  return (Context.MstTransaction.OrderBy(a=>a.MainFunction).Distinct()).AsQueryable();
                return (from mst in Context.MstTransaction
                        join art in Context.AuthRoleTransactionMap on mst.TransactionCode equals art.AuthTransactionCode
                        join mr in Context.MstRole
                            on art.RoleID equals mr.RoleID
                        where ((bool)art.STATUS == true) && ((bool)mr.STATUS == true) && mr.RoleID.Equals(role)
                        select mst).AsQueryable();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool AddAuthUserSite(DataTable dtNodes, string username)
        {
            try
            {
                //   AuthUserSiteTransactionMap auth=new AuthUserSiteTransactionMap();
                IList<AuthUserSiteTransactionMap> authlist = new List<AuthUserSiteTransactionMap>();

                int i;
                for (i = 0; i < dtNodes.Rows.Count; i++)
                {
                    AuthUserSiteTransactionMap auth = new AuthUserSiteTransactionMap();
                    auth.UserID = username;
                    auth.SiteCode = CommonModel.SiteCode;
                    auth.AuthTransactionCode = dtNodes.Rows[i]["Nodes"].ToString();
                    auth.Rights = true;
                    auth.CREATEDAT = CommonModel.SiteCode;
                    auth.CREATEDBY = CommonModel.UserID;
                    auth.CREATEDON = CommonModel.CurrentDate;
                    auth.UPDATEDAT = CommonModel.SiteCode;
                    auth.UPDATEDBY = CommonModel.UserID;
                    auth.UPDATEDON = CommonModel.CurrentDate;
                    auth.STATUS = Convert.ToBoolean(dtNodes.Rows[i]["Status"]);
                    authlist.Add(auth);

                }
                foreach (AuthUserSiteTransactionMap obj in authlist)
                {
                    Context.AuthUserSiteTransactionMap.Add(obj);
                    Context.SaveChanges();
                }



                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateAuthUserSite(DataTable dtNodes, string username)
        {
            try
            {
                IList<AuthUserSiteTransactionMap> authlist = new List<AuthUserSiteTransactionMap>();
                int i;
                for (i = 0; i < dtNodes.Rows.Count; i++)
                {
                    AuthUserSiteTransactionMap auth = new AuthUserSiteTransactionMap();
                    auth.AuthTransactionCode = dtNodes.Rows[i]["Nodes"].ToString();
                    string str = dtNodes.Rows[i]["Nodes"].ToString();
                    auth = Context.AuthUserSiteTransactionMap.Where(a => a.UserID == username && a.AuthTransactionCode == str && a.SiteCode == CommonModel.SiteCode).FirstOrDefault();
                    if (auth != null)
                    {
                        auth.UPDATEDAT = CommonModel.SiteCode;
                        auth.UPDATEDBY = CommonModel.UserID;
                        auth.UPDATEDON = CommonModel.CurrentDate;
                        auth.STATUS = Convert.ToBoolean(dtNodes.Rows[i]["Status"]);
                        Context.SaveChanges();
                    }
                    else
                    {
                        AuthUserSiteTransactionMap auth1 = new AuthUserSiteTransactionMap();
                        auth1.UserID = username;
                        auth1.SiteCode = CommonModel.SiteCode;
                        auth1.AuthTransactionCode = dtNodes.Rows[i]["Nodes"].ToString();
                        auth1.Rights = true;
                        auth1.CREATEDAT = CommonModel.SiteCode;
                        auth1.CREATEDBY = CommonModel.UserID;
                        auth1.CREATEDON = CommonModel.CurrentDate;
                        auth1.UPDATEDAT = CommonModel.SiteCode;
                        auth1.UPDATEDBY = CommonModel.UserID;
                        auth1.UPDATEDON = CommonModel.CurrentDate;
                        auth1.STATUS = Convert.ToBoolean(dtNodes.Rows[i]["Status"]);
                        Context.AuthUserSiteTransactionMap.Add(auth1);
                        Context.SaveChanges();
                    }
                }

             
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetTransCode(string TransactionCode)
        {
            try
            {
                return Context.MstTransaction.Where(x => x.TransactionName == TransactionCode).Select(y => y.TransactionCode).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool isUserExits(string UserName)
        {
            try
            {
                bool isExist = Context.AuthUserSiteTransactionMap.Count(x => x.UserID == UserName) > 0;
                if (isExist)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<MstTransaction> GetNodeTableForUpdate(string user)
        {
            try
            {
                return (from mst in Context.MstTransaction
                        join auth in Context.AuthUserSiteTransactionMap
                        on mst.TransactionCode equals auth.AuthTransactionCode
                        where auth.UserID.Equals(user) && auth.STATUS == true
                        select mst).AsQueryable();
                //select new { transectionName=mst.TransactionName,Status=auth.STATUS });


                //var data=from msT in Context.MstTransaction join 
                //           auth in Context.AuthUserSiteTransactionMap 
                //          on msT.TransactionCode equals auth.AuthTransactionCode
                //          where(auth.UserID==user) 

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<MstTransaction> GetNodeTableForNewUpdate(string user)
        {
            try
            {
                return (from mst in Context.MstTransaction
                        join auth in Context.AuthUserSiteTransactionMap
                        on mst.TransactionCode equals auth.AuthTransactionCode
                        where auth.UserID.Equals(user) && auth.STATUS == false
                        select mst).AsQueryable();
                //select new { transectionName=mst.TransactionName,Status=auth.STATUS });


                //var data=from msT in Context.MstTransaction join 
                //           auth in Context.AuthUserSiteTransactionMap 
                //          on msT.TransactionCode equals auth.AuthTransactionCode
                //          where(auth.UserID==user) 

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CheckUserStatus(string userCode, string user)
        {
            try
            {
                var result = Context.AuthUserSiteTransactionMap.Where(x => x.AuthTransactionCode == userCode && x.UserID == user).Select(y => y.STATUS == true).FirstOrDefault();
                if (result)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool CheckUserNewStatus(string userCode, string user)
        {
            try
            {
                var result = Context.AuthUserSiteTransactionMap.Where(x => x.AuthTransactionCode == userCode && x.UserID == user).Select(y => y.STATUS == false).FirstOrDefault();
                if (result)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string GetSiteEmilId(string sitecode)
        {
            try
            {
                return Context.MstSite.Where(x => x.SiteCode == sitecode).Select(y => y.EmailId).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IQueryable<DefaultConfig> GetUsernamePassword()
        {
            try
            {
                return Context.DefaultConfig.Where(x => x.Sitecode == "BOCommon").AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      //================================================================================================================================
      
      public IQueryable<ItemHierarchy> GetItemHierarchyList()
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter { ParameterName = "V_NodeCode", SqlDbType = SqlDbType.VarChar, SqlValue = "ANCCCE000000049" };
                
                var transactions = Context.Database.SqlQuery<DataTable>("EXEC dbo.GetArticleXlsData @V_NodeCode", sqlParameters).ToList();
                
                //System.Data.SqlClient.SqlParameter[] pppss = new System.Data.SqlClient.SqlParameter[1];
                //pppss[0] = new SqlParameter("V_NodeCode", SqlDbType.VarChar, 50);
                //pppss[0].Value = "ANCCCE000000049";
                //var ddd = Context.Database.SqlQuery<object>("GetArticleXlsData", pppss);

                //var p = ddd.ToList();

                return (
                    (from at in Context.MstArticleTree
                     where at.STATUS == true
                     select new ItemHierarchy
                     {
                         Nodecode = at.TreeCode,
                         NodeName = at.TreeName,
                         ParentNodecode = null,
                         ParentNodeName = null,
                         TreeCode = at.TreeCode,
                         TreeName = at.TreeName,
                         ISThisLastNode = false,
                         LevelCode = "0",
                         LevelName = "",
                         NodeType = 1
                     }).Concat
                    (from atnm in Context.ArticleTreeNodeMap
                     join man in Context.MstArticleNode on atnm.Nodecode equals man.Nodecode
                     join at in Context.MstArticleTree on atnm.Treecode equals at.TreeCode
                     join atl in Context.MstArticleTreeLevel on new { a = at.TreeCode, b = man.LevelCode } equals new { a = atl.TreeCode, b = atl.LevelCode }
                     join pman in Context.MstArticleNode on atnm.ParentNodecode equals pman.Nodecode
           into t
                     from rt in t.DefaultIfEmpty()
                     where at.STATUS == true && man.STATUS == true
                     select new ItemHierarchy
                     {
                         Nodecode = atnm.Nodecode,
                         NodeName = man.NodeName,
                         //orderby (c.Description1 == null ? string.Empty : c.Description1) + (c.Description2 == null ? string.Empty : c.Description2) 
                         ParentNodecode = (atnm.ParentNodecode == null ? atnm.Treecode : atnm.ParentNodecode),
                         ParentNodeName = (rt.NodeName == null ? at.TreeName : rt.NodeName),
                         TreeCode = atnm.Treecode,
                         TreeName = at.TreeName,
                         ISThisLastNode = man.ISThisLastNode,
                         LevelCode = man.LevelCode,
                         LevelName = atl.LevelName,
                         NodeType = 2
                     })).AsQueryable();

       

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      public int GetMaxTreeLevelCode(string treeCode)
      {
          return
              Convert.ToInt32(
                  (Context.MstArticleTreeLevel.Where(x => x.TreeCode == treeCode && x.STATUS == true)
                      .OrderByDescending(y => y.LevelCode)
                      .Select(y => y.LevelCode)).FirstOrDefault());
      }

      public string GetTreeLevelName(string treeCode, int levelCode)
      {
          string levelCodeCheck = levelCode.ToString();
          return (Context.MstArticleTreeLevel.Where(x => x.TreeCode == treeCode && x.STATUS == true && x.LevelCode == levelCodeCheck).Select(y => y.LevelName)).FirstOrDefault();
      }

      public IList<MstArticleTreeLevel> GetTreeLevels(string treeCode)
      {
          return (from var in Context.MstArticleTreeLevel
                  where var.TreeCode == treeCode
                  select var).ToList();

      }

      public MstArticleTree GetTreeById(string treeCode)
      {
          return (Context.MstArticleTree.Single(x => x.TreeCode == treeCode));
      }

        #endregion

        #region NodeMethods
    
      public int GetTreeActiveArticlesNoCount(string treeCode)
        {
            try
            {
                return Context.MstArticleNode.Count(me => me.Treecode == treeCode && me.STATUS == true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      public int GetChildArticleNoCount(string parentNodecode)
      {
          try
          {
              return Context.ArticleTreeNodeMap.Count(me => me.ParentNodecode == parentNodecode && me.STATUS == true);
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }

      public int GetActiveArticlesNoCount(String treeNodeMap)
      {
          int articleCount = 0;

          var thisNode = (from node in Context.MstArticleNode
                          where node.Nodecode == treeNodeMap && node.STATUS == true
                          select node).ToList();

          if (thisNode.Count > 0 && thisNode[0].ISThisLastNode == false)
          {
              var childNodes = (from node in Context.MstArticleNode
                                join antm in Context.ArticleTreeNodeMap on node.Nodecode equals antm.ParentNodecode
                                join nodeResult in Context.MstArticleNode on antm.Nodecode equals nodeResult.Nodecode
                                where node.Nodecode == treeNodeMap && ((bool)node.STATUS == true) && ((bool)antm.STATUS == true)
                                select nodeResult).ToList();


              foreach (var node in childNodes)
              {
                  if (node.ISThisLastNode == true)
                  {
                      int count = Context.MstArticle.Count(me => me.LastNodeCode == node.Nodecode && me.STATUS == true);
                      articleCount = articleCount + count;
                  }
                  else
                  {
                      articleCount += GetActiveArticlesNoCount(node.Nodecode);
                  }
              }
          }
          else
          {
              if (thisNode.Count > 0 && thisNode[0].ISThisLastNode == true)
              {
                  string nodeCode = thisNode[0].Nodecode;
                  int rcount = Context.MstArticle.Count(me => me.LastNodeCode == nodeCode && me.STATUS == true);
                  articleCount = articleCount + rcount;
              }
          }
          return articleCount;
      }

      public string GetArticleHierarchyString(string treeNodeMap, bool firstentry)
      {
          StringBuilder hierarchy = new StringBuilder();
          hierarchy.Length = 0;

          var nodelst = (from node in Context.MstArticleNode
                         where node.Nodecode == treeNodeMap && node.STATUS == true
                         select node).ToList();

          if (nodelst.Count==0)
          {
              hierarchy.Insert(0, "");
              return hierarchy.ToString();
          }

          if (firstentry)
          {
              hierarchy.Insert(0, nodelst[0].NodeName);
          }
          else
          {
              hierarchy.Insert(0, nodelst[0].NodeName + ".");
          }
          var nodeMap = (from nodemap in Context.ArticleTreeNodeMap
                         where nodemap.Nodecode == treeNodeMap && nodemap.STATUS == true
                         select nodemap).ToList();

          if (nodeMap[0].ParentNodecode != null && nodeMap[0].ParentNodecode.Trim().ToLower() != "")
          {
              if (nodeMap[0].ParentNodecode != nodeMap[0].Treecode)
              {
                  hierarchy.Insert(0, GetArticleHierarchyString(nodeMap[0].ParentNodecode, false));
              }
              else
              {
                  string treeCode = Convert.ToString(nodelst[0].Treecode);
                  string treeName = Context.MstArticleTree.Single(me => me.TreeCode == treeCode).TreeName;
                  hierarchy.Insert(0, treeName + ".");
              }
          }
          else
          {
              string treeCode = Convert.ToString(nodelst[0].Treecode);
              string treeName = Context.MstArticleTree.Single(me => me.TreeCode == treeCode).TreeName;
              hierarchy.Insert(0, treeName + ".");
          }
          return hierarchy.ToString();
      }
      
      public MstArticleNode GetArticleByID(object articleNodeCode)
      {
          return (Context.MstArticleNode.Single(x => x.Nodecode == articleNodeCode));
      }

        #endregion

    }
}
