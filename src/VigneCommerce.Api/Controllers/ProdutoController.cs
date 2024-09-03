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
    [Route("produtos")]
    public class ProdutoController(IProdutoRepository produtoRepository) : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;

        [HttpGet("{id}")]
        [CustomAuthorize(Roles = ["Administrador", "Comum"])]
        [ProducesResponseType(typeof(ResponseContentBase<BuscarProdutoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuscarPorId([FromRoute] int id)
        {
            var produto = await _produtoRepository.BuscarPorId(id);

            if (produto == null)
                return NotFound(new ResponseBase(false, $"Produto Id {id} não encontrado"));

            return Ok(new ResponseContentBase<BuscarProdutoResponse>(true, $"Produto Id {id} buscado com sucesso", new BuscarProdutoResponse(produto.Nome, produto.Descricao, produto.Valor, produto.QuantidadeEstoque)));
        }

        [HttpPost]
        [CustomAuthorize(Roles = ["Administrador"])]
        [ProducesResponseType(typeof(ResponseContentBase<CadastrarProdutoResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Cadastrar([FromBody] CadastrarProdutoRequest request)
        {
            var produto = new Produto(request.Nome, request.Descricao, request.Valor, request.QuantidadeEstoque);
            var idProduto = await _produtoRepository.Adicionar(produto);

            return Created("/produtos", new ResponseContentBase<CadastrarProdutoResponse>(true, $"Produto Id {idProduto} cadastrado com sucesso", new CadastrarProdutoResponse(idProduto)));
        }

        [HttpGet]
        [CustomAuthorize(Roles = ["Administrador", "Comum"])]
        [ProducesResponseType(typeof(ResponseContentBase<List<BuscarProdutoResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuscarPorId([FromQuery] int take = 10, int skip = 0)
        {
            var listaProdutos = new List<BuscarProdutoResponse>();
            var produtos = _produtoRepository.Buscar(take, skip);

            foreach (var produto in produtos)
                listaProdutos.Add(new BuscarProdutoResponse(produto.Nome, produto.Descricao, produto.Valor, produto.QuantidadeEstoque));

            return Ok(new ResponseContentBase<List<BuscarProdutoResponse>>(true, $"Produtos buscados com sucesso", listaProdutos));
        }

        [HttpPatch("{id}")]
        [CustomAuthorize(Roles = ["Administrador"])]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Alterar([FromRoute] int id, [FromBody] AlterarProdutoRequest request)
        {
            var produto = await _produtoRepository.BuscarPorId(id);

            if (produto == null)
                return NotFound(new ResponseBase(false, $"Produto Id {id} não encontrado"));

            produto.AlterarDados(request.Nome, request.Descricao);

            await _produtoRepository.Atualizar(produto);

            return Ok(new ResponseBase(true, $"Produto Id {id} atualizado com sucesso"));
        }
    }
}
