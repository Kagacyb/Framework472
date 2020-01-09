using DL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Report
{
    public class ReportBll
    {
        private ReportDal dal = new ReportDal();
        public DynamicListResult ProductDeliveryScanNote_Search(DeliveryRequest request)
        {
            string sql = "SELECT A.ID,B.ID DETAIL_ID, A.VBELN,A.TANUM,B.MATNR,B.CHARG,B.MAKTX,B.DLFIMG,B.SCAN_QTY FROM PRODUCTION_DN_MAIN A, PRODUCTION_DN_DETAIL B WHERE A.ID = B.PARENT_ID";

            sql += OracleHelper.GetConditionByDateFromTo("A.CREATE_TIME", request.FromDate, request.ToDate);

            if (!string.IsNullOrEmpty(request.Vbeln))
                sql += $" AND A.VBELN = '{request.Vbeln}' ";

            if (!string.IsNullOrEmpty(request.Matnr))
                sql += $" AND B.MATNR = '{request.Matnr}' ";

            if (!string.IsNullOrEmpty(request.Tanum))
                sql += $" AND A.TANUM = '{request.Tanum}' ";

            sql += "ORDER BY A.VBELN, B.MATNR, B.CHARG";

            return dal.ProductDeliveryScanNote_Search(sql);
        }

        public DynamicListResult ProductDeliveryScanNote_GetDetail(DeliveryRequest request)
        {
            string sql = string.Format("WITH CTE_A AS( SELECT ID, TANUM, MATNR, DLFIMG FROM PRODUCTION_DN_DETAIL WHERE PARENT_ID = '{0}' AND MATNR = '{1}') ,CTE_B AS( SELECT A.TANUM, A.MATNR, A.DLFIMG, substr(B.BARCODE, -10) AS BARCODE FROM CTE_A A, PRODUCTION_DN_DETAIL_SCAN B WHERE A.ID = B.PARENT_ID ) SELECT B.* ,V.BATCH FROM VIEW_PRDIDMASTER V , CTE_B B WHERE V.PRDID = B.BARCODE "
               , request.ID
               , request.Matnr
                );

            return dal.ProductDeliveryScanNote_Search(sql);
        }

        public DynamicListResult ProductionPalletDetail_Search(PalletRequest request)
        {
            var sql = "SELECT CASE.FACTORY,CASE.LINE_NAME,CASE.MATNR,CASE.KTEXT,CASE.CHARG,CASE.BARCODE AS CASE_BARCODE,PALLET.BARCODE AS PALLET_BARCODE,CASE.CREATE_TIME,DETAIL.ID,PALLET.CREATE_TIME PALLET_CREATE_TIME FROM PRODUCTION_CASE CASE ,PRODUCTION_PALLET_DETAIL DETAIL ,PRODUCTION_PALLET PALLET WHERE rownum < 10 AND PALLET.ID = DETAIL.PALLET_ID AND DETAIL.BARCODE = CASE.BARCODE ";

            sql += OracleHelper.GetConditionByDateFromTo("CASE.CREATE_TIME", request.FromDate, request.ToDate);

            if (!string.IsNullOrEmpty(request.Factory))
                sql += $" AND CASE.FACTORY = '{request.Factory}' ";

            if (!string.IsNullOrEmpty(request.Factory))
                sql += $" AND CASE.LINE_NAME = '{request.LineName}' ";

            if (!string.IsNullOrEmpty(request.Matnr))
                sql += $" AND CASE.MATNR = '{request.Matnr}' ";

            if (!string.IsNullOrEmpty(request.Charg))
                sql += $" AND CASE.CHARG = '{request.Charg}' ";

            if (!string.IsNullOrEmpty(request.PalletBarcode))
                sql += $" AND PALLET.BARCODE = '{request.PalletBarcode}' ";


            return dal.ProductDeliveryScanNote_Search(sql);
        }
    }
}
