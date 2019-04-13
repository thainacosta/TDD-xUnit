using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alura.LeilaoOnline.Core
{
    public class OfertaSuperiorMaisProxima : IModalidadeAvaliacao
    {
        public double ValorDestino { get; }

        public OfertaSuperiorMaisProxima(double valorDestino)
        {
            ValorDestino = valorDestino;
        }
        
        Lance IModalidadeAvaliacao.Avalia(Leilao leilao)
        {
            return leilao.Lances.
                DefaultIfEmpty(new Lance(null, 0)).
                Where(l => l.Valor > ValorDestino).
                OrderBy(l => l.Valor).
                FirstOrDefault();
        }
    }
}
