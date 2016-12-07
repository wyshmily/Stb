using System.Threading.Tasks;

namespace Stb.Api.Services.Push
{
    public interface IPushService
    {
        Task<bool> PushToSingleAsync(string pushId, string message);
        Task<bool> PushToSingleAsync(string pushId, object content);
    }
}
