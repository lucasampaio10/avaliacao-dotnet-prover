using Contatos.Application.DTOs;
using Contatos.Domain.Entities;
using Contatos.Domain.Interfaces;

namespace Contatos.Application.Services;

public class ContatoService
{
    private readonly IContatoRepository _repository;

    public ContatoService(IContatoRepository repository)
    {
        _repository = repository;
    }

    public async Task<ContatoResponseDto> CriarAsync(CriarContatoDto dto)
    {
        var contato = new Contato(dto.Nome, dto.DataNascimento, dto.Sexo);
        await _repository.AdicionarAsync(contato);
        return ToDto(contato);
    }

    public async Task<IEnumerable<ContatoResponseDto>> ListarAsync()
    {
        var contatos = await _repository.ListarAtivosAsync();
        return contatos.Select(ToDto);
    }

     public async Task<ContatoResponseDto> ObterPorIdAsync(Guid id)
    {
        var contato = await _repository.ObterPorIdAsync(id);
        if (contato == null)
            throw new KeyNotFoundException("Contato não encontrado.");
        return ToDto(contato);
    }

    public async Task AtualizarAsync(Guid id, CriarContatoDto dto)
    {
        var contato = await _repository.ObterPorIdAsync(id);
        if (contato == null)
            throw new KeyNotFoundException("Contato não encontrado.");

        contato.Atualizar(dto.Nome, dto.DataNascimento, dto.Sexo);
        await _repository.AtualizarAsync(contato);
    }

     public async Task DesativarAsync(Guid id)
    {
        var contato = await _repository.ObterPorIdAsync(id);
        if (contato == null)
            throw new KeyNotFoundException("Contato não encontrado.");

        contato.Desativar();
        await _repository.AtualizarAsync(contato);
    }

     public async Task ExcluirAsync(Guid id)
    {
        var contato = await _repository.ObterPorIdAsync(id);
        if (contato == null)
            throw new KeyNotFoundException("Contato não encontrado.");

        await _repository.RemoverAsync(contato);
    }

     private static ContatoResponseDto ToDto(Contato contato) => new()
    {
        Id = contato.Id,
        Nome = contato.Nome,
        DataNascimento = contato.DataNascimento,
        Sexo = contato.Sexo,
        Idade = contato.Idade
    };
}