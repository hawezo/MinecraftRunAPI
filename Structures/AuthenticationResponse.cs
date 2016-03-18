namespace MinecraftLaunching.Structures
{

    /// <summary>
    /// The same structure as MojangAPI, for compatibility purposes.
    /// </summary>
    public struct AuthenticationResponse
    {
        public string AccessToken;
        public string ClientToken;
        public string PlayerName;

        public static explicit operator AuthenticationResponse(global::MojangAPI.AuthenticationResponse.AuthenticationResponse v)
        {
            return new AuthenticationResponse
            {
                AccessToken = v.AccessToken,
                ClientToken = v.ClientToken,
                PlayerName = v.PlayerName
            };
        }
    }

}
