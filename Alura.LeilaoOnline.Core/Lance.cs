namespace Alura.LeilaoOnline.Core
{
    public class Lance
    {
        public Interessada Cliente { get; }
        public double Valor { get; }

        public Lance(Interessada cliente, double valor)
        {
            if (valor < 0)
            {
                throw new System.ArgumentException("O valor do Lance nao pode ser negativo");
            }
            Cliente = cliente;
            Valor = valor;
        }
    }
}
