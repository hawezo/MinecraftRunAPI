using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinecraftLaunching
{
    class JSON
    {

        /// <summary>
        /// The JSON string, not parsed.
        /// </summary>
        public static string Json {
            get
            {
                return json;
            }
            set
            {
                ParsedJson = JObject.Parse(value);
                json = value;
            }
        }
        private static string json;

        /// <summary>
        /// The parsed JSON.
        /// </summary>
        private static JObject ParsedJson;



        /// <summary>
        /// Gets the list of the libraries (used in the -cp argument).
        /// </summary>
        public static List<String> GetLibraries()
        {
            if (ParsedJson == null)
                throw new NullReferenceException("You have not initialized the JSON file yet.");

            // Creating the list
            List<String> libraries = new List<string>() { };

            // Running through the items
            IEnumerable<JToken> Root = ParsedJson.SelectTokens("libraries");
            foreach (JToken library in Root.First().Children())
            {
                JToken item = library.SelectToken("downloads.artifact.url");

                // Selecting all libraries excluding the natives
                if (item != null && library.SelectToken("downloads.classifiers") == null)
                {
                    // Adding the item to the list
                    libraries.Add(item.ToString().Split(new char[] { '/' }, 4)[3].Replace("\",", null).Replace("/", "\\"));
                }
            }

            // Returning the list
            return libraries;
        }

        /// <summary>
        /// Gets the argument list (the end of the command).
        /// </summary>
        /// <returns></returns>
        public static string GetMinecraftArguments()
        {
            if (ParsedJson == null)
                throw new NullReferenceException("You have not initialized the JSON file yet.");

            return ParsedJson.SelectToken("minecraftArguments").ToString();
        }

        /// <summary>
        /// Gets the path of the main class of the <version>.jar
        /// </summary>
        /// <returns></returns>
        public static string GetMainClass()
        {
            if (ParsedJson == null)
                throw new NullReferenceException("You have not initialized the JSON file yet.");

            return ParsedJson.SelectToken("mainClass").ToString();
        }

        /// <summary>
        /// Gets the assets string
        /// </summary>
        /// <returns></returns>
        public static string GetAssets()
        {
            if (ParsedJson == null)
                throw new NullReferenceException("You have not initialized the JSON file yet.");

            return ParsedJson.SelectToken("assets").ToString();
        }

        /// <summary>
        /// Gets the assets index string
        /// </summary>
        /// <returns></returns>
        public static string GetAssetIndex()
        {
            if (ParsedJson == null)
                throw new NullReferenceException("You have not initialized the JSON file yet.");

            return ParsedJson.SelectToken("assetIndex.id").ToString();
        }
    }

}
