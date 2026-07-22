using ControleGastos.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Api.DTOs;

/// <summary>
/// Dados necessários para cadastrar uma transação.
/// </summary>
public class CriarTransacaoDto
{
    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [MaxLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
    public decimal Valor { get; set; }

    [Required(ErrorMessage = "O tipo da transação é obrigatório.")]
    public TipoTransacao Tipo { get; set; }

    [Required(ErrorMessage = "A pessoa associada é obrigatória.")]
    public int PessoaId { get; set; }
}