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
    [Route("api/request")]
    [ApiController]
    [Authorize]
    public class RequestController : ControllerBase
    {
        [HttpPost("create")]
        public IActionResult Create([FromBody]Request request)
        {
            using (var db = new FindThemContext())
            {
                try
                {
                    db.Requests.Add(request);
                    db.SaveChanges();
                } catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return Ok(request);
        }
    }
}