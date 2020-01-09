using DL.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Dynamic;

namespace DL.Security
{
    internal class UserDal : BaseDal
    {
        public DynamicListResult SecurityLogin(string account, string pwd)
        {
            return DoSecurity<DynamicListResult, dynamic>((result, sqlConn) =>
            {
                var cmd = sqlConn.CreateCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "rUserMTRByLoginAccountNPWD";

                cmd.Parameters.Add(GetSqlParameter("LoginAccount", account));
                cmd.Parameters.Add(GetSqlParameter("Password", EML.Util.CryptUtil.Encrypt(pwd)));

                dynamic data = new ExpandoObject();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr != null)
                    {
                        data = JsonHelper.SerializeObject(dr);
                    }
                }

                result.Pass(data);
            });
        }

        public UserResult WebLogin(string account, string pwd)
        {
            return DoSecurity<UserResult, User>((result, sqlConn) =>
             {
                 var cmd = sqlConn.CreateCommand();

                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.CommandText = "rUserMTRByLoginAccountNPWDNSAPCompanyCode";

                 cmd.Parameters.Add(GetSqlParameter("LoginAccount", account));
                 cmd.Parameters.Add(GetSqlParameter("Password", EML.Util.CryptUtil.Encrypt(pwd)));
                 //cmd.Parameters.Add(GetSqlParameter("SAPCompanyCode", companyCode));
                 var model = new User();
                 using (SqlDataReader dr = cmd.ExecuteReader())
                 {
                     if (dr != null)
                     {
                         while (dr.Read())
                         {

                             model.ID = ReadString(dr["ID"]);
                             model.Name = ReadString(dr["UserName"]);
                             model.Account = ReadString(dr["LoginAccount"]);
                             model.IsAdminstrator = ReadBoolean(dr["IsAdminstrator"]);
                           //  model.LocationTypeID = ReadInt(dr["LocationTypeID"]);
                             result.Pass(model);
                         }
                     }
                 }

                 if (string.IsNullOrEmpty(model.ID))
                     result.Fail("用户名或密码不正确。");
             });
        }

        public ActionResult GetOldCompanyCode(string newCompanyCode)
        {
            return DoMain<ActionResult, string>((result, sqlConn) =>
            {
                var cmd = sqlConn.CreateCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "rLocationCodeRelationByNewCode";

                cmd.Parameters.Add(GetSqlParameter("NewCode", newCompanyCode));
                var model = new User();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                        {

                            result.Pass(ReadString(dr["OldCode"]));
                        }
                    }
                }

                if (result.Success == false) { result.Pass(newCompanyCode); }

            });



        }

        public MenuResult GetMenu(string userID)
        {

            return DoSecurity<MenuResult, List<string>>((result, sqlConn) =>
           {
               var data = new List<string>();
               var cmd = sqlConn.CreateCommand();

               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandText = "rUserRightByUserID";

               cmd.Parameters.Add(GetSqlParameter("UserID", userID));

               using (SqlDataReader dr = cmd.ExecuteReader())
               {
                   if (dr != null)
                   {
                       while (dr.Read())
                       {

                           string key = Convert.ToString(dr["sysCode"]);

                           data.Add(key);

                       }
                       result.Pass(data);
                   }
               }
           });
        }

        public LocationListResult GetLocation(string userID)
        {
            return DoSecurity<LocationListResult, List<Location>>((result, sqlConn) =>
           {
               var data = new List<Location>();
               var cmd = sqlConn.CreateCommand();

               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandText = "rUserLocationRelationByUserID";
               cmd.Parameters.Add(GetSqlParameter("UserID", userID));

               using (SqlDataReader dr = cmd.ExecuteReader())
               {
                   if (dr != null)
                   {
                       while (dr.Read())
                       {
                           var model = new Location();
                           model.RelationID = Convert.ToString(dr["ID"]);
                           model.Code = Convert.ToString(dr["SAPLocationCode"]);
                           model.Name = Convert.ToString(dr["SAPLocationName"]);
                           model.CompanyCode = Convert.ToString(dr["SAPCompanyCode"]);
                           model.TypeID = Convert.ToInt32(dr["LocationTypeID"]);
                           model.TypeLevel = Convert.ToInt32(dr["LocationLevel"]);
                           data.Add(model);

                       }
                       result.Pass(data);
                   }
               }
           });
        }

        public LocationListResult GetLocationList(int typeID)
        {
            return DoMain<LocationListResult, List<Location>>((result, sqlConn) =>
            {
                var data = new List<Location>();
                var cmd = sqlConn.CreateCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "rLocationAndLocationTypeForAll";

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            var model = new Location();
                            model.ID = Convert.ToString(dr["ID"]);
                            model.ParentID = Convert.ToString(dr["ParentID"]);
                            model.Code = Convert.ToString(dr["SAPLocationCode"]);
                            model.Name = Convert.ToString(dr["SAPLocationName"]);
                            model.CompanyCode = Convert.ToString(dr["SAPCompanyCode"]);
                            model.LotCode = Convert.ToString(dr["LotCode"]);

                            model.TypeID = Convert.ToInt32(dr["LocationTypeID"]);
                            model.TypeName = Convert.ToString(dr["LocationTypeName"]);
                            model.TypeLevel = Convert.ToInt32(dr["Level"]);
                            data.Add(model);

                        }

                        // data.Sort((x, y) => x.Code.CompareTo(y.Code));

                        if (typeID > 0)
                            data = FilterLocations(typeID, data);


                        result.Pass(data);
                    }
                }
            });

        }

        public List<Location> FilterLocations(int typeID, List<Location> locations)
        {
            var filteredLocations = new List<Location>();
            var companyLocations = GetCompanyLocationListByLocationTypeId(typeID, locations);
            filteredLocations.AddRange(companyLocations);
            foreach (Location companyLocation in companyLocations)
            {
                GetChildrenLocation(companyLocation, locations, new Action<Location>(childLocation => filteredLocations.Add(childLocation)));
            }

            return filteredLocations;
        }

        public LocationListResult GetCompanyLocationList()
        {
            var result = GetLocationList(0);

            if (result.Success)
                result.Data = result.Data.Where(i => i.TypeLevel == 1).ToList();

            return result;

        }

        /// <summary>
        /// 取得总公司/分公司
        /// </summary>
        /// <param name="locationTypeId">总公司 1 ， 分公司 2</param>
        /// <param name="locations"></param>
        /// <returns></returns>
        public List<Location> GetCompanyLocationListByLocationTypeId(int typeID, List<Location> locations)
        {
            return locations.Where(i => i.TypeLevel == 1 && i.TypeID == typeID).ToList();

        }

        /// <summary>
        /// 取得子Location
        /// </summary>
        /// <param name="location"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        private void GetChildrenLocation(Location parent, List<Location> source, Action<Location> findAction)
        {
            foreach (Location location in source)
            {
                if (location.ParentID == parent.ID)
                {
                    findAction(location);
                    GetChildrenLocation(location, source, findAction);
                }
            }
        }

        internal ActionResult CreateLocation(Location sender)
        {
            return DoMain<ActionResult, string>((result, sqlConn) =>
            {
                var cmd = sqlConn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "cLocation_C2018";
                cmd.Parameters.Add(GetSqlParameter("ID", Guid.NewGuid().ToString()));
                cmd.Parameters.Add(GetSqlParameter("LocationTypeID", sender.TypeID));
                cmd.Parameters.Add(GetSqlParameter("SAPCompanyCode", sender.CompanyCode));
                cmd.Parameters.Add(GetSqlParameter("SAPLocationCode", sender.Code));
                cmd.Parameters.Add(GetSqlParameter("SAPLocationName", sender.Name));

                if (string.IsNullOrEmpty(sender.ParentID))
                    cmd.Parameters.Add(new SqlParameter("ParentID", DBNull.Value));
                else
                    cmd.Parameters.Add(GetSqlParameter("ParentID", sender.ParentID));
                //cmd.Parameters.Add(new SqlParameter("LotCode", DBNull.Value));

                var pass = cmd.ExecuteNonQuery() > 0;

                if (pass)
                    result.Pass("新增成功。");
                else
                    result.Fail("新增失败。");
            });
        }


        internal ActionResult UpdateLoaction(Location sender)
        {
            return DoMain<ActionResult, string>((result, sqlConn) =>
            {
                var cmd = sqlConn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "uLocation_C2018";
                cmd.Parameters.Add(GetSqlParameter("ID", sender.ID));
                cmd.Parameters.Add(GetSqlParameter("LocationTypeID", sender.TypeID));
                cmd.Parameters.Add(GetSqlParameter("SAPCompanyCode", sender.CompanyCode));
                cmd.Parameters.Add(GetSqlParameter("SAPLocationCode", sender.Code));
                cmd.Parameters.Add(GetSqlParameter("SAPLocationName", sender.Name));

                if (string.IsNullOrEmpty(sender.ParentID))
                    cmd.Parameters.Add(new SqlParameter("ParentID", DBNull.Value));
                else
                    cmd.Parameters.Add(GetSqlParameter("ParentID", sender.ParentID));

                //if (string.IsNullOrEmpty(sender.LotCode))
                //    cmd.Parameters.Add(new SqlParameter("LotCode", DBNull.Value));
                //else
                //    cmd.Parameters.Add(new SqlParameter("LotCode", sender.LotCode));

                var pass = cmd.ExecuteNonQuery() > 0;

                if (pass)
                    result.Pass("新增成功。");
                else
                    result.Fail("新增失败。");
            });
        }
        internal ActionResult DeleteLocation(Location sender)
        {

            return DoMain<ActionResult, string>((result, sqlConn) =>
            {
                var cmd = sqlConn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dLocation_C2018";
                cmd.Parameters.Add(GetSqlParameter("ID", sender.ID));


                var pass = cmd.ExecuteNonQuery() > 0;

                if (pass)
                    result.Pass("删除成功。");
                else
                    result.Fail("删除失败。");
            });
        }

    }
}
