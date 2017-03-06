using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZSNMSWebAPI.Entities
{
    public class RoleDataRight
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "rolecode")]
        public string RoleCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "parentcode")]
        public string ParentCode { get; set; }
    }

    public class RoleHandRight
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "rolecode")]
        public string RoleCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }
        /// <summary>
        /// 应用管理
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "parentcode")]
        public string ParentCode { get; set; }
    }

    public class RoleEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "appid")]
        public string Appid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }
        /// <summary>
        /// 护士工作站管理员
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "parentcode")]
        public string ParentCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "RoleDataRight")]
        public List<RoleDataRight> RoleDataRight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "RoleHandRight")]
        public List<RoleHandRight> RoleHandRight { get; set; }
    }
}
