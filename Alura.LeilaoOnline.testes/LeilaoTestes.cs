using Alura.LeilaoOnline.Core;
using Xunit;

namespace Alura.LeilaoOnline.Testes
{
    public class LeilaoTestes
    {
        [Theory]
        [InlineData(new double[] {800, 900, 990, 1000})]
        [InlineData(new double[] {800, 900, 1000, 880})]
        [InlineData(new double[] {1000})]
        public void LeilaoComVariosLances( double [] ofertas)
        {
            //Arrange
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            
            foreach (var valor in ofertas)
            {
                leilao.RecebeLance(fulano, valor);
            }

            //Act
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 1000;
            var valorObtido = (leilao.Ganhador.Valor);
            Assert.Equal(valorEsperado, valorObtido);
        }

       

        [Fact]
        public void LeilaoSemLance()
        {
            //Arrange
            var leilao = new Leilao("Van Gogh");

            //Act
            var valorEsperado = 0;
            leilao.TerminaPregao();

            //Assert            
            var valorObtido = (leilao.Ganhador.Valor);
        }
    }
}