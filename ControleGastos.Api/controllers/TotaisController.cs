using ControleGastos.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TotaisController : ControllerBase
{
    private readonly TotalService _service;

    public TotaisController(TotalService service)
    {
        _service = service;
    }

    /// <summary>
    /// Retorna os totais financeiros de cada pessoa
    /// e o total geral.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        return Ok(await _service.ObterTotaisAsync());
    }
}