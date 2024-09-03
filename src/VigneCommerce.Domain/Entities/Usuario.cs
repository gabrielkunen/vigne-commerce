using VigneCommerce.Domain.Entities.Base;
using VigneCommerce.Domain.Enums;

namespace VigneCommerce.Domain.Entities
{
    public class Usuario(string nome, string email, string senha, ECargoUsuario cargo) : EntityBase
    {
        public int Id { get; private set; }
        public string Nome { get; private set; } = nome;
        public string Email { get; private set; } = email;
        public string Senha { get; private set; } = senha;
        public ECargoUsuario Cargo { get; private set; } = cargo;
        public List<Pedido> Pedidos { get; private set; }
    }
}
