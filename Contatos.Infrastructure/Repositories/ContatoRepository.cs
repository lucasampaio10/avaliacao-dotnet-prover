using Contatos.Domain.Entities;
using Contatos.Domain.Interfaces;
using Contatos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Contatos.Infrastructure.Repositories;

public class ContatoRepository : IContatoRepository
{
    private readonly AppDbContext _context;

    public ContatoRepository(AppDbContext context)
    {
        _context = context;
    }

    // perguntar o pq que ta usando o FirstOrDefaultAsync
    public async Task<Contato?> ObterPorIdAsync(Guid id)
    {
        return await _context.Contatos
            .Where(c => c.Ativo)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    // vai perguntar o pq uma List
    public async Task<IEnumerable<Contato>> ListarAtivosAsync()
    {
        return await _context.Contatos
            .Where(c => c.Ativo)
            .ToListAsync();
    }

    public async Task AdicionarAsync(Contato contato)
    {
        await _context.Contatos.AddAsync(contato);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Contato contato)
    {
        _context.Contatos.Update(contato);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Contato contato)
    {
        _context.Contatos.Remove(contato);
        await _context.SaveChangesAsync();
    }
}
