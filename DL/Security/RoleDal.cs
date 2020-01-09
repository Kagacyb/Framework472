using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using DL.Base;

namespace DL.Security
{
    class RoleDal : Base.BaseDal
    {
        internal DynamicListResult GetRoleForALL()
        {
            return DoSecurity<DynamicListResult, dynamic>((result, sqlConn) =>
            {
                var cmd = sqlConn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "rRoleMTRForAll";
                //       cmd.ExecuteNonQuery();

                dynamic data = new ExpandoObject();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr != null)
                    {
                        data = JsonHelper.Serialize(dr);
                    }
                }

                result.Pass(data);

            });
        }

        internal DynamicListResult GetUserInfo(string userID)
        {
            return DoSecurity<DynamicListResult, dynamic>((result, sqlConn) =>
            {


                dynamic data = new ExpandoObject();

                var cmd = sqlConn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(GetSqlParameter("UserID", userID));

                cmd.CommandText = "rRoleMTRByUserID";
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr != null)
                    {
                        data.Roles = JsonHelper.Serialize(dr);
                    }
                }

                cmd.CommandText = "rUserLocationRelationByUserID";
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr != null)
                    {
                        data.Locations = JsonHelper.Serialize(dr);
                    }
                }

                result.Pass(data);
            });
        }

        internal ActionResult ChangeUserPassword(string account, string newPwd, string oldPwd, bool isSystem, DateTime entryTime, string ip)
        {
            return DoTransaction<ActionResult, string>("SEC", (result, sqlConn, sqlTran) =>
             {
                 var cmd = sqlConn.CreateCommand();
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Transaction = sqlTran;

                 cmd.CommandText = "ChangeUserPassword_C2018";
                 cmd.Parameters.Add(GetSqlParameter("LoginAccount", account));
                 cmd.Parameters.Add(GetSqlParameter("OldPassword", oldPwd));
                 cmd.Parameters.Add(GetSqlParameter("NewPassword", newPwd));
                 cmd.ExecuteNonQuery();

                 //cmd.CommandText = "cPasswordLog_HFC2018";
                 //cmd.Parameters.Clear();
                 //cmd.Parameters.Add(GetSqlParameter("@LoginAccount", account));
                 //cmd.Parameters.Add(GetSqlParameter("@Password", newPwd));
                 //cmd.Parameters.Add(GetSqlParameter("@IsSystem", isSystem));
                 //cmd.Parameters.Add(GetSqlParameter("@EntryTime", entryTime.ToString("yyyy-MM-dd HH:mm:ss")));
                 //cmd.ExecuteNonQuery();

                 //cmd.CommandText = "cLoginLog_HFC2018";
                 //cmd.Parameters.Clear();
                 //cmd.Parameters.Add(GetSqlParameter("@LoginAccount", account));
                 //cmd.Parameters.Add(GetSqlParameter("@IP", ip));
                 //cmd.Parameters.Add(GetSqlParameter("@EXE", "ChangePassword"));
                 //cmd.Parameters.Add(GetSqlParameter("@EntryTime", entryTime.ToString("yyyy-MM-dd HH:mm:ss")));
                 //cmd.ExecuteNonQuery();

                 result.Pass();

             });
        }




        internal DynamicListResult GetUser(string account, string pwd)
        {
            return DoSecurity<DynamicListResult, dynamic>((result, sqlConn) =>
            {
                var cmd = sqlConn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "rUserForAccount_HFC2018";
                cmd.Parameters.Add(GetSqlParameter("LoginAccount", account));
                if (!string.IsNullOrEmpty(pwd))
                    cmd.Parameters.Add(GetSqlParameter("Password", pwd));

                dynamic data = new ExpandoObject();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr != null)
                    { data = JsonHelper.SerializeObject(dr); }
                }

                result.Pass(data);
            });
        }

        internal ActionResult DeleteUser(string id)
        {
            return DoSecurity<ActionResult, string>((result, sqlConn) =>
            {
                var cmd = sqlConn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dUserMTR";
                cmd.Parameters.Add(GetSqlParameter("ID", id));

                cmd.ExecuteNonQuery();

                result.Pass();

            });
        }

        internal ActionResult SaveRole(RoleMTRModel model, string accountID)
        {
            return DoSecurity<ActionResult, string>((result, sqlConn) =>
            {
                var cmd = sqlConn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "cuRoleMTR_C2018";
                if (string.IsNullOrEmpty(model.ID) == false)
                    cmd.Parameters.Add(GetSqlParameter("ID", model.ID));

                cmd.Parameters.Add(GetSqlParameter("RoleName", model.Name));

                cmd.Parameters.Add(GetSqlParameter("LastUpdateUserID", accountID));
                cmd.Parameters.Add(GetSqlParameter("Effectiveness", model.Effectiveness));
                cmd.Parameters.Add(new SqlParameter("RoleModuleTable", toTable(model.Modules)));
                cmd.Parameters.Add(new SqlParameter("RoleRightTable", toTable(model.Rights)));

                cmd.ExecuteNonQuery();

                result.Pass();

            });
        }

        internal ActionResult SaveUser(UserMTRModel model, string accountID)
        {
            return DoSecurity<ActionResult, string>((result, sqlConn) =>
            {
                var cmd = sqlConn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "cuUserMTR_C2018";
                if (string.IsNullOrEmpty(model.ID) == false)
                    cmd.Parameters.Add(GetSqlParameter("ID", model.ID));


                cmd.Parameters.Add(GetSqlParameter("LoginAccount", model.LoginAccount));
                cmd.Parameters.Add(GetSqlParameter("UserName", model.UserName));
                cmd.Parameters.Add(GetSqlParameter("UploadAccount", model.UploadAccount));
                cmd.Parameters.Add(GetSqlParameter("Password", EML.Util.CryptUtil.Encrypt("123456")));
                cmd.Parameters.Add(GetSqlParameter("CreatorID", accountID));
                cmd.Parameters.Add(GetSqlParameter("Effectiveness", model.Effectiveness));
                cmd.Parameters.Add(new SqlParameter("UserRoleTable", toTable(model.Roles)));
                cmd.Parameters.Add(new SqlParameter("UserLocationTable", toTable(model.Locations)));

                cmd.ExecuteNonQuery();

                result.Pass();

            });
        }


        internal DynamicListResult GetUserForALL()
        {
            return DoSecurity<DynamicListResult, dynamic>((result, sqlConn) =>
            {

                var cmd = sqlConn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "rUserMTRForAll";
                //cmd.ExecuteNonQuery();

                dynamic data = new ExpandoObject();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr != null)
                    {
                        data = JsonHelper.Serialize(dr);
                    }
                }

                result.Pass(data);

            });
        }

        internal DynamicListResult GetRoleInfo(string roleID)
        {
            return DoSecurity<DynamicListResult, dynamic>((result, sqlConn) =>
            {
                var cmd = sqlConn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "rRoleAndUserByRoleID_C2018";

                cmd.Parameters.Add(GetSqlParameter("ID", roleID));
                //   cmd.ExecuteNonQuery();

                dynamic data = new ExpandoObject();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr != null)
                    {
                        data.Menus = JsonHelper.Serialize(dr);

                        if (dr.NextResult())
                            data.Users = JsonHelper.Serialize(dr);

                        if (dr.NextResult())
                            data.UserRoles = JsonHelper.Serialize(dr);
                    }
                }

                result.Pass(data);
            });
        }

        internal DynamicListResult GetRoleEdit(string roleID)
        {
            return DoSecurity<DynamicListResult, dynamic>((result, sqlConn) =>
            {
                var cmd = sqlConn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "rRoleMTRByID_C2018";

                cmd.Parameters.Add(GetSqlParameter("ID", roleID));
                //   cmd.ExecuteNonQuery();

                dynamic data = new ExpandoObject();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr != null)
                    {
                        data.RoleInfo = JsonHelper.SerializeObject(dr);

                        if (dr.NextResult())
                            data.Menus = JsonHelper.Serialize(dr);


                    }
                }

                result.Pass(data);
            });
        }

        internal DynamicListResult GetUserMTR(string userID)
        {
            return DoSecurity<DynamicListResult, dynamic>((result, sqlConn) =>
            {
                var cmd = sqlConn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "rUserMTRByID";

                cmd.Parameters.Add(GetSqlParameter("ID", userID));

                dynamic data = new ExpandoObject();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr != null)
                    { data = JsonHelper.SerializeObject(dr); }
                }

                result.Pass(data);
            });
        }

        internal DynamicListResult GetUserEdit(string userID)
        {
            return DoSecurity<DynamicListResult, dynamic>((result, sqlConn) =>
            {
                var cmd = sqlConn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "rUserMTRByID_C2018";

                cmd.Parameters.Add(GetSqlParameter("ID", userID));
                //   cmd.ExecuteNonQuery();

                dynamic data = new ExpandoObject();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr != null)
                    {
                        data.UserInfo = JsonHelper.SerializeObject(dr);

                        if (dr.NextResult())
                            data.Roles = JsonHelper.Serialize(dr);


                        if (dr.NextResult())
                            data.Locations = JsonHelper.Serialize(dr);


                    }
                }

                result.Pass(data);
            });
        }

        internal DynamicListResult GetModuleRightMTR()
        {
            return DoSecurity<DynamicListResult, dynamic>((result, sqlConn) =>
            {
                dynamic data = new ExpandoObject();

                var cmd = sqlConn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "rSysModuleMTRForAll";
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr != null)
                        data.ModuleMTR = JsonHelper.Serialize(dr);
                }

                cmd.CommandText = "rSysRightMTRForAll";
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr != null)
                        data.RightMTR = JsonHelper.Serialize(dr);
                }

                result.Pass(data);
            });
        }

        private DataTable toTable(List<string> list)
        {
            var dt = new DataTable("ID");

            dt.Columns.Add("ID");
            list.ForEach(i => dt.Rows.Add(i));

            return dt;
        }
    }


}
