using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public enum EstadoLeilao
    {
        LeilaoEmAndamento,
        LeilaoFinalizado,
        LeilaoAntesDoPregao
    }

    public class Leilao
    {
        public Interessada _ultimoCliente;
        private IList<Lance> _lances;
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }
        public EstadoLeilao Estado { get; private set; }
        public double ValorDestino { get; }

        public Leilao(string peca, double valorDestino = 0)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Estado = EstadoLeilao.LeilaoAntesDoPregao;
            ValorDestino = valorDestino;

        }

        private bool NovoLanceEhAceito(Interessada cliente, double valor)
        {
            return (Estado == EstadoLeilao.LeilaoEmAndamento)
                && (cliente != _ultimoCliente);
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if (NovoLanceEhAceito(cliente, valor))
            {
                _lances.Add(new Lance(cliente, valor));
                _ultimoCliente = cliente;
            }

        }

        public void IniciaPregao()
        {
            Estado = EstadoLeilao.LeilaoEmAndamento;
        }

        public void TerminaPregao()
        {
            if (Estado != EstadoLeilao.LeilaoEmAndamento)
            {
                throw new System.InvalidOperationException("Nao é possivel terminar o Pregao sem ele ter iniciado, por isso utilize o metodo IniciaPregao");
            }

            if (ValorDestino > 0)
            {
                //modalidade oferta superior mais proxima
                Ganhador = Lances.
                    Where(l => l.Valor > ValorDestino).
                    OrderBy(l => l.Valor).
                    FirstOrDefault();                   
            }
            else
            {
                //modalidade maior valor 
                Ganhador = Lances
               .DefaultIfEmpty(new Lance(null, 0))
               .OrderBy(l => l.Valor)
               .LastOrDefault();
            }

            Estado = EstadoLeilao.LeilaoFinalizado;
        }
    }
}
