using System.Numerics;

namespace MonteCarlo.Fun;

public class MonteCarlo<T> where T : INumber<T>
{
    private readonly int _countIterations;
    private readonly ISimulation<T> _simulation;

    public MonteCarlo(int n, ISimulation<T> simulation)
    {
        _countIterations = n;
        _simulation = simulation;
    }
 
    public double GetAvg(Dictionary<string, string> parameters)
    {
        T sum = T.Zero;
        for (var i = 0; i < _countIterations; i++)
        {
            var val = _simulation.Run(parameters);

            sum += val;
        }

        return Convert.ToDouble(sum) / _countIterations;
    }
    
    public double ThreadGetAvg(Dictionary<string, string> parameters)
    {
        T sum = T.Zero;
        object tMut = new object();
        var cpuCount = 4;
        var chunkSize = _countIterations / cpuCount;
        var threads = new List<Thread>();
        for (var cpuIndex = 0; cpuIndex < cpuCount; cpuIndex++)
        {
            var t = new Thread(() =>
            {
                T sumIn = T.Zero;
                for (var i = 0; i < chunkSize; i++)
                {
                    var val = _simulation.Run(parameters);

                    sumIn += val;
                }

                lock (tMut)
                {
                    sum += sumIn;
                }
            });
            t.Start();
            threads.Add(t);
        }
        
        threads.ForEach(t => t.Join());

        return Convert.ToDouble(sum) / _countIterations;
    }
    
    public async Task<double> TaskGetAvg(Dictionary<string, string> parameters)
    {
        T sum = T.Zero;
        
        var cpuCount = 20;
        var chunkSize = _countIterations / cpuCount;
        var taskList = new List<Task<T>>();
        for (var cpuIndex = 0; cpuIndex < cpuCount; cpuIndex++)
        {
            var t = new Task<T>(() =>
            {
                T sumIn = T.Zero;
                var i = 0;
                while (i++ < chunkSize)
                {
                    sumIn += _simulation.Run(parameters);
                }

                return sumIn;
            });
            t.Start();
            taskList.Add(t);
        }

        var results = await Task.WhenAll(taskList);
        foreach (var result in results)
        {
            sum += result;
        }

        return Convert.ToDouble(sum) / _countIterations;
    }
}