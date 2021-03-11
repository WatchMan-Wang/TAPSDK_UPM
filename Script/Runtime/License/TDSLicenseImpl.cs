using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TDSCommon;

namespace TapSDK
{
    public class TDSLicenseImpl : ITDSLicense
    {
        private TDSLicenseImpl()
        {
            EngineBridge.GetInstance().Register(TDSLicenseConstants.TDS_LICENSE_SERVICE_CLZ, TDSLicenseConstants.TDS_LICENSE_SERVICE_IMPL);
        }

        private volatile static TDSLicenseImpl sInstance;

        private static readonly object locker = new object();

        public static TDSLicenseImpl GetInstance()
        {
            lock (locker)
            {
                if (sInstance == null)
                {
                    sInstance = new TDSLicenseImpl();
                }
            }
            return sInstance;
        }

        public void SetLicencesCallback(TDSLicenseCallback callback)
        {
            Command command = new Command.Builder()
                .Service(TDSLicenseConstants.TDS_LICENSE_SERVICE)
                .Method("setLicenseCallback")
                .Callback(true)
                .CommandBuilder();
                
            EngineBridge.GetInstance().CallHandler(command, (result) =>
             {
                 Debug.Log("result:" + result.toJSON());
                 if (result.code != Result.RESULT_SUCCESS)
                 {
                     return;
                 }

                 if (string.IsNullOrEmpty(result.content))
                 {
                     return;
                 }

                 Dictionary<string, object> dic = Json.Deserialize(result.content) as Dictionary<string, object>;
                 string success = SafeDictionary.GetValue<string>(dic, "login") as string;

                 if (success.Equals("success"))
                 {
                     callback.OnLicenseSuccess();
                 }
             });
        }

        public void SetDLCCallback(TDSDLCCallback callback)
        {
            SetDLCCallbackWithParams(callback, false, null);
        }

        public void SetDLCCallbackWithParams(TDSDLCCallback callback, bool checkOnce, string publicKey)
        {
            Command command = new Command.Builder()
                .Service(TDSLicenseConstants.TDS_LICENSE_SERVICE)
                .Method("setDLCCallbackWithParams")
                .Callback(true)
                .Args("checkOnce", checkOnce)
                .Args("publicKey", publicKey)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command, (result) =>
             {
                 Debug.Log("result:" + result.toJSON());
                 if (result.code != Result.RESULT_SUCCESS)
                 {
                     return;
                 }

                 if (string.IsNullOrEmpty(result.content))
                 {
                     return;
                 }

                 Dictionary<string, object> dic = Json.Deserialize(result.content) as Dictionary<string, object>;

                 string dlc = SafeDictionary.GetValue<string>(dic, "orderDLC") as string;
                 if (!string.IsNullOrEmpty(dlc))
                 {
                     int statusCode = SafeDictionary.GetValue<int>(dic, "orderStatus");
                     callback.OnOrderCallBack(dlc, statusCode);
                     return;
                 }

                 int code = SafeDictionary.GetValue<int>(dic, "queryCode");
                 string queryListJson = SafeDictionary.GetValue<string>(dic, "queryResult");

                 Dictionary<string, object> quertListDic = Json.Deserialize(queryListJson) as Dictionary<string, object>;

                 callback.OnQueryCallBack(code, quertListDic);
             });
        }

        public void Check()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            Command command = new Command(TDSLicenseConstants.TDS_LICENSE_SERVICE, "check", true, dic);
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void QueryDLC(string[] dlcList)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("dlcList", dlcList);
            Command command = new Command(TDSLicenseConstants.TDS_LICENSE_SERVICE, "queryDLC", true, dic);
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void PurchaseDLC(string dlc)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("dlc", dlc);
            Command command = new Command(TDSLicenseConstants.TDS_LICENSE_SERVICE, "purchaseDLC", true, dic);
            EngineBridge.GetInstance().CallHandler(command);
        }


    }
}