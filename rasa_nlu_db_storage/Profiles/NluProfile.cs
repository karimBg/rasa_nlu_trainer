using AutoMapper;
using rasa_nlu_db_storage.Models;
using rasa_nlu_storage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rasa_nlu_db_storage.Profiles
{
    public class NluProfile : Profile
    {
        public NluProfile()
        {
            this.CreateMap<NluModel, Nlu>().ReverseMap();
            this.CreateMap<RasaNluDataModel, RasaNLUData>().ReverseMap();
            this.CreateMap<CommonExampleModel, CommonExample>().ReverseMap();
            this.CreateMap<EntityModel, Entity>().ReverseMap();
        }
    }
}
