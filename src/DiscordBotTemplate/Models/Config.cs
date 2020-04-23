using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DiscordBotTemplate.Models
{
    public class Config
    {
        [JsonProperty("command_prefix")] public string CommandPrefix { get; set; }
    }
}
