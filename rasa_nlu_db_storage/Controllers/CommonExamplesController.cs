using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using rasa_nlu_db_storage.Models;
using rasa_nlu_db_storage.Repository;
using rasa_nlu_storage.Entities;
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
        private readonly LinkGenerator _linkGenerator;

        public CommonExamplesController(INluRepository repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._linkGenerator = linkGenerator;
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

        [HttpPost]
        public async Task<ActionResult<CommonExampleModel>> Post(int rasaNluId, int rasaNluDataId, CommonExampleModel model)
        {
            try
            {
                var rasaNluData = await _repository.GetRasaNluDataByNluModelAsync(rasaNluId, rasaNluDataId);
                if (rasaNluData == null) return BadRequest("The NLU Data does not Exist");

                var commonExample = _mapper.Map<CommonExample>(model);
                commonExample.RasaNLUData = rasaNluData;

                _repository.Add(commonExample);

                if(await _repository.SaveChangesAsync())
                {
                    var url = _linkGenerator.GetPathByAction(HttpContext,
                        "Get",
                        values: new { rasaNluId, rasaNluDataId, id = commonExample.Id });
                    return Created(url, _mapper.Map<CommonExampleModel>(commonExample));

                } else
                {
                    return BadRequest("Failed To save the new Common Example");
                }

            } catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}
