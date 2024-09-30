using MediatR;
using Moq;
using Wojcik.Controllers;
using Wojcik.Shared.Dtos;
using Wojcik.Shared.Request.Example.Get;

namespace Wojcik.UnitTests;

[TestFixture]
public class ExampleControllerTests
{
	private Mock<IMediator> _mediatorMock;
	private ExampleController _controller;

	[SetUp]
	public void SetUp()
	{
		_mediatorMock = new Mock<IMediator>();
		_controller = new ExampleController(_mediatorMock.Object);
	}

	[TearDown]
	public void TearDown()
		=> _controller?.Dispose();
	
	[Test]
	public async Task Get_WhenCalled_ReturnsExampleDto()
	{
		// Arrange
		var exampleDto = new ExampleDto
		{
			GuId = Guid.NewGuid(),
			Name = "Test Name",
			Description = "Test Description"
		};
		_mediatorMock.Setup(m => m.Send(It.IsAny<GetQuery>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(exampleDto);

		// Act
		var result = await _controller.Get();

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.Multiple(() =>
		{
			Assert.That(result.GuId, Is.EqualTo(exampleDto.GuId));
			Assert.That(result.Name, Is.EqualTo(exampleDto.Name));
			Assert.That(result.Description, Is.EqualTo(exampleDto.Description));
		});
	}

	[Test]
	public void Get_WhenExceptionIsThrown_ThrowsException()
	{
		// Arrange
		_mediatorMock.Setup(m => m.Send(It.IsAny<GetQuery>(), It.IsAny<CancellationToken>()))
			.ThrowsAsync(new Exception("Test Exception"));

		// Act & Assert
		var exception = Assert.ThrowsAsync<Exception>(async () => await _controller.Get());
		Assert.That(exception.Message, Is.EqualTo("Test Exception"));
	}
}
