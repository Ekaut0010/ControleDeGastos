namespace ControleGastos.Api.DTOs;

/// <summary>
/// Retorno da consulta de totais.
/// </summary>
public class TotaisDto
{
    public List<PessoaTotalDto> Pessoas { get; set; } = new();

    public decimal TotalReceitas { get; set; }

    public decimal TotalDespesas { get; set; }

    public decimal Saldo => TotalReceitas - TotalDespesas;
}