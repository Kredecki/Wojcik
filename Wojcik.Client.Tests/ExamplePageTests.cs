using Bunit;
using Microsoft.Extensions.DependencyInjection;
using RichardSzalay.MockHttp;
using System.Net.Http.Json;
using Wojcik.Client.Pages;
using Wojcik.Shared.Dtos;
using Xunit;

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
	public async Task ExampleComponent_WhenButtonClicked_UpdatesModel()
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

		mockHttp.When("Api/Example/Get")
			.Respond("application/json", JsonContent.Create(exampleDto).ReadAsStringAsync().Result);

		// Register mock HTTP client in the service collection
		Services.AddScoped(sp => new HttpClient(mockHttp));

		// Render the component
		var component = RenderComponent<Example>();

		// Act
		// Simulate clicking the button
		component.Find("button").Click();

		// Allow async operations to complete
		await Task.Delay(100);

		// Assert
		// Verify that the model's values are displayed correctly
		component.Find("p:nth-of-type(1)").MarkupMatches("<p>test-guid</p>");
		component.Find("p:nth-of-type(2)").MarkupMatches("<p>Test Name</p>");
		component.Find("p:nth-of-type(3)").MarkupMatches("<p>Test Description</p>");
	}
}
