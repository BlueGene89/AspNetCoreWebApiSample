using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using ZSNMSWebAPI.Entities;

namespace ZSNMSWebAPI.Helpers
{
    public class TokenOptions : AuthenticationOptions
    {
        public TokenOptions() : base()
        {
            AuthenticationScheme = "ZSHWebApiToken";
            AutomaticAuthenticate = true;
            ClaimsIssuer = "ZSNMSWebAPI.zshis.com.sh";
        }

    }

    public class ZsWebApiAuthMiddleware : AuthenticationMiddleware<TokenOptions>
    {
        protected override AuthenticationHandler<TokenOptions> CreateHandler()
        {
            return new AuthHandler(this._validator, this._webApiSettings);
        }

        private readonly IAuthValidator _validator;
        private readonly IOptions<WebApiSettings> _webApiSettings;
        public ZsWebApiAuthMiddleware(IAuthValidator validator, IOptions<WebApiSettings> webApiSettings, RequestDelegate next, IOptions<TokenOptions> options, ILoggerFactory loggerFactory, UrlEncoder encoder) : base(next, options, loggerFactory, encoder)
        {
            _webApiSettings = webApiSettings;
            _validator = validator;
        }
    }

    public class AuthHandler : AuthenticationHandler<TokenOptions>
    {
        private readonly IAuthValidator _authValidator;
        private readonly WebApiSettings _webApiSettingsValidator;
        public AuthHandler(IAuthValidator authValidator, IOptions<WebApiSettings> webApiSettingsValidator)
        {
            _authValidator = authValidator;
            _webApiSettingsValidator = webApiSettingsValidator.Value;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            StringValues token;
            AuthenticateResult result;

            if (!Request.Headers.TryGetValue(_webApiSettingsValidator.TokenKeyName, out token))
            { //用skip代替fail，以防log入侵
                //result = AuthenticateResult.Fail("权限验证失败");
                //Logger.LogDebug("权限验证失败");
                return AuthenticateResult.Skip();
            }

            // If no token found, no further work possible
            if (string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.Skip();
            }


            var tokenValidResponseMessage = await _authValidator.TokenValidAndGetUserInfoAsync(token, _webApiSettingsValidator);
            if (tokenValidResponseMessage.IsSuccessStatusCode)
            {
                var userInfoResponseJsonStr = await tokenValidResponseMessage.Content.ReadAsStringAsync();
                var userEntity = JsonConvert.DeserializeObject<UserEntity>(userInfoResponseJsonStr);
                //没有用户信息 或者 反序列化失败 就不能登录
                if (userEntity == null)
                {
                    return AuthenticateResult.Skip();
                }
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userEntity.UserName),
                    new Claim(ClaimTypes.NameIdentifier, userEntity.UserCode.ToString()),
                    new Claim("DeptName", userEntity.DeptName),
                    new Claim("DeptID", userEntity.DeptId.ToString()),
                    new Claim(ClaimTypes.Email, userEntity.Email),
                    new Claim("Sex", userEntity.Sex.ToString())
                };
                //获取用户的角色
                var rolesResponseMessage = await _authValidator.GetUserRolesAsync(token, _webApiSettingsValidator);
                var rolesResponseJsonStr = await rolesResponseMessage.Content.ReadAsStringAsync();
                var roleEntities = JsonConvert.DeserializeObject<List<RoleEntity>>(rolesResponseJsonStr);


                foreach (var role in roleEntities)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    claims.AddRange(role.RoleHandRight.Select(rolehandRight => new Claim("RoleHandRight", rolehandRight.Name)));
                }

                var claimsIdentity = new ClaimsIdentity(claims, "CustomClaims");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                var ticket = new AuthenticationTicket(claimsPrincipal,
                    new AuthenticationProperties(), Options.AuthenticationScheme);
                result = AuthenticateResult.Success(ticket);
            }
            else
            {
                //Logger.LogDebug("权限验证失败：无法获得用户信息");
                return AuthenticateResult.Skip();
            }
            return result;
        }
    }

    public interface IAuthValidator
    {
        Task<HttpResponseMessage> TokenValidAndGetUserInfoAsync(StringValues token, WebApiSettings webApiSettingsValidator);
        Task<HttpResponseMessage> GetUserRolesAsync(StringValues token, WebApiSettings webApiSettingsValidator);
    }

    public class AuthValidator : IAuthValidator
    {
        //验证token并返回userinfo
        public Task<HttpResponseMessage> TokenValidAndGetUserInfoAsync(StringValues token, WebApiSettings webApiSettings)
        {
            var headerParams = new Dictionary<string, string>
            {
                {"accesstoken", token.ToString()},
                //{"appid", webApiSettings.Appid>0?webApiSettings.Appid.ToString():"0"}
                //这里用0，因为权限api没有对这方面进行完善
                { "appid", "0"}
            };

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            foreach (var param in headerParams)
            {
                client.DefaultRequestHeaders.Add(param.Key, param.Value);
            }

            return client.GetAsync(webApiSettings.BaseAddress + webApiSettings.UserApiPath);
        }
        //获取用户角色
        public Task<HttpResponseMessage> GetUserRolesAsync(StringValues token, WebApiSettings webApiSettings)
        {
            var headerParams = new Dictionary<string, string>
            {
                {"accesstoken", token.ToString()},
                {"appid", webApiSettings.Appid>0?webApiSettings.Appid.ToString():"0"}
            };

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            foreach (var param in headerParams)
            {
                client.DefaultRequestHeaders.Add(param.Key, param.Value);
            }
            return client.GetAsync(webApiSettings.BaseAddress + webApiSettings.RoleApiPath);
        }
    }


}
