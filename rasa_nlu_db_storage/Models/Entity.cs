using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rasa_nlu_storage.Models
{
    public class Entity
    {
        public int Id { get; set; }
        public CommonExample CommonExample { get; set; }

        [Display(Name = "start")]
        [JsonProperty("start")]
        public int Start { get; set; }

        [Display(Name = "end")]
        [JsonProperty("end")]
        public int End { get; set; }

        [Display(Name = "value")]
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("entity")]
        [Display(Name = "Entity")]
        public string EntityName { get; set; }
    }
}
