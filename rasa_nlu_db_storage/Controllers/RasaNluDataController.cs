using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rasa_nlu_db_storage.Models;
using rasa_nlu_db_storage.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rasa_nlu_db_storage.Controllers
{
    [ApiController]
    [Route("api/RasaNLU/{rasaNluId}/RasaNluData")]
    public class RasaNluDataController : ControllerBase
    {
        private readonly INluRepository _repository;
        private readonly IMapper _mapper;

        public RasaNluDataController(INluRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        [HttpGet("{rasaNluDataId:int}")]
        public async Task<ActionResult<RasaNluDataModel>> Get(int rasaNluId, int rasaNluDataId)
        {
            try
            {
                var result = await _repository.GetRasaNluDataByNluModelAsync(rasaNluId, rasaNluDataId);
                return _mapper.Map<RasaNluDataModel>(result);

            } catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}
