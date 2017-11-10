using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Week9PrismExampleApp.Models
{
    public static class BreweryDbModel
    {
        public class MainPacket<T> where T : class
        {
            [JsonProperty("numberOfPages")]
            public string NumberOfPages { get; set; }
            [JsonProperty("currentPage")]
            public string CurrentPage { get; set; }

            [JsonProperty("data")]
            public List<T> Data { get; set; }
        }

        public class Brewery
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

             [JsonProperty("established")]
            public string Established { get; set; }

            [JsonProperty("isOrganic")]
            public string IsOrganic { get; set; }

            [JsonProperty("website")]
            public string Website { get; set; }
        }
    }
}
