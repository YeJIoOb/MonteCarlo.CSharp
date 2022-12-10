namespace MonteCarlo.Fun.Simulations;

public class SimulationIgnis : ISimulation<int>
{
    private readonly Dictionary<int, Dice> _chances = new()
    {
        { 1, new Dice(0.5) },
        { 2, new Dice(0.5) },
        { 3, new Dice(0.4) },
        { 4, new Dice(0.4) },
        { 5, new Dice(0.3) },
        { 6, new Dice(0.3) },
        { 7, new Dice(0.2) },
        { 8, new Dice(0.1) },
        { 9, new Dice(0.03) },
        { 10, new Dice(0.01) }
    };


    private int ParseIntParameters(Dictionary<string, string> parameters, string parameter, int defaultValue)
    {
        parameters.TryGetValue(parameter, out var valueStr);
        if (valueStr is not null && int.TryParse(valueStr, out defaultValue)) { }

        return defaultValue;
    }

    public int Run(Dictionary<string, string> parameters)
    {
        var spentLacks = 0;
        var startLvl = ParseIntParameters(parameters, "startLvl", 0);
        var endLvl = ParseIntParameters(parameters, "endLvl", 10);

        var currentIgnisLvl = startLvl;
        while (currentIgnisLvl != endLvl)
        {
            spentLacks++;
            if (Modify(currentIgnisLvl) == 1)
            {
                currentIgnisLvl++;
            }
            else
            {
                currentIgnisLvl = 0;
            }
        }

        return spentLacks;
    }

    private int Modify(int currentLvl)
    {
        return _chances[currentLvl + 1].Check();
    }
}