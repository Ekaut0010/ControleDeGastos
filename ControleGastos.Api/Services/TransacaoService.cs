using ControleGastos.Api.DTOs;
using ControleGastos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Api.Services;

/// <summary>
/// Contém as regras de negócio relacionadas às transações.
/// </summary>
public class TransacaoService
{
    private readonly AppDbContext _context;

    public TransacaoService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Lista todas as transações cadastradas.
    /// </summary>
    public async Task<List<Transacao>> ListarAsync()
    {
        return await _context.Transacoes
            .Include(t => t.Pessoa)
            .ToListAsync();
    }

    /// <summary>
    /// Cadastra uma nova transação.
    /// </summary>
    public async Task<(bool Sucesso, string? Erro)> CriarAsync(CriarTransacaoDto dto)
    {
        // Verifica se a pessoa existe.
        var pessoa = await _context.Pessoas.FindAsync(dto.PessoaId);

        if (pessoa == null)
            return (false, "Pessoa não encontrada.");

        // Regra de negócio:
        // Menores de idade só podem possuir despesas.
        if (pessoa.Idade < 18 && dto.Tipo == TipoTransacao.Receita)
            return (false, "Menores de idade só podem cadastrar despesas.");

        var transacao = new Transacao
        {
            Descricao = dto.Descricao,
            Valor = dto.Valor,
            Tipo = dto.Tipo,
            PessoaId = dto.PessoaId
        };

        _context.Transacoes.Add(transacao);

        await _context.SaveChangesAsync();

        return (true, null);
    }
}