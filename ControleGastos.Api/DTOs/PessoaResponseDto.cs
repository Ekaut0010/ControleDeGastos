namespace ControleGastos.Api.DTOs;

using ControleGastos.Api.Models;

/// <summary>
/// Dados retornados ao listar uma transação.
/// </summary>
public class TransacaoResponseDto
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public TipoTransacao Tipo { get; set; }
    public int PessoaId { get; set; }
    public string NomePessoa { get; set; } = string.Empty; // Opcional: facilita exibir o nome da pessoa na tabela do front-end
}