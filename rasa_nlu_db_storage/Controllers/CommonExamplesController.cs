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
    [Route("api/RasaNLU/{rasaNluId}/RasaNluData/{rasaNluDataId}/CommonExamples")]
    public class CommonExamplesController : ControllerBase
    {
        private readonly INluRepository _repository;
        private readonly IMapper _mapper;

        public CommonExamplesController(INluRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CommonExampleModel[]>> Get(int rasaNluId, int rasaNluDataId)
        {
            try
            {
                var result = await _repository.GetExamplesByRasaNluIdAsync(rasaNluId, rasaNluDataId);
                return _mapper.Map<CommonExampleModel[]>(result);

            } catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{exampleId}")]
        public async Task<ActionResult<CommonExampleModel>> Get(int rasaNluId, int rasaNluDataId, int exampleId)
        {
            try
            {
                var result = await _repository.GetExampleByRasaNluIdAsync(rasaNluId, rasaNluDataId, exampleId);

                if (result == null) return NotFound();

                return _mapper.Map<CommonExampleModel>(result);

            } catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}
