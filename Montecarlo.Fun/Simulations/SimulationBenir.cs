namespace MonteCarlo.Fun.Simulations;

public class SimulationBenir : ISimulation<long>
{
    private readonly Dictionary<int, Dice> _chances = new()
    {
        { 2, new Dice(0.5) },
        { 3, new Dice(0.45) },
        { 4, new Dice(0.4) },
        { 5, new Dice(0.35) },
        { 6, new Dice(0.5) },
        { 7, new Dice(0.45) },
        { 8, new Dice(0.4) },
        { 9, new Dice(0.35) },
        { 10, new Dice(0.3) },
        { 11, new Dice(0.25) },
        { 12, new Dice(0.5) },
        { 13, new Dice(0.35) },
        { 14, new Dice(0.3) },
        { 15, new Dice(0.25) },
        { 16, new Dice(0.20) },
        { 17, new Dice(0.15) },
        { 18, new Dice(0.5) },
        { 19, new Dice(0.35) },
        { 20, new Dice(0.3) },
        { 21, new Dice(0.25) },
        { 22, new Dice(0.20) },
        { 23, new Dice(0.15) },
        { 24, new Dice(0.5) },
    };


    private int ParseIntParameters(Dictionary<string, string> parameters, string parameter, int defaultValue)
    {
        parameters.TryGetValue(parameter, out var valueStr);
        if (valueStr is not null && int.TryParse(valueStr, out defaultValue)) { }

        return defaultValue;
    }

    public long Run(Dictionary<string, string> parameters)
    {
        var spentOskolkov = 0;
        var startLvl = ParseIntParameters(parameters, "startLvl", 1);
        var endLvl = ParseIntParameters(parameters, "endLvl", 24);

        var currentModifyLvl = startLvl;
        while (currentModifyLvl != endLvl)
        {
            spentOskolkov++;
            if (Modify(currentModifyLvl) == 1)
            {
                currentModifyLvl++;
            }
            else
            {
                if (currentModifyLvl < 6) currentModifyLvl = 1;
                if (currentModifyLvl < 12) currentModifyLvl = 6;
                if (currentModifyLvl < 18) currentModifyLvl = 12;
                if (currentModifyLvl < 24) currentModifyLvl = 18;
            }
        }

        return spentOskolkov;
    }

    private int Modify(int currentLvl)
    {
        return _chances[currentLvl + 1].Check();
    }
}