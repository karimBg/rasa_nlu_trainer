using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rasa_nlu_storage.Models
{
    public class CommonExample
    {
        public int Id { get; set; }
        public RasaNLUData RasaNLUData { get; set; }

        [Display(Name = "text")]
        [JsonProperty("text")]
        public string Text { get; set; }

        [Display(Name = "intent")]
        [JsonProperty("intent")]
        public string Intent { get; set; }

        [Display(Name = "entities")]
        [JsonProperty("entities")]
        public ICollection<Entity> Entities { get; set; }
    }
}
