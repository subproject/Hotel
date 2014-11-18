using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;
namespace HotelLogic
{
    public static class Utils
    {
        /// <summary>
        /// 对象转jason
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string ToJsJson(object item)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(item.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, item);
                StringBuilder sb = new StringBuilder();
                sb.Append(Encoding.UTF8.GetString(ms.ToArray()));
                return sb.ToString();
            }
        }
        /// <summary>
        /// json转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T FromJsonTo<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                T jsonObject = (T)ser.ReadObject(ms);
                return jsonObject;
            }
        }

        /// <summary>  
        /// 汉字转拼音缩写  
        /// </summary>  
        /// <param name="str">要转换的汉字字符串</param>  
        /// <returns>拼音缩写</returns>  
        public static string GetPYString(string str)
        {
            str = str.Replace(" ", "");
            string tempStr = "";
            foreach (char c in str)
            {
                if ((int)c >= 33 && (int)c <= 126)
                {//字母和符号原样保留  
                    tempStr += c.ToString();
                }
                else
                {//累加拼音声母  
                    tempStr += GetPYChar(c.ToString());
                }
            }
            return tempStr;
        }
        /// <summary>  
        /// 取单个字符的拼音声母  
        /// Code By   
        /// 2004-11-30  
        /// </summary>  
        /// <param name="c">要转换的单个汉字</param>  
        /// <returns>拼音声母</returns>  
        public static string GetPYChar(string c)
        {
            byte[] array = new byte[2];
            array = System.Text.Encoding.Default.GetBytes(c);
            int i = (short)(array[0] - '\0') * 256 + ((short)(array[1] - '\0'));
            if (i < 0xB0A1) return "*";
            if (i < 0xB0C5) return "a";
            if (i < 0xB2C1) return "b";
            if (i < 0xB4EE) return "c";
            if (i < 0xB6EA) return "d";
            if (i < 0xB7A2) return "e";
            if (i < 0xB8C1) return "f";
            if (i < 0xB9FE) return "g";
            if (i < 0xBBF7) return "h";
            if (i < 0xBFA6) return "j";
            if (i < 0xC0AC) return "k";
            if (i < 0xC2E8) return "l";
            if (i < 0xC4C3) return "m";
            if (i < 0xC5B6) return "n";
            if (i < 0xC5BE) return "o";
            if (i < 0xC6DA) return "p";
            if (i < 0xC8BB) return "q";
            if (i < 0xC8F6) return "r";
            if (i < 0xCBFA) return "s";
            if (i < 0xCDDA) return "t";
            if (i < 0xCEF4) return "w";
            if (i < 0xD1B9) return "x";
            if (i < 0xD4D1) return "y";
            if (i < 0xD7FA) return "z";
            return "*";
        }
        /// <summary>
        /// 分页函数反序列化
        /// </summary>
        /// <param name="jsonPageParams"></param>
        /// <returns></returns>
        public static  HotelLogic.CommonModel.PageParams GetPageInfoFromJsonPage(string jsonPageParams)
        {
            var f = new System.Web.Script.Serialization.JavaScriptSerializer().DeserializeObject(jsonPageParams);

            HotelLogic.CommonModel.PageParams page = new HotelLogic.CommonModel.PageParams();
            foreach (var item in (Dictionary<string, object>)f)
            {
                switch (item.Key)
                {
                    case "sortField": page.SortField = item.Value.ToString(); break;
                    case "sortDirection": page.SortDirection = int.Parse(item.Value.ToString()); break;
                    case "pageNumber": page.PageNumber = int.Parse(item.Value.ToString()); break;
                    case "pageSize": page.PageSize = int.Parse(item.Value.ToString()); break;
                }
            }
            return page;
        }
       
    }
}
