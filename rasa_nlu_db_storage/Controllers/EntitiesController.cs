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
    [Route("api/RasaNLU/{rasaNluId}/RasaNluData/{rasaNluDataId}/CommonExamples/{exampleId}/entities")]
    public class EntitiesController : ControllerBase
    {
        private readonly INluRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public EntitiesController(INluRepository repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<EntityModel[]>> Get(int rasaNluId, int rasaNluDataId, int exampleId)
        {
            try
            {
                var result = await _repository.GetEntitiesByExampleIdAsync(rasaNluId, rasaNluDataId, exampleId);
                return _mapper.Map<EntityModel[]>(result);

            } catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{entityId}")]
        public async Task<ActionResult<EntityModel>> Get(int rasaNluId, int rasaNluDataId, int exampleId, int entityId)
        {
            try
            {
                var result = await _repository.GetEntityByExampleIdAsync(rasaNluId, rasaNluDataId, exampleId, entityId);
                if (result == null) return NotFound($"Could not find Entitie with Id: {entityId}, " +
                    $"inside the CommonExample with the Id: {exampleId}");

                return _mapper.Map<EntityModel>(result);

            } catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<EntityModel>> Post(int rasaNluId, int rasaNluDataId, 
            int exampleId, EntityModel model)
        {
            try
            {
                var commonExample = await _repository.GetExampleByRasaNluIdAsync(rasaNluId, rasaNluDataId, exampleId);
                if (commonExample == null) return NotFound($"Could Not find example with Id: {exampleId}");

                var entity = _mapper.Map<Entity>(model);
                entity.CommonExample = commonExample;

                _repository.Add(entity);

                if (await _repository.SaveChangesAsync())
                {
                    var url = _linkGenerator.GetPathByAction(HttpContext,
                        "Get",
                        values: new { rasaNluId, rasaNluDataId, exampleId, id = entity.Id });
                    return Created(url, _mapper.Map<EntityModel>(entity));

                } else
                {
                    return BadRequest("Failed To save the new Entity");
                }

            } catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }


    }
}
