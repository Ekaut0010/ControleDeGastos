namespace ControleGastos.Api.DTOs;

/// <summary>
/// Totais financeiros de uma pessoa.
/// </summary>
public class PessoaTotalDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public decimal TotalReceitas { get; set; }

    public decimal TotalDespesas { get; set; }

    public decimal Saldo => TotalReceitas - TotalDespesas;
}