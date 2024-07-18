﻿using BAL.IServices;
using Bank.Models;
using Common.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtmController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAtmService _atmService;

        public AtmController(IConfiguration configuration,IAtmService atmService)
        {
            _configuration = configuration;
            _atmService = atmService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AtmLoginRequest request)
        {
            Card? card = _atmService.AtmCardLogin(request);
            if (card!=null)
            {
                var token = GenerateJwtToken(request.CardNumber);
                return Ok(new { token });
            }

            return Unauthorized();
        }

        private string GenerateJwtToken(string cardNumber)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, cardNumber),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

