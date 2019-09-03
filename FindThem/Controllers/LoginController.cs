using Microsoft.AspNetCore.Mvc;
using FindThem.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FindThem.Controllers
{
    [Route("api/login")]
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult post(string login, string password)
        {
            var user = new User();

            using (var db = new FindThemContext())
            {
                user = db.Users
                            .Where(x => x.email == login && x.password == Utils.GetMd5HashPassword(password))
                           .FirstOrDefault();

                
            }

            if (user == null)
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim("userId", user.id.ToString()), // Aqui é adicionado o id do usuário
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Authentication:SecretKey"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                claims: claims,
                signingCredentials: credential,
                expires: DateTime.Now.AddDays(1),
                issuer: _config["Authentication:Issuer"],
                audience: _config["Authentication:Audience"]
            );

            var jwtToken = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };

            return Ok(jwtToken);
        }
    }
}