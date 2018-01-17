using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_jwt.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace dotnet_jwt.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        // POST api/values
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody]AuthRequestModel req)
        {
            if (req.Username == "Jon" && req.Password == "Doe") {
                var token = new JwtSecurityToken(
                    issuer: Program.Configuration["JWT:ValidIssuer"],
                    audience: Program.Configuration["JWT:ValidIssuer"],
                    claims: new[] {
                        new Claim(ClaimTypes.Name, req.Username)
                    },
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Program.Configuration["JWT:SignKey"])), SecurityAlgorithms.HmacSha256)
                );

                return Ok(new {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return BadRequest("Username or password could not be verified.");
        }
    }
}
