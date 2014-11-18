using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;

namespace HotelLogic.WareHouse
{
    public class SupplierHelper
    {
        //read
        public List<SupplierViewModel> ReadSupplier()
        {
            List<SupplierViewModel> result = new List<SupplierViewModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var query = from a in _db.WH_Supplier
                            select a;
                foreach (var q in query)
                {
                    SupplierViewModel temp = new SupplierViewModel();
                    temp.ID = q.AutoID;
                    temp.SupplierName = q.SupplierName;
                    temp.Address = q.Address;
                    temp.PostCode = q.PostCode;
                    temp.SupplierSimple = q.SupplierSimple;
                    temp.Tel = q.Tel;
                    result.Add(temp);
                }
            }
            return result;
        }

        //read part
        public List<SupplierViewModel> ReadPartSupplier(int page, int rows)
        {
            List<SupplierViewModel> result = new List<SupplierViewModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var query = (from a in _db.WH_Supplier
                             orderby a.AutoID descending
                            select a).Skip((page-1)*rows).Take(rows);

                foreach (var q in query)
                {
                    SupplierViewModel temp = new SupplierViewModel();
                    temp.ID = q.AutoID;
                    temp.SupplierName = q.SupplierName;
                    temp.Address = q.Address;
                    temp.PostCode = q.PostCode;
                    temp.SupplierSimple = q.SupplierSimple;
                    temp.Tel = q.Tel;
                    result.Add(temp);
                }
            }
            return result;
        }

        //create
        public string CreateSupplier(SupplierViewModel supplier)
        {
            string result = string.Empty;
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                    WH_Supplier temp = new WH_Supplier();
                    temp.SupplierName = supplier.SupplierName;
                    temp.Address = supplier.Address;
                    temp.PostCode = supplier.PostCode;
                    temp.SupplierSimple = supplier.SupplierSimple;
                    temp.Tel = supplier.Tel;
                    db.WH_Supplier.AddObject(temp);
                    db.SaveChanges();
                    result = "0";
                }
                catch (Exception e)
                {
                    result = "-1";
                }
                return result;
            }
        }

        //update
        public string UpdateSupplier(SupplierViewModel supplier)
        {
            string result = string.Empty;
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                    var temp = db.WH_Supplier.Where(a => a.AutoID == supplier.ID).SingleOrDefault();
                    if (temp != null)
                    {
                        temp.SupplierName = supplier.SupplierName;
                        temp.Address = supplier.Address;
                        temp.PostCode = supplier.PostCode;
                        temp.SupplierSimple = supplier.SupplierSimple;
                        temp.Tel = supplier.Tel;

                      
                    }
                    db.SaveChanges();
                    result = "0";
                }
                catch (Exception e)
                {
                    result = "-1";
                }
            }
            return result;
        }

        public string UpdateSupplier2(int supplierid,decimal? money)
        {
            string result = string.Empty;//SupplierViewModel
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                    var temp = db.WH_Supplier.Where(a => a.AutoID == supplierid).SingleOrDefault();
                    if (temp != null)
                    {
                        temp.InGoodsTimes = temp.InGoodsTimes == null ? 0 : temp.InGoodsTimes;
                        temp.InGoodsTimes +=temp.InGoodsTimes+ 1;
                        temp.TotalMoney = temp.TotalMoney == null ? 0 : temp.TotalMoney;
                        temp.TotalMoney += money;
                    }
                    db.SaveChanges();
                    result = "0";
                }
                catch (Exception e)
                {
                    result = "-1";
                }
            }
            return result;
        }

        public string UpdateSupplierTuihui(int supplierid, decimal? money)
        {
            string result = string.Empty;//SupplierViewModel
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                    var temp = db.WH_Supplier.Where(a => a.AutoID == supplierid).SingleOrDefault();
                    if (temp != null)
                    {
                        temp.OutGoodsTimes = temp.OutGoodsTimes == null ? 0 : temp.OutGoodsTimes;
                        temp.OutGoodsTimes += 1;

                        //temp.TotalMoney -= money;
                    }
                    db.SaveChanges();
                    result = "0";
                }
                catch (Exception e)
                {
                    result = "-1";
                }
            }
            return result;
        }

        //delete
        public string DeleteSupplier(Int32 supplierID)
        {
            string result = string.Empty;
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                    var temp = db.WH_Supplier.Where(a => a.AutoID == supplierID).SingleOrDefault();
                    if (temp != null)
                    {
                        db.WH_Supplier.DeleteObject(temp);
                    }
                    db.SaveChanges();
                    result = "0";
                }
                catch (Exception e)
                {
                    result = "-1";
                }
            }
            return result;
        }
    }
    public class SupplierViewModel
    {
        public int ID { get; set; }
        public string SupplierName { get; set; }
        public string SupplierSimple { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Tel { get; set; }

        public int? InGoodsTimes { get; set; }
        public int? OutGoodsTimes { get; set; }
        public decimal? TotalMoney { get; set; }
    }

    public class SupplierGoodsViewModel
    {
        public string GoodsName { get; set; }
        public string GoodsStyle { get; set; }
        public string Unit { get; set; }
        public decimal? Price { get; set; }
        public int Counts { get; set; }
        public int TotalInCounts { get; set; }
        public int TotalAllocationCounts { get; set; }
    }
}
