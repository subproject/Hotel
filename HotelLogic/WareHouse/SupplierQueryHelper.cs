using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;

namespace HotelLogic.WareHouse
{
    public class SupplierQueryHelper
    {
        //read 
        public List<SupplierQueryDetailViewModel> ReadSupplierDetails(string id)
        {
            List<SupplierQueryDetailViewModel> result = new List<SupplierQueryDetailViewModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {

                int orderid = Convert.ToInt32(id);
                id = orderid.ToString();
                        var query2 = (from a in _db.WH_InOrder
                                      where a.Supplier == id
                                    select a).Distinct();

                        foreach (var qtemp in query2)
                        {
                            var query = from a in _db.WH_InOrderDetails
                                        join g in _db.WH_Goods  on a.ProductId  equals g.GoodsNo 
                                        where a.OrderID == qtemp.AutoID
                                        orderby a.AutoID descending
                                        select new
                                        {
                                            a.AutoID,
                                            a.Counts,
                                            a.InPrice,                                              
                                            g.GoodsName,
                                        };

                             

                            foreach (var q in query)
                            {
                                SupplierQueryDetailViewModel temp = new SupplierQueryDetailViewModel();
                                temp.ID = q.AutoID.ToString().PadLeft(8, '0');
                                temp.OrderNum = qtemp.OrderNum;
                                temp.Date = qtemp.Date.ToString();  //DateTime.Now.ToString("YYYY-MM-DD"); 
                                temp.OrderType = "商品进货";
                                temp.ProductName = q.GoodsName;
                                temp.Counts = q.Counts;
                                temp.InPrice = q.InPrice;
                                temp.TotalMoney = q.InPrice * q.Counts;

                                result.Add(temp);
                            }
                        }
                   
                    
                
            }
            return result;
        }
        public List<SupplierQueryViewModel> ReadSupplier(DateTime starttime, DateTime endtime)
        {
            List<SupplierQueryViewModel> result = new List<SupplierQueryViewModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var query2 = (from a in _db.WH_InOrder
                             where System.Data.Objects.EntityFunctions.DiffMinutes(starttime, a.Date) >= 0
                              && System.Data.Objects.EntityFunctions.DiffMinutes(endtime, a.Date) <= 0
                             select a.Supplier).Distinct();
                             //group a by a.Supplier into g
                             
             
           
                foreach (var q2 in query2)
                {
                    if (!string.IsNullOrEmpty(q2))
                    {
                        int i = Convert.ToInt32(q2);
                        var query = from a in _db.WH_Supplier
                                    where a.AutoID == i
                                    select a;
                        foreach (var q in query)
                        {
                            SupplierQueryViewModel temp = new SupplierQueryViewModel();
                          
                            temp.ID = q.AutoID.ToString().PadLeft(8,'0');
                            temp.SupplierName = q.SupplierName;
                            temp.Address = q.Address;
                            temp.PostCode = q.PostCode;
                            temp.SupplierSimple = q.SupplierSimple;
                            temp.Tel = q.Tel;
                            temp.InGoodsTimes = q.InGoodsTimes;
                            temp.OutGoodsTimes = q.OutGoodsTimes;
                            temp.TotalMoney = q.TotalMoney;
                            result.Add(temp);
                        }
                    }
                }
            }
            return result;
        }

        ////read part
        //public List<SupplierViewModel> ReadPartSupplier(int page, int rows)
        //{
        //    List<SupplierViewModel> result = new List<SupplierViewModel>();
        //    using (HotelDBEntities _db = new HotelDBEntities())
        //    {
        //        var query = (from a in _db.WH_Supplier
        //                     orderby a.AutoID descending
        //                    select a).Skip((page-1)*rows).Take(rows);

        //        foreach (var q in query)
        //        {
        //            SupplierViewModel temp = new SupplierViewModel();
        //            temp.ID = q.AutoID;
        //            temp.SupplierName = q.SupplierName;
        //            temp.Address = q.Address;
        //            temp.PostCode = q.PostCode;
        //            temp.SupplierSimple = q.SupplierSimple;
        //            temp.Tel = q.Tel;
        //            result.Add(temp);
        //        }
        //    }
        //    return result;
        //}

        ////create
        //public string CreateSupplier(SupplierViewModel supplier)
        //{
        //    string result = string.Empty;
        //    using (HotelDBEntities db = new HotelDBEntities())
        //    {
        //        try
        //        {
        //            WH_Supplier temp = new WH_Supplier();
        //            temp.SupplierName = supplier.SupplierName;
        //            temp.Address = supplier.Address;
        //            temp.PostCode = supplier.PostCode;
        //            temp.SupplierSimple = supplier.SupplierSimple;
        //            temp.Tel = supplier.Tel;
        //            db.WH_Supplier.AddObject(temp);
        //            db.SaveChanges();
        //            result = "0";
        //        }
        //        catch (Exception e)
        //        {
        //            result = "-1";
        //        }
        //        return result;
        //    }
        //}

        ////update
        //public string UpdateSupplier(SupplierViewModel supplier)
        //{
        //    string result = string.Empty;
        //    using (HotelDBEntities db = new HotelDBEntities())
        //    {
        //        try
        //        {
        //            var temp = db.WH_Supplier.Where(a => a.AutoID == supplier.ID).SingleOrDefault();
        //            if (temp != null)
        //            {
        //                temp.SupplierName = supplier.SupplierName;
        //                temp.Address = supplier.Address;
        //                temp.PostCode = supplier.PostCode;
        //                temp.SupplierSimple = supplier.SupplierSimple;
        //                temp.Tel = supplier.Tel; 
        //            }
        //            db.SaveChanges();
        //            result = "0";
        //        }
        //        catch (Exception e)
        //        {
        //            result = "-1";
        //        }
        //    }
        //    return result;
        //}

        ////delete
        //public string DeleteSupplier(Int32 supplierID)
        //{
        //    string result = string.Empty;
        //    using (HotelDBEntities db = new HotelDBEntities())
        //    {
        //        try
        //        {
        //            var temp = db.WH_Supplier.Where(a => a.AutoID == supplierID).SingleOrDefault();
        //            if (temp != null)
        //            {
        //                db.WH_Supplier.DeleteObject(temp);
        //            }
        //            db.SaveChanges();
        //            result = "0";
        //        }
        //        catch (Exception e)
        //        {
        //            result = "-1";
        //        }
        //    }
        //    return result;
        //}
    }
    public class SupplierQueryDetailViewModel 
    {
        public string ID { get; set; }
        public string OrderNum { get; set; }
        public string Date { get; set; }
        public string OrderType { get; set; }
        public string ProductName { get; set; }
        public int? Counts { get; set; }
        public decimal? InPrice { get; set; }
        public decimal? TotalMoney { get; set; }
        
    }

    public class SupplierQueryViewModel
    {
        public string AutoID { get; set; }
        public string ID { get; set; }
        public string SupplierName { get; set; }
        public string SupplierSimple { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Tel { get; set; }
        public int? InGoodsTimes { get; set; }
        public int? OutGoodsTimes { get; set; }
        public decimal? TotalMoney { get; set; }
    }
}
