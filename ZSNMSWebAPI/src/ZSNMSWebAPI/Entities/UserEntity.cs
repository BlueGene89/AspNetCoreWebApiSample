using System;
using Newtonsoft.Json;

namespace ZSNMSWebAPI.Entities
{
    public class UserEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "errorcode")]
        public int ErrorCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "errorinfo")]
        public string ErrorInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "UserAccount")]
        public string UserAccount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "UserCode")]
        public int UserCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "UserName")]
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "PinyinFirst")]
        public string PinyinFirst { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "PinyinLast")]
        public string PinyinLast { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "PinyinInitials")]
        public string PinyinInitials { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "Password")]
        public string Password { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "Sex")]
        public int Sex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "BirthDate")]
        public DateTime? BirthDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "Mobile")]
        public string Mobile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "Telephone")]
        public string Telephone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "Phone")]
        public string Phone { get; set; }
        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "CreateDate")]
        public DateTime? CreateDate { get; set; }
        [JsonProperty(PropertyName = "Status")]
        public int Status { get; set; }
        [JsonProperty(PropertyName = "LastUpdateDate")]
        public DateTime? LastUpdateDate { get; set; }
        [JsonProperty(PropertyName = "DeptId")]
        public int DeptId { get; set; }
        [JsonProperty(PropertyName = "ShortPassword")]
        public string ShortPassword { get; set; }
        [JsonProperty(PropertyName = "DeptName")]
        public string DeptName { get; set; }
    }
}
