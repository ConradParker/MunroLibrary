using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MunroLibrary.Data;
using MunroLibrary.Domain;

namespace MunroLibrary.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunrosController : ControllerBase
    {
        // GET: api/<Munros>
        [HttpGet]
        public IEnumerable<Munro> Get()
        {
            var data = new MunroRepository();

            return data.GetData();
        }

        // GET api/<Munros>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
