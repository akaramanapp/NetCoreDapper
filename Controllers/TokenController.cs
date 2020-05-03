using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using netcoreDapper.Business;
using netcoreDapper.Contracts;
using netcoreDapper.Data;
using netcoreDapper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;

namespace netcoreDapper.Controllers
{
    [Route("api/v1/[controller]")]
    public class TokenController : Controller
    {
        [HttpPost]
        [AllowAnonymous]
        [HttpPost, Route("login")]
        public IActionResult Login(User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            if (user.UserName == "kerim" && user.Password == "karaman")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:5000",
                    audience: "http://localhost:5000",
                    claims: new List<Claim>(){
                        new Claim("role", "admin1")
                    },
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }

    }
}