using DL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Security
{
    public class Location
    {

        public string RelationID { get; set; }

        public string ID { get; set; }

        public string ParentID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string CompanyCode { get; set; }

        public string LotCode { get; set; }

        public int TypeID { get; set; }

        public int TypeLevel { get; set; }

        public string TypeName { get; set; }
    }

    public class LocationListResult : Result<List<Location>> { }

    public class LocationType
    {
        public LocationType() { SubLocationTypeList = new List<LocationType>(); }

        public string ID { get; set; }

        public string Name { get; set; }

        public int ParentID { get; set; }

        public int Level { get; set; }

        public int Group { get; set; }

        public List<LocationType> SubLocationTypeList { get; set; }
    }

}
