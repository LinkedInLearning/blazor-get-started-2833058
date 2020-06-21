using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Beam.Shared;
using Microsoft.AspNetCore.Components.Authorization;

namespace Beam.Client.Services
{
    public class BeamAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IBeamApiService _beamApi; 
        private readonly IDataService _dataService;
        public BeamAuthenticationStateProvider(IBeamApiService beamApi, IDataService dataSerivice)
        {
            _beamApi = beamApi;
            _dataService = dataSerivice;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            
            var currentUser = _dataService.CurrentUser;
            if (currentUser == null || !currentUser.IsAuthenticated) {
                _dataService.CurrentUser = await _beamApi.GetUser();
                currentUser = _dataService.CurrentUser;
            }

            if (currentUser.IsAuthenticated)
            {
                var claims = new[] { new Claim(ClaimTypes.Name, _dataService.CurrentUser.Name) }.Concat(_dataService.CurrentUser.Claims.Select(c => new Claim(c.Key, c.Value)));
                identity = new ClaimsIdentity(claims, "Server authentication");
            }
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task Logout()
        {
            await _beamApi.Logout();
            _dataService.CurrentUser = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        public async Task Login(Login login)
        {
            await _beamApi.Login(login);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        public async Task Register(Login login)
        {
            await _beamApi.Register(login);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
