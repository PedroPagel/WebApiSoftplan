namespace Services.TaxaJuros
{
    public class TaxaJurosService : ITaxaJurosService
    {
        private const double TAXA = 1;

        public double RetonarTaxaJuros() => TAXA;
    }
}
