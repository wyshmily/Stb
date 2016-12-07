using Stb.Api.Models.Getui;
using Stb.Util;
using System;
using Flurl;
using Flurl.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Stb.Api.Services.Push
{
    public class GetuiService : IPushService
    {
        //public const string AppId = "iyUA2x2f4t7z24ekpjwxv8";
        public const string AppSecret = "RU8UAPLuOj97zZgnBGSgq3";
        public const string AppKey = "HYHYp1tQ3n9cgXqNALOXn5";
        public const string MasterSecret = "LDgamE8MTM8ZNmOMwzWeu6";
        public const string BaseUrl = "https://restapi.getui.com/v1/iyUA2x2f4t7z24ekpjwxv8";

        private static string token = null;
        private static DateTime tokenTime;

        public async Task<bool> PushToSingleAsync(string pushId, string message)
        {
            await GetAuthTokenAsync();
            if (token == null)
                throw new ApiException("获取个推Token失败");

            SingleTransmissionData data = new SingleTransmissionData
            {
                message = new Message { appkey = AppKey, },
                transmission = new Transmission { transmission_content = message },
                cid = pushId,
                requestid = DateTime.Now.ToUnixMilliSeconds().ToString(),
            };

            var result = await BaseUrl.AppendPathSegment("push_single")
                .WithHeader("authtoken", token)
                .PostJsonAsync(data)
                .ReceiveJson<GetuiResult>();

            return true;
        }

        public async Task<bool> PushToSingleAsync(string pushId, object content)
        {
            return await PushToSingleAsync(pushId, JsonConvert.SerializeObject(content));
        }

        // 个推鉴权
        private async Task GetAuthTokenAsync()
        {
            // token存在并处在有效期内
            if (token != null && (DateTime.Now - tokenTime) < TimeSpan.FromHours(23))
                return;

            token = null;

            var timestamp = DateTime.Now.ToUnixMilliSeconds();
            AuthSignParams loginParams = new AuthSignParams
            {
                appkey = AppKey,
                timestamp = timestamp.ToString(),
                sign = ShaHelper.GetSha256($"{AppKey}{timestamp}{MasterSecret}"),
            };

            var result = await BaseUrl.AppendPathSegment("auth_sign")
                .PostJsonAsync(loginParams)
                .ReceiveJson<GetuiResult>();

            if (result != null && result.result == "ok")
            {
                token = result.auth_token;
                tokenTime = DateTime.Now;
            }
        }
    }
}
