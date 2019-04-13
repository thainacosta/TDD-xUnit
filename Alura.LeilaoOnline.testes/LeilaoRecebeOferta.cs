using Alura.LeilaoOnline.Core;
using System.Linq;
using Xunit;

namespace Alura.LeilaoOnline.Testes
{
    public class LeilaoRecebeOferta
    {
        [Theory]
        [InlineData(4, new double[] { 1100, 1200, 1400, 1300 })]
        [InlineData(2, new double[] {800, 900 })] 
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(
            int qtdeEsperada, double[] ofertas)
        {
            //Arrange
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("maria", leilao);
            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if ((i%2) == 0)
                {
                    leilao.RecebeLance(fulano, valor);
                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }
            }
            leilao.TerminaPregao();

            //Act
            leilao.RecebeLance(fulano, 1000);

            //Assert
            var qtdObtida = leilao.Lances.Count();

            Assert.Equal(qtdeEsperada, qtdObtida);
        }
    }
}
