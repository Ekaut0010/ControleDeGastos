using ControleGastos.Api.DTOs;
using ControleGastos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Api.Services;

/// <summary>
/// Responsável pelos cálculos financeiros.
/// </summary>
public class TotalService
{
    private readonly AppDbContext _context;

    public TotalService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TotaisDto> ObterTotaisAsync()
    {
        var pessoas = await _context.Pessoas
            .Include(p => p.Transacoes)
            .ToListAsync();

        var resultado = new TotaisDto();

        foreach (var pessoa in pessoas)
        {
            var receitas = pessoa.Transacoes
                .Where(t => t.Tipo == TipoTransacao.Receita)
                .Sum(t => t.Valor);

            var despesas = pessoa.Transacoes
                .Where(t => t.Tipo == TipoTransacao.Despesa)
                .Sum(t => t.Valor);

            resultado.Pessoas.Add(new PessoaTotalDto
            {
                Id = pessoa.Id,
                Nome = pessoa.Nome,
                TotalReceitas = receitas,
                TotalDespesas = despesas
            });
        }

        resultado.TotalReceitas =
            resultado.Pessoas.Sum(p => p.TotalReceitas);

        resultado.TotalDespesas =
            resultado.Pessoas.Sum(p => p.TotalDespesas);

        return resultado;
    }
}