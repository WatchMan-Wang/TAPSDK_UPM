using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TDSCommon;

namespace TapSDK
{
    public interface ITDSLicense
    {
        void SetLicencesCallback(TDSLicenseCallback callback);

        void SetDLCCallback(TDSDLCCallback callback);

        void SetDLCCallbackWithParams(TDSDLCCallback callback, bool checkOnce, string publicKey);

        void Check();

        void QueryDLC(string[] dlcList);

        void PurchaseDLC(string dlc);

    }

    public interface TDSLicenseCallback
    {
        void OnLicenseSuccess();
    }

    public interface TDSDLCCallback
    {
        bool OnQueryCallBack(int code, Dictionary<string, object> queryList);

        void OnOrderCallBack(string sku, int status);
    }


    public class TDSLicenseConstants
    {
        public static string TDS_LICENSE_SERVICE = "TapLicenseService";

        public static string TDS_LICENSE_SERVICE_CLZ = "com.taptap.pay.sdk.library.wrapper.TapLicenseService";

        public static string TDS_LICENSE_SERVICE_IMPL = "com.taptap.pay.sdk.library.wrapper.TapLicenseServiceImpl";
    }

}