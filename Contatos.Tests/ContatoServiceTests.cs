using Contatos.Application.DTOs;
using Contatos.Application.Services;
using Contatos.Domain.Entities;
using Contatos.Domain.Enums;
using Contatos.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Contatos.Tests;

public class ContatoServiceTests
{
    private readonly Mock<IContatoRepository> _repositoryMock;
    private readonly ContatoService _service;

    public ContatoServiceTests()
    {
        _repositoryMock = new Mock<IContatoRepository>();
        _service = new ContatoService(_repositoryMock.Object);
    }

    [Fact]
    public async Task DeveCriarContatoComSucesso()
    {
        var dto = new CriarContatoDto
        {
            Nome = "Lucas",
            DataNascimento = new DateTime(1990, 1, 1),
            Sexo = Sexo.Masculino
        };

        var resultado = await _service.CriarAsync(dto);

        resultado.Nome.Should().Be("Lucas");
        _repositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Contato>()), Times.Once);
    }

    [Fact]
    public async Task DeveLancarExcecaoQuandoContatoNaoEncontrado()
    {
        _repositoryMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Contato?)null);

        var acao = async () => await _service.ObterPorIdAsync(Guid.NewGuid());

        await acao.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task DeveDesativarContatoComSucesso()
    {
        var contato = new Contato("Lucas", new DateTime(1990, 1, 1), Sexo.Masculino);

        _repositoryMock.Setup(r => r.ObterPorIdAsync(contato.Id))
            .ReturnsAsync(contato);

        await _service.DesativarAsync(contato.Id);

        contato.Ativo.Should().BeFalse();
        _repositoryMock.Verify(r => r.AtualizarAsync(contato), Times.Once);
    }
}
