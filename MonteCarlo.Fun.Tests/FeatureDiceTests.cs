namespace MonteCarlo.Fun.Tests;

public class FeatureDiceTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void DiceWithOneProbabilityShouldReturn1()
    {
        var dice = new FeatureDice(1.0, new Dictionary<int, double>() { {1, 0.6}, {2, 0.25}, {3, 0.15}});
        var countOne = 0;
        var countTwo = 0;
        var countThree = 0;
        var count = 1000000;
        for (var i = 0; i < count; i++)
        {
            var result = dice.Check();
            if (result == 1) countOne += 1;
            if (result == 2) countTwo += 1;
            if (result == 3) countThree += 1;
        }
        Console.WriteLine("Ones: {0}, Twos: {1}, Threes: {2}", countOne, countTwo, countThree);
        
        Assert.That(countOne, Is.EqualTo(0.6 * count).Within(0.01 * 0.6 * count));
        Assert.That(countTwo, Is.EqualTo(0.25 * count).Within(0.01 * 0.25 * count));
        Assert.That(countThree, Is.EqualTo(0.15 * count).Within(0.01 * 0.15 * count));
    }
}