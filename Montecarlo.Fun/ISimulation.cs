using System.Numerics;

namespace MonteCarlo.Fun;

public interface ISimulation<T> where T : INumber<T>
{
    T Run(Dictionary<string, string> parameters);
}