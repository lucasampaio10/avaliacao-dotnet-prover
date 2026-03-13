using Contatos.Domain.Enums;
using Contatos.Domain.Exceptions;

namespace Contatos.Domain.Entities;

public class Contato
{
    
    public Guid Id{get; private set;}
    public String Nome {get; private set;} = string.Empty;
    public DateTime DataNascimento {get; private set;}
    public Sexo Sexo {get; private set;}

    public bool Ativo{get; private set;}

    public int Idade
    {
        get
        {
            var hoje = DateTime.Today;
            var idade = hoje.Year - DataNascimento.Year;
            if(DataNascimento.Date > hoje.AddYears(-idade)) idade--;
            return idade;
        }
    }

    protected Contato(){}

    public Contato(String nome, DateTime dataNascimento, Sexo sexo)
    {
        if(string.IsNullOrWhiteSpace(nome))
            throw new DomainException("Nome é obrigatório.");

        if(dataNascimento > DateTime.Today)
            throw new DomainException("Data de nascimento não pode ser maior que a data de hoje.");

        Id = Guid.NewGuid();
        Nome = nome;
        DataNascimento = dataNascimento;
        Sexo = sexo;
        Ativo = true;
        
        ValidarIdade();
    }

    public void Atualizar(string nome, DateTime dataNascimento, Sexo sexo)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("Nome é obrigatório.");

        if (dataNascimento > DateTime.Today)
            throw new DomainException("Data de nascimento não pode ser maior que a data de hoje.");

        Nome = nome;
        DataNascimento = dataNascimento;
        Sexo = sexo;

        ValidarIdade();
    }

    public void Desativar() => Ativo = false;

    private void ValidarIdade()
    {
        if (Idade == 0)
            throw new DomainException("Idade não pode ser igual a 0.");

        if (Idade < 18)
            throw new DomainException("O contato deve ser maior de idade.");
    }
}