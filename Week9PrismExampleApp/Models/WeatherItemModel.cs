using System;
using System.Net;
using System.Collections.Generic;

using Newtonsoft.Json;
namespace Week9PrismExampleApp.Models
{
    public static class WeatherItemModel
    {
        public partial class WeatherItem
        {
            [JsonProperty("coord")]
            public Coord Coord { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("clouds")]
            public Clouds Clouds { get; set; }

            [JsonProperty("base")]
            public string Base { get; set; }

            [JsonProperty("cod")]
            public long Cod { get; set; }

            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("dt")]
            public long Dt { get; set; }

            [JsonProperty("main")]
            public Main Main { get; set; }

            [JsonProperty("visibility")]
            public long Visibility { get; set; }

            [JsonProperty("sys")]
            public Sys Sys { get; set; }

            [JsonProperty("weather")]
            public Weather[] Weather { get; set; }

            [JsonProperty("wind")]
            public Wind Wind { get; set; }
        }

        public partial class Coord
        {
            [JsonProperty("lat")]
            public double Lat { get; set; }

            [JsonProperty("lon")]
            public double Lon { get; set; }
        }

        public partial class Clouds
        {
            [JsonProperty("all")]
            public long All { get; set; }
        }

        public partial class Main
        {
            [JsonProperty("pressure")]
            public long Pressure { get; set; }

            [JsonProperty("temp_max")]
            public double TempMax { get; set; }

            [JsonProperty("humidity")]
            public long Humidity { get; set; }

            [JsonProperty("temp")]
            public double Temp { get; set; }

            [JsonProperty("temp_min")]
            public double TempMin { get; set; }
        }

        public partial class Sys
        {
            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("sunrise")]
            public long Sunrise { get; set; }

            [JsonProperty("country")]
            public string Country { get; set; }

            [JsonProperty("message")]
            public double Message { get; set; }

            [JsonProperty("sunset")]
            public long Sunset { get; set; }

            [JsonProperty("type")]
            public long Type { get; set; }
        }

        public partial class Weather
        {
            [JsonProperty("icon")]
            public string Icon { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("main")]
            public string Main { get; set; }
        }

        public partial class Wind
        {
            [JsonProperty("deg")]
            public long Deg { get; set; }

            [JsonProperty("speed")]
            public double Speed { get; set; }
        }

        public partial class WeatherItem
        {
            public static WeatherItem FromJson(string json) => JsonConvert.DeserializeObject<WeatherItem>(json, Converter.Settings);
        }

        public static string ToJson(this WeatherItem self) => JsonConvert.SerializeObject(self, Converter.Settings);

        public class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
            };
        }
    }
}
