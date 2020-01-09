using DL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Security
{
    public class RoleBll
    {
        private RoleDal dal = new RoleDal();
        public DynamicListResult GetRoleForALL()
        {
            return dal.GetRoleForALL();
        }

        public DynamicListResult GetRoleInfo(string roleID)
        {
            return dal.GetRoleInfo(roleID);
        }

        public DynamicListResult GetUserForALL()
        {
            return dal.GetUserForALL();
        }

        public DynamicListResult GetUserInfo(string userID)
        {
            return dal.GetUserInfo(userID);
        }

        public DynamicListResult GetRoleEdit(string roleID)
        {
            return dal.GetRoleEdit(roleID);
        }

        public DynamicListResult GetUserEdit(string userID)
        {
            return dal.GetUserEdit(userID);
        }

        public DynamicListResult GetModuleRightMTR()
        {
            return dal.GetModuleRightMTR();
        }

        public ActionResult SaveRole(RoleMTRModel model, string accountID)
        {
            return dal.SaveRole(model, accountID);
        }

        public ActionResult SaveUser(UserMTRModel model, string accountID)
        {
            return dal.SaveUser(model, accountID);
        }

        public ActionResult DeleteUser(string id)
        {
            return dal.DeleteUser(id);
        }

        public ActionResult ChangePwd(string userID, string ip, string oldPwd = "", string newPwd = "123456", bool reset = true)
        {

            var userReq = dal.GetUserMTR(userID);

            if (userReq.Success == false)
            {
                var r = new ActionResult();
                r.Fail("获取用户信息失败。");
                return r;
            }

            string dbPwd = Convert.ToString(userReq.Data.Password);
            string account = userReq.Data.LoginAccount;


            if (reset == false)
                if (oldPwd != EML.Util.CryptUtil.Decrypt(dbPwd))
                {
                    var r = new ActionResult();
                    r.Fail("旧密码不正确。");
                    return r;

                }

            return dal.ChangeUserPassword(account, EML.Util.CryptUtil.Encrypt(newPwd), dbPwd, reset, DateTime.Now, ip);
        }

        public ActionResult ChangeUserPassword(string account, string newPwd, string oldPwd, bool isSystem, DateTime entryTime, string ip)
        {
            return dal.ChangeUserPassword(account, EML.Util.CryptUtil.Encrypt(newPwd), oldPwd, isSystem, DateTime.Now, ip);

        }

        public DynamicListResult GetUser(string user, string pwd)
        {
            return dal.GetUser(user, string.IsNullOrEmpty(pwd) ? null : EML.Util.CryptUtil.Encrypt(pwd));
        }


    }
}
