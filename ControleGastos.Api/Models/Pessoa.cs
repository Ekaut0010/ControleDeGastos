using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Api.Models;

/// <summary>
/// Representa uma pessoa cadastrada no sistema.
/// Cada pessoa pode possuir várias transações.
/// </summary>
public class Pessoa
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    public int Idade { get; set; }

    // Navegação para as transações da pessoa
    public List<Transacao> Transacoes { get; set; } = new();
}