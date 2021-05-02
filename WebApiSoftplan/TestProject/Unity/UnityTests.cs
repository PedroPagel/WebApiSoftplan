using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.CalculoJuros;
using Services.ShowMeTheCode;
using Services.TaxaJuros;
using System;

namespace TestProject.Unity
{
    [TestClass]
    public class UnityTests
    {
        private readonly IShowMeTheCode _showMeTheCode;
        private readonly ITaxaJurosService _taxaJurosService;
        private readonly ICalculoJurosService _calculoJurosService;

        public UnityTests()
        {
            _showMeTheCode = new ShowMeTheCode();
            _taxaJurosService = new TaxaJurosService();
            _calculoJurosService = new CalculoJurosService();
        }

        [TestMethod]
        public void Verificar_Path_Github()
        {
            var path = _showMeTheCode.GetGitPath();

            Assert.IsTrue(path.Equals("https://github.com/PedroPagel/WebApiSoftplan"), "Path do github incorreto");
        }

        [TestMethod]
        public void Retorno_da_taxa_deve_ser_um()
        {
            var taxa = _taxaJurosService.RetonarTaxaJuros();

            Assert.IsTrue(taxa == 1, "Taxa está com o valor incorreto no retorno");
        }

        [TestMethod]
        public void Calcular_jusros_composto_esperando_valor_correto()
        {
            var valorComposto = _calculoJurosService.GerarCalculo(100, 5, 1);

            Assert.IsTrue(valorComposto == 105.1, "Valor calculado está incorreto.");
        }

        [TestMethod]
        public void Calcular_jusros_composto_esperando_valor_inicial_incorreto()
        {
            try
            {
                double valorComposto = _calculoJurosService.GerarCalculo(0, 5, 1);
            } catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Valor não informado corretamente"), "Esperava valor inicial incorreto");
            }
        }

        [TestMethod]
        public void Calcular_jusros_composto_esperando_tempo_incorreto()
        {
            try
            {
                double valorComposto = _calculoJurosService.GerarCalculo(110, 0, 1);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Tempo não informado corretamente"), "Tempo esperava erro");
            }
        }

        [TestMethod]
        public void Calcular_jusros_composto_esperando_taxa_incorreta()
        {
            try
            {
                double valorComposto = _calculoJurosService.GerarCalculo(100, 5, 0);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Taxa não informada corretamente"), "Taxa esperava erro");
            }
        }
    }
}
