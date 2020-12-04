using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TDSCommon;

namespace TDSLogin
{
    public class TDSLoginImpl : ITDSLogin
    {

        private TDSLoginImpl()
        {
            EngineBridge.GetInstance().Register(TDSLoginConstants.TDS_LOGIN_SERVICE_CLZ, TDSLoginConstants.TDS_LOGIN_SERVICE_IMPL);
        }

        private volatile static TDSLoginImpl sInstance;

        private static readonly object locker = new object();

        public static TDSLoginImpl GetInstance()
        {
            lock (locker)
            {
                if (sInstance == null)
                {
                    sInstance = new TDSLoginImpl();
                }
            }
            return sInstance;
        }

        public void Init(string clientId)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("clientID", clientId);
            Command command = new Command(TDSLoginConstants.TDS_LOGIN_SERVICE, "initWithClientID", true, null, dic);
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void Init(string clientId, bool isCN, bool roundCorner)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("clientID", clientId);
            dic.Add("regionType", isCN);
            dic.Add("roundCorner", roundCorner);
            Command command = new Command(TDSLoginConstants.TDS_LOGIN_SERVICE, "initWithClientID", true, null, dic);
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void ChangeConfig(bool isCN, bool roundCorner)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("regionType", isCN);
            dic.Add("roundCorner", roundCorner);
            Command command = new Command(TDSLoginConstants.TDS_LOGIN_SERVICE, "changeConfig", true, null, dic);
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void StartLogin(string[] permissions)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("permissons", permissions);
            Command command = new Command(TDSLoginConstants.TDS_LOGIN_SERVICE, "startTapLogin", true, null, dic);
            TDSCommon.EngineBridge.GetInstance().CallHandler(command);
        }

        public void RegisterLoginCallback(LoginCallback callback)
        {
            Command command = new Command(TDSLoginConstants.TDS_LOGIN_SERVICE, "registerLoginCallback", true, null, null);
            TDSCommon.EngineBridge.GetInstance().CallHandler(command, (result) =>
            {
                Debug.Log("loginCallback:" + result.toJSON());
                if (result.code != Result.RESULT_SUCCESS)
                {
                    callback.LoginError(result.message);
                    return;
                }

                if (string.IsNullOrEmpty(result.content))
                {
                    callback.LoginError(result.message);
                    return;
                }

                LoginWrapperBean<string> wrapperBean = new LoginWrapperBean<string>(result.content);
                if (wrapperBean.loginCallbackCode == 0)
                {
                    TDSAccessToken accessToken = new TDSAccessToken(wrapperBean.wrapper);
                    callback.LoginSuccess(accessToken);
                    return;
                }

                if (wrapperBean.loginCallbackCode == 1)
                {
                    callback.LoginCancel();
                    return;
                }
                callback.LoginError(wrapperBean.wrapper);
            });
        }

        public void GetCurrentAccessToken(Action<TDSAccessToken> callback)
        {
            Command command = new Command(TDSLoginConstants.TDS_LOGIN_SERVICE, "currentAccessToken", true, null, null);
            EngineBridge.GetInstance().CallHandler(command, (result) =>
            {
                Debug.Log("accessToken:" + result.toJSON());
                if (result.code != Result.RESULT_SUCCESS)
                {
                    return;
                }

                if (string.IsNullOrEmpty(result.content))
                {
                    return;
                }

                TDSAccessToken accessToken = new TDSAccessToken(result.content);
                callback(accessToken);
            });
        }

        public void GetCurrentProfile(Action<TDSLoginProfile> callback)
        {
            Command command = new Command(TDSLoginConstants.TDS_LOGIN_SERVICE, "currentProfile", true, null, null);
            EngineBridge.GetInstance().CallHandler(command, (result) =>
            {
                Debug.Log("currentProfile:" + result.toJSON());
                if (result.code != Result.RESULT_SUCCESS)
                {
                    return;
                }

                if (string.IsNullOrEmpty(result.content))
                {
                    return;
                }

                TDSLoginProfile profile = new TDSLoginProfile(result.content);
                callback(profile);
            });
        }

        public void FetchProfileForCurrentAccessToken(Action<TDSLoginProfile> profileCallback, Action<string> errorCallback)
        {
            Command command = new Command(TDSLoginConstants.TDS_LOGIN_SERVICE, "fetchProfileForCurrentAccessToken", true, null, null);
            EngineBridge.GetInstance().CallHandler(command, (result) =>
            {
                Debug.Log("currentProfile:" + result.toJSON());
                if (result.code != Result.RESULT_SUCCESS)
                {
                    errorCallback(result.message);
                    return;
                }

                if (string.IsNullOrEmpty(result.content))
                {
                    errorCallback(result.message);
                    return;
                }
                LoginWrapperBean<string> wrapperBean = new LoginWrapperBean<string>();
                if (wrapperBean.loginCallbackCode == 0)
                {
                    profileCallback(new TDSLoginProfile(wrapperBean.wrapper));
                    return;
                }
                errorCallback(wrapperBean.wrapper);
            });
        }
        public void Logout()
        {
            Command command = new Command(TDSLoginConstants.TDS_LOGIN_SERVICE, "logout", false, null, null);
            EngineBridge.GetInstance().CallHandler(command);
        }

    }
}