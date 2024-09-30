using Bunit;
using Microsoft.Extensions.DependencyInjection;
using RichardSzalay.MockHttp;
using System.Net.Http.Json;
using Wojcik.Client.Pages;
using Wojcik.Shared.Dtos;

namespace Wojcik.Client.UnitTests;

public class ExamplePageTests : TestContext
{
	[Fact]
	public void ExamplePage_RendersCorrectly_Initially()
	{
		// Arrange
		// Render the Example component
		var component = RenderComponent<Example>();

		// Act & Assert
		// Ensure initial state of the component is as expected
		component.MarkupMatches(@"
            <h3>Example Page</h3>
            <p></p> <!-- GuId will be empty initially -->
            <p></p> <!-- Name will be empty initially -->
            <p></p> <!-- Description will be empty initially -->
            <button>GetExample</button>
        ");
	}

	[Fact]
	public async Task ExamplePage_WhenButtonClicked_UpdatesModel()
	{
		// Arrange
		// Mock the HTTP response with ExampleDto data
		var mockHttp = new MockHttpMessageHandler();
		var exampleDto = new ExampleDto
		{
			GuId = Guid.NewGuid(),
			Name = "Test Name",
			Description = "Test Description"
		};

		// Use JsonContent to serialize the ExampleDto to JSON
		var responseContent = JsonContent.Create(exampleDto);
        mockHttp.When("http://localhost/Api/Example/Get")
                .Respond(responseContent);

		// Register mock HTTP client in the service collection with a BaseAddress
		var client = new HttpClient(mockHttp)
		{
			BaseAddress = new Uri("http://localhost") // Set a dummy base address
		};
		Services.AddScoped(sp => client);

		// Render the component
		var component = RenderComponent<Example>();

		// Act
		// Simulate clicking the button
		component.Find("button").Click();

		// Allow async operations to complete
		await component.InvokeAsync(() => Task.Delay(100));

		// Assert
		// Verify that the model's values are displayed correctly
		component.WaitForAssertion(() =>
		{
			component.Find("p:nth-of-type(1)").MarkupMatches($"<p>{exampleDto.GuId}</p>");
			component.Find("p:nth-of-type(2)").MarkupMatches("<p>Test Name</p>");
			component.Find("p:nth-of-type(3)").MarkupMatches("<p>Test Description</p>");
		}, timeout: TimeSpan.FromSeconds(2));
	}
}
