using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System.Collections;



/// <summary>
///Utils 的摘要说明
/// </summary>
public static class Utils
{
    public static string ToRecordJson(object obj, int count)
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
       // jss.RegisterConverters(new JavaScriptConverter[] { new DateTimeConverter() });
        string json = jss.Serialize(obj);
        return "{\"total\":" + count + ",\"rows\":" + json + "}";
    }
    public static string ToRecordJson(object obj, int count, string memberno)
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
       // jss.RegisterConverters(new JavaScriptConverter[] { new DateTimeConverter() });
        string json = jss.Serialize(obj);

        return "{\"total\":" + count + ",\"rows\":" + json + ",\"membernolast\":" + memberno + "}";
    }
    public static string ToRecordJson(object obj)
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
       // jss.RegisterConverters(new JavaScriptConverter[] { new DateTimeConverter() });
        string json = jss.Serialize(obj);
        
        return json;
    }

    public static T ToObject<T>(string json)
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        jss.RegisterConverters(new JavaScriptConverter[] { new DateTimeConverter() });
        return jss.Deserialize<T>(json);
    }

    /// <summary>
    /// 对象转换成json
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="jsonObject">需要格式化的对象</param>
    /// <returns>Json字符串</returns>
    public static string DataContractJsonSerialize<T>(T jsonObject)
    {
        var serializer = new DataContractJsonSerializer(typeof(T));
        string json = null;
        using (var ms = new MemoryStream()) //定义一个stream用来存发序列化之后的内容
        {
            serializer.WriteObject(ms, jsonObject);
            var dataBytes = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(dataBytes, 0, (int)ms.Length);
            json = Encoding.UTF8.GetString(dataBytes);
            ms.Close();
        }
        return json;
    }

    /// <summary>
    /// json字符串转换成对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="json">要转换成对象的json字符串</param>
    /// <returns></returns>
    public static T DataContractJsonDeserialize<T>(string json)
    {
        var serializer = new DataContractJsonSerializer(typeof(T));
        var obj = default(T);
        using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
        {
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
        }
        return obj;
    }

    //public List<object> DeSerialize<T>(string jsonStr)
    //{
    //    List<object> list = new List<object>(); 
    //    JsonTextParser jtp = new JsonTextParser();
    //    JsonArrayCollection jac = jtp.Parse(jsonStr) as JsonArrayCollection;
    //    T o = Activator.CreateInstance<T>(); 
    //    foreach (JsonObjectCollection joc in jac) 
    //    { 
    //        using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(joc.ToString()))) 
    //        { DataContractJsonSerializer serializer = new DataContractJsonSerializer(o.GetType()); 
    //            list.Add((T)serializer.ReadObject(ms)); 
    //        } 
    //    } 
    //    return list;
    //} 
}


public class DateTimeConverter : JavaScriptConverter
{
    public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
    {
        return new JavaScriptSerializer().ConvertToType(dictionary, type);
    }
    public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
    {
        if (!(obj is DateTime)) return null;
        return new CustomString(((DateTime)obj).ToString("yyyy-MM-dd hh:mm:ss"));
    }
    public override IEnumerable<Type> SupportedTypes
    {
        get { return new[] { typeof(DateTime) }; }
    }
    private class CustomString : Uri, IDictionary<string, object>
    {
        public CustomString(string str)
            : base(str, UriKind.Relative)
        {
        }
        void IDictionary<string, object>.Add(string key, object value)
        {
            throw new NotImplementedException();
        }
        bool IDictionary<string, object>.ContainsKey(string key)
        {
            throw new NotImplementedException();
        }
        ICollection<string> IDictionary<string, object>.Keys
        {
            get { throw new NotImplementedException(); }
        }
        bool IDictionary<string, object>.Remove(string key)
        {
            throw new NotImplementedException();
        }
        bool IDictionary<string, object>.TryGetValue(string key, out object value)
        {
            throw new NotImplementedException();
        }
        ICollection<object> IDictionary<string, object>.Values
        {
            get { throw new NotImplementedException(); }
        }
        object IDictionary<string, object>.this[string key]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
        {
            throw new NotImplementedException();
        }
        void ICollection<KeyValuePair<string, object>>.Clear()
        {
            throw new NotImplementedException();
        }
        bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
        {
            throw new NotImplementedException();
        }
        void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }
        int ICollection<KeyValuePair<string, object>>.Count
        {
            get { throw new NotImplementedException(); }
        }
        bool ICollection<KeyValuePair<string, object>>.IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }
        bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
        {
            throw new NotImplementedException();
        }
        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();

        }

    }

}
