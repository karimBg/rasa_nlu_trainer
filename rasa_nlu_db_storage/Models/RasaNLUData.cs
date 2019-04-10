using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rasa_nlu_storage.Models
{
    public class RasaNLUData
    {
        public int Id { get; set; }
        public int NluModelId { get; set; }

        [Display(Name = "Common Examples")]
        [JsonProperty("common_examples")]
        public ICollection<CommonExample> CommonExamples { get; set; }
    }
}
