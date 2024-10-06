using System.Net.Http;
using System.Text;

namespace Loymark.Back.WebApi.Tests.Helpers
{
    public static class StringContentHelper
    {
        public static StringContent GetStringContent(string contentSerialized)
        {
            return new(
                contentSerialized,
                Encoding.UTF8,
                "application/json"
            );
        }
    }
}
