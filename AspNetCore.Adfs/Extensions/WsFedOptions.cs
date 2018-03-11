namespace Microsoft.AspNetCore.Authentication
{
    public class WsFedOptions
    {
        public string Realm { get; set; }
        public string Metadata { get; set; }
        public string CallbackPath { get; set; }
    }
}
