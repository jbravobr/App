using System;

namespace IcatuzinhoApp
{
    public class DeveloperSettings : EntityBase
    {
        public bool IsEmulatorDebugEnabledForConnectivity { get; private set; }

        public bool ConnectivityStatus { get; private set;}

        public bool IsTestCloudEnabled { get; private set; }

        public bool ResetDefaultDatabase { get; private set; }

        public void EnableEmulatorDebug(bool connStatus)
        {
            IsEmulatorDebugEnabledForConnectivity = true;
            ConnectivityStatus = connStatus;
        }

        public void EnableTestCloud()
        {
            IsTestCloudEnabled = true;
        }

        public void EnableResetDefaultDatabase()
        {
            ResetDefaultDatabase = true;
        }
    }
}

