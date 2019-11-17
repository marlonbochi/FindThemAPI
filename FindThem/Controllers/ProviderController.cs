using Microsoft.AspNetCore.Mvc;
using FindThem.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace FindThem.Controllers
{
    [Route("api/provider")]
    [ApiController]
    [Authorize]
    public class ProviderController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ProviderController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet("findAll")]
        public IActionResult FindAll(int page = 1)
        {
            if (page > 0)
            {
                page--;
            }
            var providers = new List<Provider>();

            using (var db = new FindThemContext())
            {
                providers = db.Providers
                            .Where(x => x.enabled == true)
                            .Include(user => user.user)
                            .Skip(20 * page).Take(20)
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
                    var user = db.Users.FirstOrDefault(x => x.email == provider.user.email);

                    provider.user.password = Utils.GetMd5HashPassword(provider.user.password);

                    if (provider.user.name == string.Empty)
                    {
                        provider.user.name = provider.name;
                    }

                    if (user == null)
                    {
                        provider.user = db.Users.Add(provider.user).Entity;
                        db.SaveChanges();
                    }
                    else
                    {
                        user.email = provider.user.email;
                        user.name = provider.user.name;
                        user.password = provider.user.password;
                        user.photo = provider.user.photo;

                        db.Users.Update(user);
                        db.SaveChanges();

                        provider.user = user;
                    }

                    var utils = new Utils();

                    var addressComplete = string.Format("{0} {1} {2} {3} {4}", provider.address, provider.number, provider.neighborhood, provider.city, provider.state);

                    var geometry = utils.getLatitudeLongitude(_config["API_KEY_GOOGLE_MAPS"], addressComplete);

                    if (geometry != null)
                    {
                        provider.latitude = geometry.location.lat;
                        provider.longitude = geometry.location.lng;
                    }

                    db.Providers.Add(provider);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Ok(new { success = false, message = ex.Message });
                }
            }

            return Ok(new { success = true, message = "Register created with success", data = provider });
        }

        [HttpPost("edit/{id}")]
        public IActionResult Edit([FromBody] Provider provider)
        {

            if (provider.user.password != string.Empty)
            {
                provider.user.password = Utils.GetMd5HashPassword(provider.user.password);
            }

            using (var db = new FindThemContext())
            {
                try
                {
                    var user = db.Users.FirstOrDefault(x => x.id == provider.user.id);

                    user.email = provider.user.email;
                    user.name = provider.user.name;
                    user.photo = provider.user.photo;

                    db.Users.Update(user);
                    db.SaveChanges();

                    provider.user = user;

                    var utils = new Utils();

                    var addressComplete = string.Format("{0} {1} {2} {3} {4}", provider.address, provider.number, provider.neighborhood, provider.city, provider.state);

                    var geometry = utils.getLatitudeLongitude(_config["API_KEY_GOOGLE_MAPS"], addressComplete);

                    if (geometry != null)
                    {
                        provider.latitude = geometry.location.lat;
                        provider.longitude = geometry.location.lng;
                    }

                    db.Providers.Update(provider);
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    return Ok(new { success = false, message = ex.Message });
                }
            }

            return Ok(new { success = true, message = "Register edited with success", data = provider });
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
                    return Ok(new { success = false, message = ex.Message });
                }
            }

            return Ok(new { success = true, message = "Register deleted with success" });
        }
    }
}