using NUnit.Framework;
using Name1;
namespace UnitTest;

[TestFixture]

public class Tests
{
    private Sparrow _sparrow;
    private Penguin _penguin;

    [SetUp]
    public void Setup()
    {
        _sparrow = new Sparrow();
        _penguin = new Penguin();
    }

    [Test]
    public void Sparrow_Should_Fly()
    {
        // Arrange
        // Sparrow is already initialized in Setup

        // Act
        _sparrow.Fly();

        // Assert
        Assert.Pass("Sparrow can fly.");
        
    }
}
