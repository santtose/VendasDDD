using Microsoft.AspNetCore.Mvc;
using Vendas.Core.Entities;
using Vendas.Core.Repositories;

namespace VendasDDD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var produto = await _produtoRepository.ObterPorId(id);
            if (produto == null) return NotFound();
            return Ok(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Produto produto)
        {
            await _produtoRepository.Adicionar(produto);
            return CreatedAtAction(nameof(Get), new { id = produto.Id }, produto);
        }
    }
}
