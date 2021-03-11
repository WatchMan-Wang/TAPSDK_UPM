using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TDSCommon
{
    public class TDSCommonImpl : ITDSCommon
    {
        private static TDSCommonImpl sInstance = new TDSCommonImpl();

        public static TDSCommonImpl GetInstance()
        {
            return sInstance;
        }

        private TDSCommonImpl()
        {
            EngineBridge.GetInstance().Register(TDSCommonConstants.TDS_COMMON_SERVICE_CLZ, TDSCommonConstants.TDS_COMMON_SERVICE_IMPL);
        }

        public void SetLanguage(string language)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("language", language);
            Command command = new Command(TDSCommonConstants.TDS_COMMON_SERVICE_CLZ, "setLanguage", false, dic);
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void GetRegionCode(Action<bool> callback)
        {
            Command command = new Command(TDSCommonConstants.TDS_COMMON_SERVICE_CLZ, "getRegionCode", true, null);
            EngineBridge.GetInstance().CallHandler(command, (result) =>
            {
                if (result.code != Result.RESULT_SUCCESS)
                {
                    callback(false);
                    return;
                }

                if (string.IsNullOrEmpty(result.content))
                {
                    callback(false);
                    return;
                }

                callback(BridgeBooleanWrapper.GetBoolFromDic("isMainland", result.content));
            });

        }

        public void IsTapTapInstalled(Action<bool> callback)
        {
            Command command = new Command(TDSCommonConstants.TDS_COMMON_SERVICE_CLZ, "isTapTapInstalled", true, null);
            EngineBridge.GetInstance().CallHandler(command, (result) =>
             {
                 if (result.code != Result.RESULT_SUCCESS)
                 {
                     callback(false);
                     return;
                 }

                 if (string.IsNullOrEmpty(result.content))
                 {
                     callback(false);
                     return;
                 }

                 callback(BridgeBooleanWrapper.GetBoolFromDic("isTapTapInstalled", result.content));
             });
        }

        public void IsTapTapGlobalInstalled(Action<bool> callback)
        {
            Command command = new Command(TDSCommonConstants.TDS_COMMON_SERVICE_CLZ, "isTapGlobalInstalled", true, null);
            EngineBridge.GetInstance().CallHandler(command, (result) =>
             {
                 if (result.code != Result.RESULT_SUCCESS)
                 {
                     callback(false);
                     return;
                 }

                 if (string.IsNullOrEmpty(result.content))
                 {
                     callback(false);
                     return;
                 }

                 callback(BridgeBooleanWrapper.GetBoolFromDic("isTapGlobalInstalled", result.content));
             });
        }
        public void UpdateGameInTapTap(string appId, Action<bool> callback)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("appId", appId);
            Command command = new Command(TDSCommonConstants.TDS_COMMON_SERVICE_CLZ, "updateGameInTapTap", true, dic);
            EngineBridge.GetInstance().CallHandler(command, (result) =>
             {
                 if (result.code != Result.RESULT_SUCCESS)
                 {
                     callback(false);
                     return;
                 }

                 if (string.IsNullOrEmpty(result.content))
                 {
                     callback(false);
                     return;
                 }

                 callback(BridgeBooleanWrapper.GetBoolFromDic("updateGameInTapTap", result.content));
             });
        }

        public void UpdateGameInTapGlobal(string appId, Action<bool> callback)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("appId", appId);
            Command command = new Command(TDSCommonConstants.TDS_COMMON_SERVICE_CLZ, "updateGameInTapGlobal", true, dic);
            EngineBridge.GetInstance().CallHandler(command, (result) =>
             {
                 if (result.code != Result.RESULT_SUCCESS)
                 {
                     callback(false);
                     return;
                 }

                 if (string.IsNullOrEmpty(result.content))
                 {
                     callback(false);
                     return;
                 }

                 callback(BridgeBooleanWrapper.GetBoolFromDic("updateGameInTapGlobal", result.content));
             });
        }

        public void OpenReviewInTapTap(string appId, Action<bool> callback)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("appId", appId);
            Command command = new Command(TDSCommonConstants.TDS_COMMON_SERVICE_CLZ, "openReviewInTapTap", true, dic);
            EngineBridge.GetInstance().CallHandler(command, (result) =>
             {
                 if (result.code != Result.RESULT_SUCCESS)
                 {
                     callback(false);
                     return;
                 }

                 if (string.IsNullOrEmpty(result.content))
                 {
                     callback(false);
                     return;
                 }

                 callback(BridgeBooleanWrapper.GetBoolFromDic("openReviewInTapTap", result.content));
             });
        }

        public void OpenReviewInTapTapGlobal(string appId, Action<bool> callback)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("appId", appId);
            Command command = new Command(TDSCommonConstants.TDS_COMMON_SERVICE_CLZ, "openReviewInTapGlobal", true, dic);
            EngineBridge.GetInstance().CallHandler(command, (result) =>
             {
                 if (result.code != Result.RESULT_SUCCESS)
                 {
                     callback(false);
                     return;
                 }

                 if (string.IsNullOrEmpty(result.content))
                 {
                     callback(false);
                     return;
                 }

                 callback(BridgeBooleanWrapper.GetBoolFromDic("openReviewInTapGlobal", result.content));
             });
        }
    }

}