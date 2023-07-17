namespace PaymentAPI.Models.Base;

public abstract class Vendedor
{
  /// <example>1</example>
  [Required]
  public uint Id { get; set; }

  /// <example>12345678901</example>
  [Required]
  public string Cpf { get; set; }

  /// <example>Wellington</example>
  [Required]
  public string Nome { get; set; }

  /// <example>wellington@email.com</example>
  [Required]
  public string Email { get; set; }

  /// <example>912345678</example>
  [Required]
  public string Telefone { get; set; }
}