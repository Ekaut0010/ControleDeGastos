using ControleGastos.Api.DTOs;
using ControleGastos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Api.Services;

/// <summary>
/// Contém todas as regras de negócio relacionadas às pessoas.
/// </summary>
public class PessoaService
{
    private readonly AppDbContext _context;

    public PessoaService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Lista todas as pessoas cadastradas.
    /// </summary>
    public async Task<List<Pessoa>> ListarAsync()
    {
        return await _context.Pessoas.ToListAsync();
    }

    /// <summary>
    /// Cadastra uma nova pessoa.
    /// </summary>
    public async Task<Pessoa> CriarAsync(CriarPessoaDto dto)
    {
        var pessoa = new Pessoa
        {
            Nome = dto.Nome,
            Idade = dto.Idade
        };

        _context.Pessoas.Add(pessoa);

        await _context.SaveChangesAsync();

        return pessoa;
    }

    /// <summary>
    /// Exclui uma pessoa.
    /// As transações são removidas automaticamente pelo Cascade Delete.
    /// </summary>
    public async Task<bool> ExcluirAsync(int id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);

        if (pessoa == null)
            return false;

        _context.Pessoas.Remove(pessoa);

        await _context.SaveChangesAsync();

        return true;
    }
}