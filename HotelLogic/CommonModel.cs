using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelLogic.CommonModel
{
    public class PageParams
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int SortDirection { get; set; }
        public string SortField { get; set; }
    }

    public class ResultMode
    {
        public string Where { get; set; }
        public bool Flag { get; set; }
        public string Message { get; set; }
        public object value { get; set; }
        public virtual void Save()
        { 
        
        }
    }
    public enum SQLAction
    { 
       Add=0,
       Update=1,
       Delete=2
    }

}
