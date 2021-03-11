using TDSCommon;

namespace TapSDK
{
    public class TDSLicense
    {
        public static void SetLicencesCallback(TDSLicenseCallback callback)
        {
            TDSLicenseImpl.GetInstance().SetLicencesCallback(callback);
        }

        public static void SetDLCCallback(TDSDLCCallback callback)
        {
            TDSLicenseImpl.GetInstance().SetDLCCallback(callback);
        }

        public static void SetDLCCallbackWithParams(TDSDLCCallback callback, bool checkOnce, string publicKey)
        {
            TDSLicenseImpl.GetInstance().SetDLCCallbackWithParams(callback, checkOnce, publicKey);
        }

        public static void Check()
        {
            TDSLicenseImpl.GetInstance().Check();
        }

        public static void QueryDLC(string[] dlcList)
        {
            TDSLicenseImpl.GetInstance().QueryDLC(dlcList);
        }

        public static void PurchaseDLC(string dlc)
        {
            TDSLicenseImpl.GetInstance().PurchaseDLC(dlc);
        }
    }
}