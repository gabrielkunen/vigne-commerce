using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using VigneCommerce.Api.Filters;
using VigneCommerce.Api.Request;
using VigneCommerce.Api.Response;
using VigneCommerce.Api.Response.Base;
using VigneCommerce.Domain.Entities;
using VigneCommerce.Domain.Interfaces.Repository;

namespace VigneCommerce.Api.Controllers
{
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("pedidos")]
    public class PedidoController(IProdutoRepository produtoRepository, IPedidoRepository pedidoRepository) : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;

        [HttpPost]
        [CustomAuthorize(Roles = ["Administrador", "Comum"])]
        [ProducesResponseType(typeof(ResponseContentBase<CadastrarPedidoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Cadastrar([FromBody] CadastrarPedidoRequest request)
        {
            var enderecoPagamento = new PedidoEndereco(request.EnderecoEntrega.Cep, request.EnderecoEntrega.DescricaoEndereco, request.EnderecoEntrega.Bairro, request.EnderecoEntrega.Numero);
            var usuarioId = Convert.ToInt32(User.FindFirst("IdUsuario")!.Value);
            var pedido = new Pedido(request.FormaPagamento);
            pedido.AdicionarEnderecoEntrega(enderecoPagamento);
            pedido.SetarUsuarioRealizouPedido(usuarioId);

            foreach (var produtoRequest in request.IdProdutos)
            {
                var produto = await _produtoRepository.BuscarPorId(produtoRequest.IdProduto);
                if (produto == null)
                    return BadRequest(new ResponseBase(false, $"Produto com id {produtoRequest.IdProduto} não cadastrado no sistema."));

                if (produto.QuantidadeEstoque < produtoRequest.Quantidade)
                    return BadRequest(new ResponseBase(false, $"Produto com id {produtoRequest.IdProduto} sem estoque suficiente para a compra."));

                pedido.AdicionarItemAoPedido(new PedidoItem(produto.Nome, produto.Valor, produtoRequest.Quantidade));
            }

            var pedidoId = await _pedidoRepository.Adicionar(pedido);

            foreach (var produtoRequest in request.IdProdutos)
                await _produtoRepository.DebitarEstoque(produtoRequest.IdProduto, produtoRequest.Quantidade);

            return Created("/pedidos", new ResponseContentBase<CadastrarPedidoResponse>(true, $"Pedido Id {pedidoId} cadastrado com sucesso", new CadastrarPedidoResponse(pedidoId)));
        }
    }
}
