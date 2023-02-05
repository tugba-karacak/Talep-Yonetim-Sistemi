using Newtonsoft.Json;

namespace UpSchool.HelpDesk.PresentationLayer.Models
{

    public class Cases
    {
        public object @new { get; set; }
        public object active { get; set; }
        public object critical { get; set; }
        public object recovered { get; set; }

        [JsonProperty("1M_pop")]
        public string _1M_pop { get; set; }
        public int total { get; set; }
    }

    public class Deaths
    {
        public object @new { get; set; }

        [JsonProperty("1M_pop")]
        public string _1M_pop { get; set; }
        public int total { get; set; }
    }

    public class Parameters
    {
        public string country { get; set; }
    }

    public class Response
    {
        public string continent { get; set; }
        public string country { get; set; }
        public int population { get; set; }
        public Cases cases { get; set; }
        public Deaths deaths { get; set; }
        public Tests tests { get; set; }
        public string day { get; set; }
        public DateTime time { get; set; }
    }

    public class RapidApiResultModel
    {
        public string get { get; set; }
        public Parameters parameters { get; set; }
        public List<object> errors { get; set; }
        public int results { get; set; }
        public List<Response> response { get; set; }
    }

    public class Tests
    {
        [JsonProperty("1M_pop")]
        public string _1M_pop { get; set; }
        public int total { get; set; }
    }

}
