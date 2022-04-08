namespace EasyWorkEntities.Authentication.Request
{
    public class AutenticarGoogleRequest
    {
        public string account { get; set; }
        public string idToken { get; set; }
        public string id { get; set; }
        public accountData accountObj { get; set; }
        public string email { get; set; }
        public string displayName { get; set; }
        public string photoUrl { get; set; }
        public string serverAuthCode { get; set; }
        public string idExpired { get; set; }
        public class accountData
        {
            public string name { get; set; } //CORREO
            public string type { get; set; } //EXTENSION
        }
    }
}
