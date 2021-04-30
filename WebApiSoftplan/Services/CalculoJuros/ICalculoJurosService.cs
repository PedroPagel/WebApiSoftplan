using System.Threading.Tasks;

namespace Services.CalculoJuros
{
    public interface ICalculoJurosService
    {
        Task<double> GerarCalculo(double valorInicial, int tempo);
    }
}
