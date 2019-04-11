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
                if (result == null) return NotFound($"Could not find example with Id: {exampleId}");

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
                if (rasaNluData == null) return NotFound("The NLU Data does not Exist");

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

        [HttpPut("{exampleId}")]
        public async Task<ActionResult<CommonExampleModel>> Put(int rasaNluId, 
            int rasaNluDataId, int exampleId, CommonExampleModel model)
        {
            try
            {
                var oldExample = await _repository.GetExampleByRasaNluIdAsync(rasaNluId, rasaNluDataId, exampleId);
                if (oldExample == null) return NotFound($"Could not find example with Id: {exampleId}");

                _mapper.Map(model, oldExample);

                if(await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<CommonExampleModel>(oldExample);
                } else
                {
                    return BadRequest("Failed To Update the old Common Example");
                }

            } catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpDelete("{exampleId:int}")]
        public async Task<IActionResult> Delete(int rasaNluId, int rasaNluDataId, int exampleId)
        {
            try
            {
                var commonExample = await _repository.GetExampleByRasaNluIdAsync(rasaNluId, rasaNluDataId, exampleId);
                if (commonExample == null) return NotFound($"Failed to find the example with Id: {exampleId}");

                var entities = await _repository.GetEntitiesByExampleIdAsync(rasaNluId, rasaNluDataId, exampleId);

                // Delete the CommonExample
                _repository.Delete(commonExample);

                if(entities != null)
                {
                    foreach (var entity in entities)
                    {
                        // Delete all the entities that are related to commonExample
                        _repository.Delete(entity);
                    }
                }

                if (await _repository.SaveChangesAsync())
                {
                    return Ok();

                } else
                {
                    return BadRequest($"Failed to Delte the Common Example with Id: {exampleId}");
                }

            } catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}
