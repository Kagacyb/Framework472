using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Security
{
    public class RoleMTRModel
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public bool Effectiveness { get; set; }

        public List<string> Modules { get; set; }

        public List<string> Rights { get; set; }
    }

    public class UserMTRModel
    {
        public string ID { get; set; }

        public string LoginAccount { get; set; }

        public string UploadAccount { get; set; }
        public string UserName { get; set; }

        public bool Effectiveness { get; set; }

        public List<string> Roles { get; set; }

        public List<string> Locations { get; set; }
    }
}
