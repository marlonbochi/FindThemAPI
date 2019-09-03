using Microsoft.AspNetCore.Mvc;
using FindThem.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Authorization;

namespace FindThem.Controllers
{
    [Route("api/provider")]
    [ApiController]
    [Authorize]
    public class ProviderController : ControllerBase
    {
        [HttpGet("findAll")]
        public IActionResult FindAll()
        {
            var providers = new List<Provider>();

            using (var db = new FindThemContext())
            {
                providers = db.Providers
                            .Where(x => x.enabled == true)
                            .Include(user => user.user)
                            .ToList();
            }

            return Ok(providers);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Int64 id)
        {
            var provider = new Provider();

            using (var db = new FindThemContext())
            {
                provider = db.Providers
                            .Where(x => x.enabled == true)
                           .Include(user => user.user)
                           .FirstOrDefault(x => x.id == id);

            }

            return Ok(provider);
        }

        [HttpPost("create")]
        [AllowAnonymous]
        public IActionResult Create([FromBody]Provider provider)
        {
            using (var db = new FindThemContext())
            {
                try
                {
                    var user = db.Users.FirstOrDefault(x => x.id == provider.user.id);

                    if (user != null)
                    {
                        provider.user = user;
                    }
                    else
                    {
                        provider.user.password = Utils.GetMd5HashPassword(provider.user.password);
                    }

                    db.Providers.Add(provider);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return Ok(provider);
        }

        [HttpPost("edit/{id}")]
        public IActionResult Edit(Int64 id, string key, string value)
        {
            Provider provider = new Provider();


            using (var db = new FindThemContext())
            {
                provider = db.Providers
                           .Include(user => user.user)
                           .FirstOrDefault(x => x.id == id);

                if (provider == null)
                {
                    return NotFound("Provider not found.");
                }

                try
                {
                    provider = Provider.Update(provider, key, value);

                    db.Providers.Update(provider);
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }

            return Ok(provider);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(Int64 id)
        {
            using (var db = new FindThemContext())
            {
                try
                {
                    var provider = db.Providers
                                .FirstOrDefault(x => x.id == id);

                    provider.enabled = false;
                    provider.dateUpdated = DateTime.Now;

                    db.Providers.Update(provider);
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