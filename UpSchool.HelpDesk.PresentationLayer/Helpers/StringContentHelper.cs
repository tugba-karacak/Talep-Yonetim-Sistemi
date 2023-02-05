using System.Text;
using System.Text.Json;

namespace UpSchool.HelpDesk.PresentationLayer.Helpers
{
    public class StringContentHelper
    {
        public static StringContent CreateStringContent<T>(T model) where T : class
        {
            return new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        }
    }
}
