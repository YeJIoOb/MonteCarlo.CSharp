// See https://aka.ms/new-console-template for more information


using MonteCarlo.Fun;
using MonteCarlo.Fun.Simulations;

#region SimulationIgnis
    // var mcIgnis = new MonteCarlo<int>(2000000, new SimulationIgnis());
    //
    // var parameters = new Dictionary<string, string>()
    // {
    //     { "startLvl", "0" },
    //     { "endLvl", "6" },
    // };
    //
    // var avg = await mcIgnis.TaskGetAvg(parameters);
    //
    // // var avg = mcIgnis.GetAvg(parameters);
    //
    // // var avg = mcIgnis.ThreadGetAvg(parameters);
    //
    // Console.WriteLine(avg);
#endregion

#region SimulationBenir
    var mcBenir = new MonteCarlo<long>(2000000, new SimulationBenir());

    var parameters = new Dictionary<string, string>()
    {
        { "startLvl", "18" },
        { "endLvl", "24" },
    };

    // var avg = await mcBenir.TaskGetAvg(parameters);

    var avg = mcBenir.GetAvg(parameters);

    // var avg = mcBenir.ThreadGetAvg(parameters);

    Console.WriteLine(avg);
#endregion