using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XIVRepo.Authorization.Repositories;
using XIVRepo.Authorization.Services;
using XIVRepo.Core.Models.Accounts;

namespace XIVRepo.Authorization.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthorizationController : ControllerBase
    {
        private readonly AccountsRepository _accountsRepository;
        private readonly JwtService _jwtService;
        
        public AuthorizationController(AccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
            _jwtService = new JwtService(accountsRepository);
        }
        
        
        [HttpGet("login")]
        public async Task<string> Login()
        {
            // Grab Default User
            var account = await _accountsRepository.GetDefaultAccount();
            
            // Generate Signed JWT for User
            var accountClaims = await this._jwtService.CreateClaimsForJwt(account);
            return _jwtService.CreateJwt(accountClaims);
        }
        
        [HttpGet("register")]
        public async Task CreateDefaultAccount()
        {
            var defaultAccount = await _accountsRepository.GetDefaultAccount();
            if (defaultAccount is null)
            {
                await _accountsRepository.AddAccount(new Account
                {
                    Id = Guid.Parse("8b3cc4c0-46d5-47df-8bc3-2d7672a2dd70"),
                    DiscordId = "142645013300510720",
                    DisplayName = "Example User",
                    Roles = null
                });
            }
        }
    }
}