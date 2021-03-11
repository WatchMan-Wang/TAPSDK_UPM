using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TDSCommon
{
    public interface ITDSCommon
    {
        void SetLanguage(string language);

        void GetRegionCode(Action<bool> callback);

        void IsTapTapInstalled(Action<bool> callback);

        void IsTapTapGlobalInstalled(Action<bool> callback);

        void UpdateGameInTapTap(string appId, Action<bool> callback);

        void UpdateGameInTapGlobal(string appId, Action<bool> callback);

        void OpenReviewInTapTap(string appId, Action<bool> callback);

        void OpenReviewInTapTapGlobal(string appId, Action<bool> callback);

    }

    public class TDSCommonConstants
    {
        public static string TDS_COMMON_SERVICE_NAME = "TDSCommonService";

        public static string TDS_COMMON_SERVICE_CLZ = "com.tds.common.wrapper.TDSCommonService";

        public static string TDS_COMMON_SERVICE_IMPL = "com.tds.common.wrapper.TDSCommonServiceImpl";
    }

    [Serializable]
    public class CommonRegionWrapper
    {
        public bool isMainland;

        public CommonRegionWrapper(string json)
        {
            Dictionary<string, object> dic = Json.Deserialize(json) as Dictionary<string, object>;
            this.isMainland = SafeDictionary.GetValue<bool>(dic, "isMainland");
        }

    }

    [Serializable]
    public class BridgeBooleanWrapper
    {
        public static bool GetBoolFromDic(string key, string json)
        {
            Dictionary<string, object> dic = Json.Deserialize(json) as Dictionary<string, object>;
#if UNITY_IOS
                return SafeDictionary.GetValue<bool>(dic,key);
#endif
            return SafeDictionary.GetValue<int>(dic, key) == 1;
        }
    }

}