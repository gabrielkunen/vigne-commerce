using VigneCommerce.Domain.Entities.Base;

namespace VigneCommerce.Domain.Entities
{
    public class Produto(string nome, string descricao, decimal valor, int quantidadeEstoque) : EntityBase
    {
        public int Id { get; private set; }
        public string Nome { get; private set; } = nome;
        public string Descricao { get; private set; } = descricao;
        public decimal Valor { get; private set; } = valor;
        public int QuantidadeEstoque { get; private set; } = quantidadeEstoque;

        public void AlterarDados(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
        }

        public void DebitarEstoque(int quantidade) 
        {
            QuantidadeEstoque -= quantidade;
        }
    }
}
