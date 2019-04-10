using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rasa_nlu_storage.Entities
{
    public class NluModel
    {
        [Display(Name = "RASA NLU Data")]
        [JsonProperty("rasa_nlu_data")]
        public RasaNLUData RasaNLUData { get; set; }

        public int Id { get; set; }
    }
}
