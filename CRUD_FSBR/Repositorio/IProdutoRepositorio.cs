using CRUD_FSBR.Models;

namespace CRUD_FSBR.Repositorio
{
    public interface IProdutoRepositorio
    {
        List<ProdutoModel> ListarTodos();
        ProdutoModel? BuscarPorId(int id);
        ProdutoModel Alterar(ProdutoModel produto);
        ProdutoModel Adicionar(ProdutoModel produto);
        void Remover(int id);

    }
}
