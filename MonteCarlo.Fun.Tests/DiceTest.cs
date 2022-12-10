namespace MonteCarlo.Fun.Tests;

public class DiceTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void DiceWithOneProbabilityShouldReturn1()
    {
        var dice = new Dice(1.0);
        var sum = 0;
        var count = 1000000;
        for (var i = 0; i < count; i++)
        {
            sum += dice.Check();
        }
        Assert.AreEqual(sum, count);
    }
    
    [Test]
    public void DiceWithZeroProbabilityShouldReturn0()
    {
        var dice = new Dice(0.0);
        var sum = 0;
        var count = 1000000;
        for (var i = 0; i < count; i++)
        {
            sum += dice.Check();
        }
        Assert.AreEqual(sum, 0);
    }
    
    [Test]
    public void DiceWithSixtyProbabilityMeanMustBeGoodToToleranceZeroPointSixty()
    {
        var dice = new Dice(0.6);
        double result = 0.0;
        var count = 1000000;
        for (var i = 0; i < count; i++)
        {
            result += dice.Check();
        }

        var mean = result / count;
        
        Assert.That(mean, Is.EqualTo(0.6).Within(0.01));
    }
    
    [Test]
    public void DiceWithFiveProbabilityMeanMustBeGoodToToleranceZeroPointZeroFive()
    {
        var dice = new Dice(0.05);
        double result = 0.0;
        var count = 1000000;
        for (var i = 0; i < count; i++)
        {
            result += dice.Check();
        }

        var mean = result / count;
        
        Assert.That(mean, Is.EqualTo(0.05).Within(0.01));
    }
}