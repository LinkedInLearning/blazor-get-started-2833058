using System;
using Xunit;
using Bunit;
using Bunit.Mocking.JSInterop;
using static Bunit.ComponentParameterFactory;

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
            // Arrange
            var cut = RenderComponent<Beam.Client.Pages.Index>();

            // Assert that content of the paragraph shows counter at zero
            Assert.Contains("Select a Frequency, or create a new one", cut.Markup);
            cut.MarkupMatches(@"<h2 diff:ignore></h2>
                                <p diff:ignore></p>"); 
        }

    }
}