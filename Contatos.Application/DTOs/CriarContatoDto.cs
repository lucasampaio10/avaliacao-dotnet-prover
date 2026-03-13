using Contatos.Domain.Enums;

namespace Contatos.Application.DTOs;

public class CriarContatoDto
{
    public string Nome{get; set;} = string.Empty;
    public DateTime DataNascimento {get; set;}
    public Sexo Sexo {get; set;}
}

