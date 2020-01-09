using DL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Security
{
    public class UserBll
    {
        private UserDal dal = new UserDal();

        public ActionResult GetOldCompanyCode(string newCompanyCode)
        {
            return dal.GetOldCompanyCode(newCompanyCode);

        }

        public UserResult WebLogin(string account, string pwd)
        {
            var result = dal.WebLogin(account, pwd);
            return result;
        }

        public MenuResult GetMenu(string userID)
        {
            return dal.GetMenu(userID);
        }

        public LocationListResult GetLocation(int locationTypeID, string userID)
        {
            var result = dal.GetLocation(userID);

            if (result.Success)
            {
                result.Data = dal.GetCompanyLocationListByLocationTypeId(locationTypeID, result.Data);
            }

            return result;
        }

        public LocationListResult GetLocation(string userID)
        {
            var result = dal.GetLocation(userID);

            if (result.Success)
            {
                result.Data = result.Data.Where(i => i.TypeLevel == 1).ToList();
            }

            return result;
        }

        public LocationListResult GetSubCompanyList()
        {
            var result = dal.GetCompanyLocationList();

            result.Data = dal.GetCompanyLocationListByLocationTypeId(2, result.Data);

            return result;
        }

        public LocationListResult GetCompanyLocationList()
        {
            var result = dal.GetCompanyLocationList();
            return result;
        }

        public LocationListResult GetLocationList(int typeId)
        {
            return dal.GetLocationList(typeId);
        }

        public ActionResult CreateLocation(Location sender)
        {
            return dal.CreateLocation(sender);
        }

        public ActionResult UpdateLocation(Location sender)
        {
            return dal.UpdateLoaction(sender);
        }

        public ActionResult DeleteLocation(Location sender)
        {
            return dal.DeleteLocation(sender);
        }

        public DynamicListResult SecurityLogin(string account, string pwd)
        {
            return dal.SecurityLogin(account, pwd);
        }
    }
}
