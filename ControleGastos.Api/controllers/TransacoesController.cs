using ControleGastos.Api.DTOs;
using ControleGastos.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    private readonly TransacaoService _service;

    public TransacoesController(TransacaoService service)
    {
        _service = service;
    }

    /// <summary>
    /// Lista todas as transações cadastradas.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var transacoes = await _service.ListarAsync();
        return Ok(transacoes);
    }

    /// <summary>
    /// Cadastra uma nova transação.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Criar(CriarTransacaoDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var (sucesso, erro) = await _service.CriarAsync(dto);

        if (!sucesso)
            return BadRequest(new { mensagem = erro });

        return Ok(new { mensagem = "Transação cadastrada com sucesso!" });
    }
}