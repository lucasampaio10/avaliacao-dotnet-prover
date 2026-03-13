using Contatos.Domain.Enums;

namespace Contatos.Application.DTOs;

public class ContatoResponseDto
{
    public Guid Id{get;set;}
    public string Nome {get; set;} = string.Empty;
    public DateTime DataNascimento {get; set;}
    public Sexo Sexo{get; set;}
    public int Idade { get; set; }
}