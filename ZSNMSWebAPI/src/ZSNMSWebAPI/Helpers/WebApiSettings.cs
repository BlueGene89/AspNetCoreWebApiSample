namespace ZSNMSWebAPI.Helpers
{
    public class WebApiSettings
    {
        public WebApiSettings()
        {
            this.Appid = 0;
        }
        public string TokenKeyName { set; get; }
        public int Appid { set; get; }
        public string BaseAddress { set; get; }
        public string LoginApiPath { set; get; }
        public string RoleApiPath { set; get; }
        public string UserApiPath { set; get; }
    }
}
