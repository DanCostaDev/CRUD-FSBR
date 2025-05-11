using CRUD_FSBR.Models;
using CRUD_FSBR.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD_FSBR.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        public ProdutoController(IProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }
        public IActionResult Index()
        {
            List<ProdutoModel> produtos = _produtoRepositorio.ListarTodos();
            return View(produtos);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Update(int id)
        {
            var produto = _produtoRepositorio.BuscarPorId(id);
            if (produto == null) return NotFound();

            return PartialView("UpdatePartialView", produto);
        }

        [HttpPost]
        public IActionResult Create(ProdutoModel produto)
        {
            _produtoRepositorio.Adicionar(produto);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(ProdutoModel produto)
        {
            if (ModelState.IsValid)
            {
                _produtoRepositorio.Alterar(produto);
                return RedirectToAction("Index");
            }
            return View(produto);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var produto = _produtoRepositorio.BuscarPorId(id);
            if (produto == null)
            {
                return NotFound();
            }

            if (produto.QuantidadeEstoque > 0)
            {
                TempData["Erro"] = "O produto não pode ser excluído porque ainda possui itens em estoque.";
                return RedirectToAction("Index");
            }

            _produtoRepositorio.Remover(id);
            return RedirectToAction("Index");
        }

    }
}
