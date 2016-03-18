using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace MinecraftLaunching
{
    public class CommandLine
    {

        /// <summary>
        /// Gets the version of the Minecraft instance linked in this command.
        /// </summary>
        public string Version {
            get
            {
                return version;
            }
        }
        private string version;

        /// <summary>
        /// Gets the credentials (see UserCredentials) of the user which'll be linked in this command
        /// </summary>
        public UserCredentials Credentials
        {
            get
            {
                return credentials;
            }
        }
        private UserCredentials credentials;

        /// <summary>
        /// Gets the amount of ram that will be dedicated to Minecraft. This is the -Xms argument.
        /// </summary>
        public int StartingRamAmount
        {
            get
            {
                return startingRamAmount;
            }
        }
        private int startingRamAmount = 512;

        /// <summary>
        /// Gets the amount of ram that will be dedicated to Minecraft. This is the -Xmx argument.
        /// </summary>
        public int MaximumRamAmount
        {
            get
            {
                return maximumRamAmount;
            }
        }
        private int maximumRamAmount = 2048;

        /// <summary>
        /// Gets the maximum amount of ram that will be dedicated to Minecraft. This is the -Xmn argument.
        /// </summary>
        public int HeapSize
        {
            get
            {
                return heapSize;
            }
        }
        private int heapSize = 512;

        /// <summary>
        /// Gets the -Dos.name string that will be sent to the command.
        /// </summary>
        public string OsName
        {
            get
            {
                return osName;
            }
        }
        private string osName;

        /// <summary>
        /// Gets the -Dos.version string that will be sent to the command.
        /// </summary>
        public string OsVersion
        {
            get
            {
                return osVersion;
            }
        }
        private string osVersion;

        /// <summary>
        /// Gets the path that contains the libraries (natives) that will be sent ti the command.
        /// </summary>
        public string LibraryPath
        {
            get
            {
                return libraryPath;
            }
        }
        private string libraryPath;

        /// <summary>
        /// Gets a list of all the libraries that will be sent after the -cp argument.
        /// </summary>
        public List<string> LibraryFiles
        {
            get
            {
                return libraryFiles;
            }
        }
        private List<string> libraryFiles;

        /// <summary>
        /// Gets the main class that will be sent to the command.
        /// </summary>
        public string MainClass
        {
            get
            {
                return mainClass;
            }
        }
        private string mainClass;

        /// <summary>
        /// Gets the minecraft arguments (the line in the .JSON file of the selected version).
        /// </summary>
        public string MinecraftArguments
        {
            get
            {
                return minecraftArguments;
            }
        }
        private string minecraftArguments;

        /// <summary>
        /// Gets the game path. By default, %appdata%/.minecraft.
        /// </summary>
        public string GamePath
        {
            get
            {
                return gamePath;
            }
        }
        private string gamePath;

        /// <summary>
        /// Gets the java path.
        /// </summary>
        public string JavaPath
        {
            get
            {
                return javaPath;
            }
        }
        private string javaPath;

        /// <summary>
        /// Gets the asset index. Automatically retrieved.
        /// </summary>
        public string AssetIndex
        {
            get
            {
                return assetIndex;
            }
        }
        private string assetIndex;

        /// <summary>
        /// Gets the assets. Automatically retrieved.
        /// </summary>
        public string Assets
        {
            get
            {
                return assets;
            }
        }
        private string assets;

        /// <summary>
        /// I don't really know what is this, but it modify the string at the bottom of the main screen.
        /// I think it is for developping purposes.
        /// You cant set every string you want, as "Hey" or "My Custom Minecaft".
        /// I recommand let it blank.
        /// </summary>
        public string VersionType
        {
            get
            {
                return versionType;
            }
            set
            {
                versionType = value;
            }
        }
        private string versionType;


        // Other variables
        private string versionFilePath;
        private string versionFile;

        public CommandLine(string version, UserCredentials credentials, string natives, int defaultRam, int maximumRam, int heapSize, string versionType) :
            this(version, credentials, natives)
        {
            this.startingRamAmount = defaultRam;
            this.maximumRamAmount = maximumRam;
            this.heapSize = heapSize;
            this.versionType = versionType;
        }

        public CommandLine(string version, UserCredentials credentials, string natives, int defaultRam, int maximumRam, string versionType) : 
            this(version, credentials, natives)
        {
            this.startingRamAmount = defaultRam;
            this.maximumRamAmount = maximumRam;
            this.versionType = versionType;
        }

        public CommandLine(string version, UserCredentials credentials, string natives, int defaultRam, int maximumRam) : 
            this(version, credentials, natives)
        {
            this.startingRamAmount = defaultRam;
            this.maximumRamAmount = maximumRam;
        }

        public CommandLine(string version, UserCredentials credentials, string natives, string versionType) : 
            this(version, credentials, natives)
        {
            this.versionType = versionType;
        }
        
        public CommandLine(string gamePath, string version, UserCredentials credentials, string natives) : 
            this(version, credentials, natives)
        {
            this.gamePath = gamePath;
        }

        public CommandLine(string version, UserCredentials credentials, string natives)
        {
            // Defining the variables
            this.version = version;
            this.credentials = credentials;
            this.libraryPath = natives;

            if (this.gamePath == null)
                this.gamePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.minecraft";
            
            this.versionFilePath = this.gamePath + String.Format(@"\versions\{0}\{0}.json", this.version);

            // Checks if the version file exists.
            if (!File.Exists(versionFilePath))
            {
                Console.WriteLine("The JSON file for Minecraft {0} could not be find. Please verify the version and your installation of Minecraft.", version);
                return;
            }

            // Reads the file
            this.versionFile = File.ReadAllText(this.versionFilePath);
            JSON.Json = this.versionFile;

            // Getting the settings
            this.libraryFiles = JSON.GetLibraries();
            this.minecraftArguments = JSON.GetMinecraftArguments();
            this.assets = JSON.GetAssets();
            this.assetIndex = JSON.GetAssetIndex();
            this.mainClass = JSON.GetMainClass();
            this.osName = OS.GetOSName();
            this.osVersion = OS.GetOSVersion();
            this.javaPath = OS.GetJavaInstallationPath();
        }

        /// <summary>
        /// Return the command line.
        /// </summary>
        /// <param name="onlyArguments">If it is set to true, there will only be the arguments, and not the java executable.</param>
        /// <returns></returns>
        public string GetCommandLine(bool onlyArguments)
        {
            string command = null;
            if (!onlyArguments)
                command += String.Format(@"{0}\java.exe ", javaPath);

            command += String.Format(@"-XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump ");
            command += String.Format(@"-Xms{0}M ", this.startingRamAmount);
            command += String.Format(@"-Xmx{0}M ", this.maximumRamAmount);
            command += String.Format(@"-XX:+UseConcMarkSweepGC ");
            command += String.Format(@"-XX:+CMSIncrementalMode ");
            command += String.Format(@"-XX:-UseAdaptiveSizePolicy ");
            command += String.Format(@"-Xmn{0}M ", this.heapSize);
            command += String.Format(@"-Dos.name=""{0} - Dos.version = {1}"" ", this.osName, this.osVersion);
            command += String.Format(@"-Djava.library.path=""{0}"" ", this.LibraryPath);
            command += String.Format(@"-cp ");

            // Adding all libraries
            foreach (string library in this.libraryFiles)
                command += String.Format("\"{0}\\libraries\\{1}\";", this.gamePath, library);

            command += String.Format(@"""{0}\versions\{1}\{1}.jar"" ", this.gamePath, version);
            command += String.Format(@"{0} ", this.mainClass);
            command += String.Format(@"{0} ",
                this.MinecraftArguments
                    .Replace("${auth_player_name}", this.credentials.Name)
                    .Replace("${version_name}", this.version)
                    .Replace("${game_directory}", String.Format("\"{0}\"", this.gamePath))
                    .Replace("${assets_root}", String.Format("\"{0}\\assets\"", this.gamePath))
                    .Replace("${assets_index_name}", this.assetIndex)
                    .Replace("${auth_uuid}", this.credentials.UUID)
                    .Replace("${auth_access_token}", this.credentials.AccessToken)
                    .Replace("${user_type}", "mojang")
                    .Replace("${user_properties}", "{}")
                    .Replace("${version_type}", String.Format("\"{0}\"", this.versionType))
                );

            // If no version type, removing the argument in order to remove the / in the main menu
            if (this.versionType == null)
                command = command.Replace("--versionType", null);

            return command;
        }

        /// <summary>
        /// Run an instance of Minecraft.
        /// </summary>
        /// <returns></returns>
        public void Run()
        {
            Process Minecraft = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "java",
                    Arguments = this.GetCommandLine(true),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            Minecraft.Start();
            while (!Minecraft.StandardOutput.EndOfStream)
            {
                string line = Minecraft.StandardOutput.ReadLine();
                Console.WriteLine(line);
            }
        }
    }
}
