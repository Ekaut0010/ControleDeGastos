using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleGastos.Api.Models;

/// <summary>
/// Representa uma movimentação financeira.
/// </summary>
public class Transacao
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Descricao { get; set; } = string.Empty;

    public decimal Valor { get; set; }

    public TipoTransacao Tipo { get; set; }

    [ForeignKey("Pessoa")]
    public int PessoaId { get; set; }

    public Pessoa? Pessoa { get; set; }
}