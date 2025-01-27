﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.Application.Common.Interfaces.Authentication;
using OnlineStore.Application.Common.Interfaces.Services;
using OnlineStore.Infrastructure.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using OnlineStore.Application.Services;
using OnlineStore.Domain.Models;

namespace OnlineStore.Infrastructure
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IDateTimeProvider _dateTimeProvider;

        public JwtTokenGenerator (IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtSettings)
        {
            _dateTimeProvider = dateTimeProvider;
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateToken(User user)
        {
            //Console.WriteLine($"Section name = {JwtSettings.SectionName} \n" +
            //    $"Secret = {_jwtSettings.Secret} \n" +
            //    $"Expiry = {_jwtSettings.ExpiryMinutes} \n " +
            //    $"Issuer = {_jwtSettings.Issuer} \n" +
            //    $"Audience = {_jwtSettings.Audience}");

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var securityToken = new JwtSecurityToken(                                                                   
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                claims: claims,
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}