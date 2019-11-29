using Microsoft.AspNetCore.Mvc;
using FindThem.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Authorization;

namespace FindThem.Controllers
{
    [Route("api/service")]
    [ApiController]
    [Authorize]
    public class ServiceController : ControllerBase
    {
        [HttpGet("findAll")]
        public IActionResult FindAll(Int64 idProvider)
        {
            var clients = new List<Service>();

            using (var db = new FindThemContext())
            {
                clients = db.Services
                            .Where(x => x.enabled == true)
                            .ToList();
            }

            return Ok(clients);
        }
        [HttpGet("findAll/{providerID}")]
        public IActionResult FindProviderServices(Int64 providerID)
        {
            var clients = new List<Service>();

            using (var db = new FindThemContext())
            {
                clients = db.Services
                            .Where(x => x.enabled == true && x.providerID == providerID)
                            .ToList();
            }

            return Ok(clients);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Int64 id)
        {
            var service = new Service();

            using (var db = new FindThemContext())
            {
                service = db.Services
                            .Where(x => x.enabled == true)
                           .FirstOrDefault(x => x.id == id);

            }

            return Ok(service);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody]Service service)
        {
            using (var db = new FindThemContext())
            {
                var provider = db.Providers.FirstOrDefault(x => x.id == service.providerID);

                if (provider == null)
                {
                    return NotFound("Provider not found.");
                }

                db.Services.Add(service);
                db.SaveChanges();
            }

            return Ok(service);
        }

        [HttpPost("edit")]
        public IActionResult Edit([FromBody]Service service)
        {
            using (var db = new FindThemContext())
            {
                try
                {
                    service.dateUpdated = DateTime.Now;

                    db.Services.Update(service);
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }

            return Ok(service);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(Int64 id)
        {
            using (var db = new FindThemContext())
            {
                try
                {
                    var service = db.Services
                                .FirstOrDefault(x => x.id == id);

                    service.enabled = false;
                    service.dateUpdated = DateTime.Now;

                    db.Services.Update(service);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return Ok();
        }
    }
}