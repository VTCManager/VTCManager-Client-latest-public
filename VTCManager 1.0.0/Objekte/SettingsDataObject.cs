using System;

namespace VTCManager_1._0._0
{
    [Serializable]
    public class SettingsDataObject
    {
        // Fields
        private string m_oSaveLoginData;
        private string m_oAccount;
        private string m_oPassword;

        // Properties
        public string SaveLoginData
        {
            get =>
                m_oSaveLoginData;
            set =>
                m_oSaveLoginData = value;
        }

        public string Account
        {
            get =>
                m_oAccount;
            set =>
                m_oAccount = value;
        }

        public string Password
        {
            get =>
                m_oPassword;
            set =>
                m_oPassword = value;
        }
    }



}
