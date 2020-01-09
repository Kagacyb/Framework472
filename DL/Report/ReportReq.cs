using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Report
{
    public class DeliveryRequest
    {
        public string ID { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string Vbeln { get; set; }

        public string Matnr { get; set; }

        public string Tanum { get; set; }
    }

    public class PalletRequest
    {
        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string Factory { get; set; }

        public string LineName { get; set; }

        public string Matnr { get; set; }

        public string Charg { get; set; }

        public string PalletBarcode { get; set; }
    }
}
