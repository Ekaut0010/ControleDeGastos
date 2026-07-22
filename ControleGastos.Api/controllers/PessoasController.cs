using ControleGastos.Api.DTOs;
using ControleGastos.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoasController : ControllerBase
{
    private readonly PessoaService _service;

    public PessoasController(PessoaService service)
    {
        _service = service;
    }

    /// <summary>
    /// Retorna todas as pessoas cadastradas.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var pessoas = await _service.ListarAsync();
        return Ok(pessoas);
    }

    /// <summary>
    /// Cadastra uma nova pessoa.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Criar(CriarPessoaDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var pessoa = await _service.CriarAsync(dto);

        return CreatedAtAction(nameof(Listar), new { id = pessoa.Id }, pessoa);
    }

    /// <summary>
    /// Remove uma pessoa.
    /// Todas as transações vinculadas também serão removidas.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var removido = await _service.ExcluirAsync(id);

        if (!removido)
            return NotFound("Pessoa não encontrada.");

        return NoContent();
    }
}