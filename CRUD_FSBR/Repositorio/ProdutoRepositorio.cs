using CRUD_FSBR.Data;
using CRUD_FSBR.Models;

namespace CRUD_FSBR.Repositorio
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly BancoContext _bancoContext;
        public ProdutoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public ProdutoModel Adicionar(ProdutoModel produto)
        {
            _bancoContext.Produto.Add(produto);
            _bancoContext.SaveChanges();
            return produto;
        }

        public ProdutoModel Alterar(ProdutoModel produto)
        {
            ProdutoModel? produtoDB = BuscarPorId(produto.Id);
            if(produtoDB == null)
            {
                throw new Exception("Erro ao atualizar. Produto não encontrado");
            }
            produtoDB.Nome = produto.Nome;
            produtoDB.Descricao = produto.Descricao;
            produtoDB.Preco = produto.Preco;
            produtoDB.QuantidadeEstoque = produto.QuantidadeEstoque;

            _bancoContext.Update(produtoDB);
            _bancoContext.SaveChanges();

            return produtoDB;
        }

        public ProdutoModel? BuscarPorId(int id)
        {
            return _bancoContext.Produto.FirstOrDefault(x => x.Id == id);
        }

        public List<ProdutoModel> ListarTodos()
        {
            return _bancoContext.Produto.ToList();
        }

        public void Remover(int id)
        {
            var produto = BuscarPorId(id);
            if (produto != null)
            {
                _bancoContext.Produto.Remove(produto);
                _bancoContext.SaveChanges();
            }
        }
    }
}
