﻿using Alura.LeilaoOnline.Core;
using System.Linq;
using Xunit;

namespace Alura.LeilaoOnline.Testes
{
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1200, 1250, new double[] { 800, 1150, 1400, 1250 })]
        public void RetornaValorSuperiorMaisProximoDadoLeilaoNessaModalidade(
            double valorDestino,
            double valorEsperado,
            double [] ofertas)
        {
            //Arrange
            IModalidadeAvaliacao modalidade = new OfertaSuperiorMaisProxima(valorDestino);
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("maria", leilao);

            leilao.IniciaPregao();
            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if ((i % 2) == 0)
                {
                    leilao.RecebeLance(fulano, valor);
                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }
            }

            //Act
            leilao.TerminaPregao();

            //Assert
            var valorObtido = (leilao.Ganhador.Valor);
            Assert.Equal(valorEsperado, valorObtido);



        }

        [Fact]
        public void NaoAceitaMesmoClienteFazerLAnceConsecutivo()
        {
            //Arrange
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            leilao.IniciaPregao();
            leilao.RecebeLance(fulano, 800);


            //Act - Metodo sob teste            
            leilao.RecebeLance(fulano, 1000);


            //Assert
            var qtdeEsperada = 1;
            var valorObtido = leilao.Lances.Count();
            Assert.Equal(qtdeEsperada, valorObtido);
        }
        [Theory]
        [InlineData(1000, new double[] { 800, 900, 990, 1000 })]
        [InlineData(1000, new double[] { 800, 900, 1000, 880 })]
        [InlineData(1000, new double[] { 1000 })]
        public void RetornaMaiorValorDadoLeilaoComNoMininoUmLance(double valorEsperado, double[] ofertas)
        {
            //Arraje
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("maria", leilao);

            leilao.IniciaPregao();
            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if ((i % 2) == 0)
                {
                    leilao.RecebeLance(fulano, valor);
                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }
            }

            //Act
            leilao.TerminaPregao();

            //Assert
            var valorObtido = (leilao.Ganhador.Valor);
            Assert.Equal(valorEsperado, valorObtido);

        }

        [Fact]
        public void LancaInvalidOperationExceptionDadoPregaoNaoIniciado()
        {
            //Arrange
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            

            var excecaoObtida = Assert.Throws<System.InvalidOperationException>(

                //Act 
                () => leilao.TerminaPregao()
            );

            var mensagemEsperada = "Nao é possivel terminar o Pregao sem ele ter iniciado, por isso utilize o metodo IniciaPregao";
            Assert.Equal(mensagemEsperada, excecaoObtida.Message);
        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            //Arrange
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            leilao.IniciaPregao();

            //Act
            var valorEsperado = 0;
            leilao.TerminaPregao();

            //Assert            
            var valorObtido = (leilao.Ganhador.Valor);
        }
    }
}