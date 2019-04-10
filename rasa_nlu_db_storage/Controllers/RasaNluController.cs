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
    [Route("api/[controller]")]
    [ApiController]
    public class RasaNluController : ControllerBase 
    {
        private readonly INluRepository _repository;
        private readonly IMapper _mapper;

        public RasaNluController(INluRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Nlu>> Get(int id)
        {
            try
            {
                var result = await _repository.GetRasaModelAsync(id);

                if (result == null) return NotFound();

                return _mapper.Map<Nlu>(result);

            } catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}
