using MinecraftLaunching.Structures;

namespace MinecraftLaunching
{
    public class UserCredentials
    {
        
        /// <summary>
        /// The UUID of the user.
        /// </summary>
        public string UUID
        {
            get
            {
                return uuid;
            }
        }
        private string uuid;

        /// <summary>
        /// The name of the user.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }
        private string name;

        /// <summary>
        /// The access token of the user.
        /// </summary>
        public string AccessToken
        {
            get
            {
                return accessToken;
            }
        }
        private string accessToken;

        /// <summary>
        /// Get an instance of this class with the user UUID, name and access token from an identification.
        /// </summary>
        /// <param name="uuid">The UUID of the player. You can get it with MojangAPI's UuidByName request.</param>
        /// <param name="name">The UUID of the player. You can get it with MojangAPI's UuidByName or Authentication request.</param>
        /// <param name="accessToken">The UUID of the player. You can get it with MojangAPI's Authentication request.</param>
        public UserCredentials(string uuid, string name, string accessToken)
        {
            this.uuid = uuid;
            this.name = name;
            this.accessToken = accessToken;
        }

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="auth"></param>
        /// <param name="uuidRequest"></param>
        public UserCredentials(AuthenticationResponse auth, UuidByNameResponse uuidRequest)
        {
            this.uuid = uuidRequest.ID;
            this.name = auth.PlayerName;
            this.accessToken = auth.AccessToken;
        }

    }
}
