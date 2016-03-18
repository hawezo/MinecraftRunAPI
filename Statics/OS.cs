using System;

namespace MinecraftLaunching
{

    /// <summary>
    /// Provides some methods in link with the OS.
    /// </summary>
    class OS
    {

        /// <summary>
        /// Get the current OS name in function of its version
        /// </summary>
        /// <returns></returns>
        public static string GetOSName()
        {
            OperatingSystem OS = Environment.OSVersion;
            string osName = "Unknown";
            
            switch (OS.Platform)
            {
                case PlatformID.Win32NT:
                    switch (OS.Version.Major)
                    {
                        case 4:
                            switch (OS.Version.Revision)
                            {
                                case 950:
                                    osName = "Windows 95";
                                    break;

                                case 1381:
                                    osName = "Windows NT 4.0";
                                    break;

                                case 1998:
                                    osName = "Windows 98";
                                    break;

                                case 3000:
                                    osName = "Windows Me";
                                    break;
                            }
                            break;

                        case 5:
                            switch (OS.Version.Minor)
                            {
                                case 0:
                                    osName = "Windows 2000";
                                    break;

                                case 1:
                                    osName = "Windows XP";
                                    break;
                            }
                            break;

                        case 6:
                            switch (OS.Version.Minor)
                            {
                                case 0:
                                    switch (OS.Version.Revision)
                                    {
                                        case 6000:
                                            osName = "Vista";
                                            break;

                                        default:
                                            osName = "Windows Server 2008";
                                            break;
                                    }
                                    break;

                                case 1:
                                    osName = "Windows 7";
                                    break;

                                case 2:
                                    osName = "Windows 8";
                                    break;

                                case 3:
                                    osName = "Windows 8.1";
                                    break;

                                case 10:
                                    osName = "Windows 10";
                                    break;

                            }
                            break;

                    }

                    break;
            }

            return osName;
        }

        /// <summary>
        /// Get the current OS version
        /// </summary>
        /// <returns></returns>
        public static string GetOSVersion()
        {
            return Environment.OSVersion.Version.ToString();
        }

        /// <summary>
        /// Gets the java installation path. Ty captain obvious.
        /// </summary>
        /// <returns></returns>
        public static string GetJavaInstallationPath()
        {
            string environmentPath = Environment.GetEnvironmentVariable("JAVA_HOME");
            if (!string.IsNullOrEmpty(environmentPath))
            {
                return environmentPath;
            }

            string javaKey = "SOFTWARE\\JavaSoft\\Java Runtime Environment\\";
            using (Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(javaKey))
            {
                string currentVersion = rk.GetValue("CurrentVersion").ToString();
                using (Microsoft.Win32.RegistryKey key = rk.OpenSubKey(currentVersion))
                {
                    return key.GetValue("JavaHome").ToString();
                }
            }
        }
    }
}
