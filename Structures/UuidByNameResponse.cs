namespace MinecraftLaunching.Structures
{
    /// <summary>
    /// The same structure as MojangAPI, for compatibility purposes.
    /// </summary>
    public struct UuidByNameResponse
    {
        public string ID;
        public string Name;

        public static explicit operator UuidByNameResponse(global::MojangAPI.UuiByNameResponse.UuidByNameResponse v)
        {
            return new UuidByNameResponse
            {
                ID = v.ID,
                Name = v.Name
            };
        }
    }
}
