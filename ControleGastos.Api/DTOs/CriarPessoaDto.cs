using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Api.DTOs;

/// <summary>
/// Dados necessários para cadastrar uma nova pessoa.
/// </summary>
public class CriarPessoaDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Range(0, 120)]
    public int Idade { get; set; }
}