using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FindThem.Models;

namespace FindThem.Controllers
{
    [Route("api/rate")]
    [ApiController]
    public class RateController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult Get(Int64 id)
        {
            var rate = new Rate();

            using (var db = new FindThemContext())
            {
                rate = db.Rates
                            .Where(x => x.enabled == true && x.id == id)
                           .FirstOrDefault(x => x.id == id);
            }

            return Ok(rate);
        }
    }
}