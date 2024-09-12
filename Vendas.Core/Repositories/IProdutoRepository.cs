using Vendas.Core.Entities;

namespace Vendas.Core.Repositories
{
    public interface IProdutoRepository
    {
        Task<Produto> ObterPorId(Guid id);
        Task<List<Produto>> ObterTodos();
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto);
        Task Excluir(Guid id);
    }
}
