using Contatos.Domain.Entities;
using Contatos.Domain.Enums;
using Contatos.Domain.Exceptions;
using FluentAssertions;

namespace Contatos.Tests;

public class ContatoEntityTests
{
    [Fact]
    public void DeveCriarContatoValido()
    {
        var contato = new Contato("Lucas", new DateTime(1990, 1, 1), Sexo.Masculino);

        contato.Nome.Should().Be("Lucas");
        contato.Ativo.Should().BeTrue();
        contato.Idade.Should().BeGreaterThan(0);
    }

    [Fact]
    public void DeveLancarExcecaoQuandoNomeVazio()
    {
        var acao = () => new Contato("", new DateTime(1990, 1, 1), Sexo.Masculino);

        acao.Should().Throw<DomainException>()
            .WithMessage("Nome é obrigatório.");
    }

    [Fact]
    public void DeveLancarExcecaoQuandoMenorDeIdade()
    {
        var acao = () => new Contato("Lucas", DateTime.Today.AddYears(-17), Sexo.Masculino);

        acao.Should().Throw<DomainException>()
            .WithMessage("O contato deve ser maior de idade.");
    }

    [Fact]
    public void DeveLancarExcecaoQuandoDataNascimentoFutura()
    {
        var acao = () => new Contato("Lucas", DateTime.Today.AddDays(1), Sexo.Masculino);

        acao.Should().Throw<DomainException>()
            .WithMessage("Data de nascimento não pode ser maior que a data de hoje.");
    }

    [Fact]
    public void DeveDesativarContato()
    {
        var contato = new Contato("Lucas", new DateTime(1990, 1, 1), Sexo.Masculino);

        contato.Desativar();

        contato.Ativo.Should().BeFalse();
    }
}
