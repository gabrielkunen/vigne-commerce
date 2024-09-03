using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using VigneCommerce.Api.Request;
using VigneCommerce.Api.Response.Base;
using VigneCommerce.Api.Response;
using VigneCommerce.Domain.Entities;
using VigneCommerce.Domain.Enums;
using VigneCommerce.Application.Interfaces;
using VigneCommerce.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Authorization;

namespace VigneCommerce.Api.Controllers
{
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("usuarios")]
    public class UsuarioController(ITokenAppService tokenAppService, IUsuarioRepository usuarioRepository, IConfiguration configuration) : ControllerBase
    {
        private readonly ITokenAppService _tokenAppService = tokenAppService;
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly IConfiguration _configuration = configuration;

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseContentBase<CadastrarUsuarioResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Cadastrar([FromBody] CadastrarUsuarioRequest request)
        {
            var usuarioJaExiste = _usuarioRepository.JaExiste(request.Email);
            if (usuarioJaExiste)
                return BadRequest(new ResponseBase(false, $"Usuário com email {request.Email} já se encontra cadastrado no sistema."));

            var senhaHash = _tokenAppService.GerarSenha(request.Senha);

            var usuario = new Usuario(request.Nome, request.Email, senhaHash, ECargoUsuario.Comum);
            var idUsuario = await _usuarioRepository.Adicionar(usuario);

            return Created("/usuarios", new ResponseContentBase<CadastrarUsuarioResponse>(true, $"Usuário Id {idUsuario} cadastrado com sucesso", new CadastrarUsuarioResponse(idUsuario)));
        }

        [HttpPost("/login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseContentBase<LoginUsuarioResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioRequest request)
        {
            var usuario = await _usuarioRepository.BuscarPorEmail(request.Email);
            if (usuario == null)
                return BadRequest(new ResponseBase(false, $"Email ou senha incorretos"));

            var senhaValida = _tokenAppService.SenhaValida(request.Senha, usuario.Senha);
            if (!senhaValida)
                return BadRequest(new ResponseBase(false, $"Email ou senha incorretos"));

            var tokenDto = _tokenAppService.GerarToken(usuario, _configuration["ChaveToken"]!);

            return Ok(new ResponseContentBase<LoginUsuarioResponse>(true, "Login realizado com sucesso", new LoginUsuarioResponse(tokenDto.Token, tokenDto.DataExpiracao)));
        }
    }
}
