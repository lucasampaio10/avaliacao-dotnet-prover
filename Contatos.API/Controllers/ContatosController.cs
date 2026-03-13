using Contatos.Application.DTOs;
using Contatos.Application.Services;
using Contatos.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Contatos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContatosController : ControllerBase
{
    private readonly ContatoService _service;

    public ContatosController(ContatoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var contatos = await _service.ListarAsync();
        return Ok(contatos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var contato = await _service.ObterPorIdAsync(id);
        return Ok(contato);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarContatoDto dto)
    {
        var contato = await _service.CriarAsync(dto);
        return CreatedAtAction(nameof(ObterPorId), new { id = contato.Id }, contato);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] CriarContatoDto dto)
    {
        await _service.AtualizarAsync(id, dto);
        return NoContent();
    }

    [HttpPatch("{id}/desativar")]
    public async Task<IActionResult> Desativar(Guid id)
    {
        await _service.DesativarAsync(id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Excluir(Guid id)
    {
        await _service.ExcluirAsync(id);
        return NoContent();
    }
}
