using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PlugIn
{
    [ProgId("PlugInCard")]//控件名称
    [Guid("1F1CF1EE-AE97-7E7C-4D21-2E4463D0E1C6")]//控件的GUID，用于COM注册和HTML中Object对象classid引用
    public class Card:IObjectSafety
    {
         public string ReadCardNo()
         {
             return "4859-0934-2398";
         }

         #region IObjectSafety 成员

         public void GetInterfacceSafyOptions(int riid, out int pdwSupportedOptions, out int pdwEnabledOptions)
         {
             pdwSupportedOptions = 1;
             pdwEnabledOptions = 2;
         }
         public void SetInterfaceSafetyOptions(int riid, int dwOptionsSetMask, int dwEnabledOptions)
         {
             throw new NotImplementedException();
         }

         private const string _IID_IDispatch = "{00020400-0000-0000-C000-000000000046}";
         private const string _IID_IDispatchEx = "{a6ef9860-c720-11d0-9337-00a0c90dcaa9}";
         private const string _IID_IPersistStorage = "{0000010A-0000-0000-C000-000000000046}";
         private const string _IID_IPersistStream = "{00000109-0000-0000-C000-000000000046}";
         private const string _IID_IPersistPropertyBag = "{37D84F60-42CB-11CE-8135-00AA004BB851}";

         private const int INTERFACESAFE_FOR_UNTRUSTED_CALLER = 0x00000001;
         private const int INTERFACESAFE_FOR_UNTRUSTED_DATA = 0x00000002;
         private const int S_OK = 0;
         private const int E_FAIL = unchecked((int)0x80004005);
         private const int E_NOINTERFACE = unchecked((int)0x80004002);

         private bool _fSafeForScripting = true;
         private bool _fSafeForInitializing = true;

         public int GetInterfaceSafetyOptions(ref Guid riid, ref int pdwSupportedOptions, ref int pdwEnabledOptions)
         {
             int Rslt = E_FAIL;

             string strGUID = riid.ToString("B");
             pdwSupportedOptions = INTERFACESAFE_FOR_UNTRUSTED_CALLER | INTERFACESAFE_FOR_UNTRUSTED_DATA;
             switch (strGUID)
             {
                 case _IID_IDispatch:
                 case _IID_IDispatchEx:
                     Rslt = S_OK;
                     pdwEnabledOptions = 0;
                     if (_fSafeForScripting == true)
                         pdwEnabledOptions = INTERFACESAFE_FOR_UNTRUSTED_CALLER;
                     break;
                 case _IID_IPersistStorage:
                 case _IID_IPersistStream:
                 case _IID_IPersistPropertyBag:
                     Rslt = S_OK;
                     pdwEnabledOptions = 0;
                     if (_fSafeForInitializing == true)
                         pdwEnabledOptions = INTERFACESAFE_FOR_UNTRUSTED_DATA;
                     break;
                 default:
                     Rslt = E_NOINTERFACE;
                     break;
             }

             return Rslt;
         }

         public int SetInterfaceSafetyOptions(ref Guid riid, int dwOptionSetMask, int dwEnabledOptions)
         {
             int Rslt = E_FAIL;

             string strGUID = riid.ToString("B");
             switch (strGUID)
             {
                 case _IID_IDispatch:
                 case _IID_IDispatchEx:
                     if (((dwEnabledOptions & dwOptionSetMask) == INTERFACESAFE_FOR_UNTRUSTED_CALLER) &&
                          (_fSafeForScripting == true))
                         Rslt = S_OK;
                     break;
                 case _IID_IPersistStorage:
                 case _IID_IPersistStream:
                 case _IID_IPersistPropertyBag:
                     if (((dwEnabledOptions & dwOptionSetMask) == INTERFACESAFE_FOR_UNTRUSTED_DATA) &&
                          (_fSafeForInitializing == true))
                         Rslt = S_OK;
                     break;
                 default:
                     Rslt = E_NOINTERFACE;
                     break;
             }

             return Rslt;
         }

         #endregion
    }
}
