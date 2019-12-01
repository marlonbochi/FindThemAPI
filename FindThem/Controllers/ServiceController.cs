using Microsoft.AspNetCore.Mvc;
using FindThem.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
            var services = new List<Service>();


            long id = 0;

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                id = Convert.ToInt64(identity.FindFirst("userId").Value);
            }

            var kindUser = FindThem.Models.User.getKindUser(id);


            using (var db = new FindThemContext())
            {
                if (kindUser == "admin")
                {
                    services = db.Services
                            .Where(x => x.enabled == true)
                            .Include(x => x.provider)
                            .ToList();
                } else
                {
                    services = db.Services
                            .Where(x => x.enabled == true && x.provider.user.id == id)
                            .Include(x => x.provider)
                            .ToList();
                }
            }

            return Ok(services);
        }

        [HttpGet("findAll/{providerID}")]
        public IActionResult FindProviderServices(Int64 providerID)
        {
            var services = new List<Service>();

            using (var db = new FindThemContext())
            {
                services = db.Services
                            .Where(x => x.enabled == true && x.provider.id == providerID)
                            .Include(x => x.provider)
                            .ToList();
            }

            return Ok(services);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Int64 id)
        {
            var service = new Service();

            using (var db = new FindThemContext())
            {
                service = db.Services
                            .Where(x => x.enabled == true)
                            .Include(x => x.provider)
                           .FirstOrDefault(x => x.id == id);

            }

            return Ok(service);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody]Service service)
        {
            using (var db = new FindThemContext())
            {
                try
                {
                    var provider = db.Providers.Include(x => x.user).FirstOrDefault(x => x.id == service.provider.id);

                    if (provider == null)
                    {
                        throw new Exception("Provider not found.");
                    }

                    service.provider = provider;

                    db.Services.Add(service);
                    db.SaveChanges();
                } 
                catch (Exception ex) 
                {
                    return Ok(new { success = false, message = ex.Message });
                }
            }

            return Ok(new { success = true, message = "Register created with success", data = service });
        }

        [HttpPost("edit")]
        public IActionResult Edit([FromBody]Service service)
        {
            using (var db = new FindThemContext())
            {
                try
                {
                    var provider = db.Providers.Include(x => x.user).FirstOrDefault(x => x.id == service.provider.id);

                    if (provider == null)
                    {
                        return NotFound("Provider not found.");
                    }

                    service.dateUpdated = DateTime.Now;

                    service.provider = provider;

                    db.Services.Update(service);
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    return Ok(new { success = false, message = ex.Message });
                }
            }

            return Ok(new { success = true, message = "Register edited with success", data = service });
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
                    return Ok(new { success = false, message = ex.Message });
                }
            }

            return Ok(new { success = true, message = "Register deleted with success" });
        }
    }
}