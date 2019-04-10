using Newtonsoft.Json;
using rasa_nlu_storage.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rasa_nlu_db_storage.Models
{
    public class Nlu
    {
        [Display(Name = "RASA NLU Data")]
        [JsonProperty("rasa_nlu_data")]
        public RasaNluDataModel RasaNLUData { get; set; }
    }
}
