using DL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Security
{
    public class User
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string UploadAccount { get; set; }
        public bool IsAdminstrator { get; set; }
        //  public int LocationTypeID { get; set; }
        //public string CompanyCode { get; set; }

        //public string OldCompanyCode { get; set; }
    }

    public class UserResult : Result<User> { }

}
