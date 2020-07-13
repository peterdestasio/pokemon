using System.Net.Http;
using System.Threading.Tasks;

namespace Pokemon.Data.Http
{
   public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> GetAsync(string requestUri);
    }
}