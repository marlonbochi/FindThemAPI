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
    [Authorize]
    public class RateController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var rate = new Rate();

            using (var db = new FindThemContext())
            {
                try
                {
                        rate = db.Rates
                                .Where(x => x.enabled == true && x.id == id)
                               .FirstOrDefault(x => x.id == id);
                } catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return Ok(rate);
        }

        [HttpPost]
        public IActionResult Set([FromBody] Rate rate)
        {
            using (var db = new FindThemContext())
            {
                try
                {
                    db.Rates.Add(rate);
                    db.SaveChanges();

                } catch(Exception ex)
                {
                    return Ok(new { success = false, message = ex.Message });
                }
            }

            return Ok(new { success = true, message = "Register created with success", data = rate });
        }
    }
}