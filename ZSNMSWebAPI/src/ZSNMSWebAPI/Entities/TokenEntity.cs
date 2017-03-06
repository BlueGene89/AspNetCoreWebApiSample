using System;

namespace ZSNMSWebAPI.Entities
{
    public class TokenEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? starttime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? expiredtime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string refrash_token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int errorcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string errorinfo { get; set; }
    }
}
