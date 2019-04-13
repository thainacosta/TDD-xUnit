using Alura.LeilaoOnline.Core;
using System;

namespace Alura.LeilaoOnline.ConsoleApp
{
    class Program
    {

        public static void Verifica(double esperado, double obtido)
        {
            if (esperado == obtido)
            {
                Console.WriteLine("Teste Ok");
                Console.Read();
               
            }
            else
            {
                Console.WriteLine(
                    $"Teste falhou!! Esperado: {esperado}, Obtido: {obtido}");
                
            }

        }
        public static void LeilaoComVariosLances()
        {
            //Arrange
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var Maria = new Interessada("Maria", leilao);

            leilao.RecebeLance(fulano, 800);
            leilao.RecebeLance(Maria, 900);
            leilao.RecebeLance(fulano, 1000);
            leilao.RecebeLance(Maria, 805);

            //Act
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 1000;
            var valorObtido = (leilao.Ganhador.Valor);
            Verifica(valorEsperado, valorObtido);
        }

        public static void LeilaoComUmLance()
        {
            //Arrange
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);            

            leilao.RecebeLance(fulano, 800);            

            //Act
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 800;
            var valorObtido = (leilao.Ganhador.Valor);
            Verifica(valorEsperado, valorObtido);

        }

        static void Main()
        {
            LeilaoComVariosLances();
            LeilaoComUmLance();

        }
    }
}
