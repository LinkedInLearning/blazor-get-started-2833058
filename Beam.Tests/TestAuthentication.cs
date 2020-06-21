using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Bunit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

/// Credit for the Authentication framework 
///https://github.com/egil/bunit/issues/135#issuecomment-641618101

namespace Beam.Tests.Auth
{
    public class TestAuthenticationStateProvider : AuthenticationStateProvider
    {
        public TestAuthenticationStateProvider(Task<AuthenticationState> state)
        {
            this.CurrentAuthStateTask = state;
        }

        public TestAuthenticationStateProvider()
        {
        }

        public Task<AuthenticationState> CurrentAuthStateTask { get; set; }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return CurrentAuthStateTask;
        }

        public void TriggerAuthenticationStateChanged(Task<AuthenticationState> authState)
        {
            this.NotifyAuthenticationStateChanged(authState);
        }

        public static Task<AuthenticationState> CreateAuthenticationState(string username)
        {
            var identity = new TestIdentity { Name = username };
            var principal = new ClaimsPrincipal(identity);
            return Task.FromResult(new AuthenticationState(principal));
        }

        public static Task<AuthenticationState> CreateUnauthenticationState()
        {
            var principal = new ClaimsPrincipal(new NonAuthenticatedPrincipal());
            return Task.FromResult(new AuthenticationState(principal));
        }
    }
    public class TestAuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        private readonly AuthorizationOptions options = new AuthorizationOptions();

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
            => Task.FromResult(options.DefaultPolicy);

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync()
            => Task.FromResult(options.FallbackPolicy);

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName) => Task.FromResult(
            new AuthorizationPolicy(new[]
            {
                    new TestPolicyRequirement { PolicyName = policyName }
            },
            new[] { $"TestScheme:{policyName}" }));
    }

    public class TestPolicyRequirement : IAuthorizationRequirement
    {
        public string PolicyName { get; set; }
    }

    public class TestAuthorizationService : IAuthorizationService
    {
        public AuthorizationResult NextResult { get; set; }
            = AuthorizationResult.Success();

        public List<(ClaimsPrincipal user, object resource, IEnumerable<IAuthorizationRequirement> requirements)> AuthorizeCalls { get; }
            = new List<(ClaimsPrincipal user, object resource, IEnumerable<IAuthorizationRequirement> requirements)>();

        public Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object resource, IEnumerable<IAuthorizationRequirement> requirements)
        {
            AuthorizeCalls.Add((user, resource, requirements));

            // The TestAuthorizationService doesn't actually apply any authorization requirements
            // It just returns the specified NextResult, since we're not trying to test the logic
            // in DefaultAuthorizationService or similar here. So it's up to tests to set a desired
            // NextResult and assert that the expected criteria were passed by inspecting AuthorizeCalls.
            return Task.FromResult(NextResult);
        }

        public Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object resource, string policyName)
            => throw new NotImplementedException();
    }

    public class TestIdentity : IIdentity
    {
        public string AuthenticationType => "Test";

        public bool IsAuthenticated => true;

        public string Name { get; set; }
    }

    public class NonAuthenticatedPrincipal : IPrincipal
    {
        public IIdentity Identity => null;
        public bool IsInRole(string role)
        {
            return false;
        }

    }

    public static class AuthExtensions
    {
        public static void AddAuthenticationServices(this TestServiceProvider services, Task<AuthenticationState> initialAuthState = null, AuthorizationResult nextAuth = null)
        {
            if (nextAuth == null) nextAuth = AuthorizationResult.Success();
            services.AddSingleton<IAuthorizationService>(new TestAuthorizationService() { NextResult = nextAuth });
            services.AddSingleton<IAuthorizationPolicyProvider>(new TestAuthorizationPolicyProvider());
            services.AddSingleton<AuthenticationStateProvider>(new TestAuthenticationStateProvider(initialAuthState));
        }
    }
}