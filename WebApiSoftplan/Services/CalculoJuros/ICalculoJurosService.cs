using System.Threading.Tasks;

namespace Services.CalculoJuros
{
    public interface ICalculoJurosService
    {
        double GerarCalculo(double valorInicial, int tempo, double taxa);
    }
}
