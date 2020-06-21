using Xunit;
using Bunit;
using static Bunit.ComponentParameterFactory;
using Beam.Tests.Auth;
using Beam.Client.Pages;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Authorization;

namespace Beam.Tests
{
    /// <summary>
    /// These tests are written entirely in C#.
    /// Learn more at https://bunit.egilhansen.com/docs/
    /// </summary>
    public class IndexPageTests : TestContext
    {
        [Fact]
        public void CounterStartsAtZero()
        {
            Services.AddAuthenticationServices(TestAuthenticationStateProvider.CreateAuthenticationState("TestUser"));

            var wrapper = RenderComponent<CascadingAuthenticationState>(p => p.AddChildContent<Beam.Client.Pages.Index>());
            // Arrange
            var cut = wrapper.FindComponent<Beam.Client.Pages.Index>();

            // Assert that content of the paragraph shows counter at zero
            Assert.Contains("Select a Frequency, or create a new one", cut.Markup);
            cut.MarkupMatches(@"<h2 diff:ignore></h2>
                                <p diff:ignore></p>"); 
        }


        [Fact]
        public void IndexShowsSimpleHeaderWhenUnAuthorized()
        {
            Services.AddAuthenticationServices(TestAuthenticationStateProvider.CreateUnauthenticationState(), AuthorizationResult.Failed());

            Services.AddScoped<NavigationManager, MockNavigationManager>();

            var wrapper = RenderComponent<CascadingAuthenticationState>(parameters => parameters.AddChildContent<Index>());

            // Arrange
            var cut = wrapper.FindComponent<Index>();
            
            // Assert that content of the paragraph shows counter at zero
            Assert.DoesNotContain("Select a Frequency, or create a new one", cut.Markup);
            
            cut.Find("a").MarkupMatches(@"<a href=""/login"">Log In</a>");
        }

    }
}