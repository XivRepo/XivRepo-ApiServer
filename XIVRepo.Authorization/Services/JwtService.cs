using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using XIVRepo.Authorization.Repositories;
using XIVRepo.Core.Helpers;
using XIVRepo.Core.Models.Accounts;

namespace XIVRepo.Authorization.Services
{
    public class JwtService
    {
        private readonly AccountsRepository _accountsRepository;

        public JwtService(AccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        /// <summary>
        /// Create the claims identity for a given account.
        /// </summary>
        /// <param name="account">Account to create claims from.</param>
        /// <returns>The created claims identity.</returns>
        public async Task<ClaimsIdentity> CreateClaimsForJwt(Account account)
        {
            ClaimsIdentity identity = new ClaimsIdentity();

            identity.AddClaim(new Claim(ClaimTypes.Name, account.DisplayName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, account.AccountId.ToString()));
            
            foreach (var role in await _accountsRepository.GetAllRoles())
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role.Title));
            }
            
            return identity;
        }
        
        /// <summary>
        /// Creates a JWT using the given claims.
        /// </summary>
        /// <param name="claims">Claims to create the token with.</param>
        /// <returns>The created token.</returns>
        public string CreateJwt(ClaimsIdentity claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(
                issuer: EnvironmentVariables.JwtIssuer(),
                audience: EnvironmentVariables.JwtAudience(),
                subject: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials:
                new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.Default.GetBytes(EnvironmentVariables.JwtKey())),
                    SecurityAlgorithms.HmacSha256Signature));
            return tokenHandler.WriteToken(token);
        }
    }
}