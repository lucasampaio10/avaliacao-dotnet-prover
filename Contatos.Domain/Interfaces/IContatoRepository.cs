using Contatos.Domain.Entities;

namespace Contatos.Domain.Interfaces;


public interface IContatoRepository
{
    Task<Contato?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Contato>> ListarAtivosAsync();
    Task AdicionarAsync(Contato contato);
    Task AtualizarAsync(Contato contato);
    Task RemoverAsync(Contato contato);
}