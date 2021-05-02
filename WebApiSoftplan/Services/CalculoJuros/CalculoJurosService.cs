using System;

namespace Services.CalculoJuros
{
    public class CalculoJurosService : ICalculoJurosService
    {
        public double GerarCalculo(double valorInicial, int tempo, double taxa)
        {
            if (valorInicial <= 0)
            {
                throw new Exception("Valor não informado corretamente");
            }

            if (tempo <= 0)
            {
                throw new Exception("Tempo não informado corretamente");
            }

            if (taxa <= 0)
            {
                throw new Exception("Taxa não informada corretamente");
            }

            var taxaMensal = taxa / 100;
            double valorFinal = valorInicial;
            double jurosMes;

            for (int meses = 0; meses < tempo; meses++)
            {
                jurosMes = taxaMensal * valorFinal;
                valorFinal += jurosMes;
            }

            return Math.Round(valorFinal, 2);
        }
    }
}
