using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace bot.config
{
    public class Config
    {
        public string? token { get; set; }
        public ulong guildId { get; set; }

        public async Task ReadConfig()
        {
            using (StreamReader sr = new StreamReader("config.json"))
            {
                string json = await sr.ReadToEndAsync();
                JSONStruct data = JsonConvert.DeserializeObject<JSONStruct>(json);

                this.token = data.token;
                this.guildId = data.guildId;
            }
        }
    }

    internal sealed class JSONStruct
    {
        public string? token { get; set; }
        public ulong guildId { get; set; }
    }
}
