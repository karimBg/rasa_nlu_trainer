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
    [Route("api/RasaNLU/{rasaNluId}/RasaNluData/{rasaNluDataId}/CommonExamples/{exampleId}/entities")]
    public class EntitiesController : ControllerBase
    {
        private readonly INluRepository _repository;
        private readonly IMapper _mapper;

        public EntitiesController(INluRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
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

                if (result == null) return NotFound();

                return _mapper.Map<EntityModel>(result);

            } catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}
