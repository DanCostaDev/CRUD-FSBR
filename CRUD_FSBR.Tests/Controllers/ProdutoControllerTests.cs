using CRUD_FSBR.Controllers;
using CRUD_FSBR.Models;
using CRUD_FSBR.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

public class ProdutoControllerTests
{
    [Fact]
    public void Index_DeveRetornarViewComListaDeProdutos()
    {
        var mockRepo = new Mock<IProdutoRepositorio>();
        var produtos = new List<ProdutoModel>
            {
            new ProdutoModel { Id = 1, Nome = "Camiseta", Preco = 59.90m },
            new ProdutoModel { Id = 2, Nome = "Calça", Preco = 89.90m }
            };
        mockRepo.Setup(r => r.ListarTodos()).Returns(produtos);

        var controller = new ProdutoController(mockRepo.Object);

        var resultado = controller.Index() as ViewResult;

        Assert.NotNull(resultado);
        var modelo = Assert.IsAssignableFrom<List<ProdutoModel>>(resultado.Model);
        Assert.Equal(2, modelo.Count);
    }

    [Fact]
    public void Create_DeveRetornarView()
    {
        var mockRepo = new Mock<IProdutoRepositorio>();
        var controller = new ProdutoController(mockRepo.Object);

        var resultado = controller.Create();

        Assert.IsType<ViewResult>(resultado);
    }


    [Fact]
    public void Update_DeveRetornarPartialView_QuandoProdutoExiste()
    {
        var mockRepo = new Mock<IProdutoRepositorio>();
        var produto = new ProdutoModel { Id = 1, Nome = "Produto Teste" };
        mockRepo.Setup(r => r.BuscarPorId(produto.Id)).Returns(produto);
        var controller = new ProdutoController(mockRepo.Object);

        var resultado = controller.Update(1);

        var viewResult = Assert.IsType<PartialViewResult>(resultado);
        Assert.Equal("UpdatePartialView", viewResult.ViewName);
        Assert.IsType<ProdutoModel>(viewResult.Model);
    }

    [Fact]
    public void Update_DeveRetornarNotFound_QuandoProdutoNaoExiste()
    {
        var mockRepo = new Mock<IProdutoRepositorio>();
        mockRepo.Setup(r => r.BuscarPorId(1)).Returns((ProdutoModel?)null);

        var controller = new ProdutoController(mockRepo.Object);

        var resultado = controller.Update(1);

        Assert.IsType<NotFoundResult>(resultado);
    }

    [Fact]
    public void CreatePost_DeveAdicionarProdutoERedirecionarParaIndex()
    {
        var mockRepo = new Mock<IProdutoRepositorio>();
        var controller = new ProdutoController(mockRepo.Object);

        var produto = new ProdutoModel 
        {   Id = 1, 
            Nome = "Produto Teste",
            Descricao = "Produto para testes",
            Preco = 2.99M,
            QuantidadeEstoque = 10
        };

        var resultado = controller.Create(produto);

        mockRepo.Verify(r => r.Adicionar(produto), Times.Once);
        var redirectResult = Assert.IsType<RedirectToActionResult>(resultado);
        Assert.Equal("Index", redirectResult.ActionName);
    }

    [Fact]
    public void UpdatePost_DeveAlterarProdutoERedirecionarParaIndex()
    {
        var mockRepo = new Mock<IProdutoRepositorio>();
        var controller = new ProdutoController(mockRepo.Object);
        var produtoExistente = new ProdutoModel
        {
            Id = 1,
            Nome = "Produto Antigo",
            Descricao = "Descrição Antiga",
            Preco = 10.0m,
            QuantidadeEstoque = 5
        };

        var produtoAtualizado = new ProdutoModel
        {
            Id = 1,
            Nome = "Produto Atualizado",
            Descricao = "Descrição Atualizada",
            Preco = 20.0m,
            QuantidadeEstoque = 10
        };

        mockRepo.Setup(r => r.BuscarPorId(produtoExistente.Id)).Returns(produtoExistente);
        var resultado = controller.Update(produtoAtualizado);

        mockRepo.Verify(r => r.Alterar(produtoAtualizado), Times.Once);
        var redirectResult = Assert.IsType<RedirectToActionResult>(resultado);
        Assert.Equal("Index", redirectResult.ActionName);
    }

    [Fact]
    public void DeletePost_DeveRemoverProdutoERedirecionarParaIndex_SeOProdutoTiverEstoque0()
    {
        var mockRepo = new Mock<IProdutoRepositorio>();
        var controller = new ProdutoController(mockRepo.Object);
        var produto = new ProdutoModel
        {
            Id = 1,
            Nome = "Produto Teste",
            Descricao = "Produto para testes",
            Preco = 2.99M,
            QuantidadeEstoque = 0
        };
        mockRepo.Setup(r => r.BuscarPorId(produto.Id)).Returns(produto);
        var resultado = controller.Delete(produto.Id);

        mockRepo.Verify(r => r.Remover(produto.Id), Times.Once);
        var redirectResult = Assert.IsType<RedirectToActionResult>(resultado);
        Assert.Equal("Index", redirectResult.ActionName);
    }

    [Fact]
    public void DeletePost_DeveDarErro_SeOProdutoTemEstoquePositivo()
    {
        var mockRepo = new Mock<IProdutoRepositorio>();
        var mockTempData = new Mock<ITempDataDictionary>();

        var controller = new ProdutoController(mockRepo.Object)
        {
            TempData = mockTempData.Object
        };
        var produto = new ProdutoModel
        {
            Id = 1,
            Nome = "Produto Teste",
            Descricao = "Produto para testes",
            Preco = 2.99M,
            QuantidadeEstoque = 10
        };
        mockRepo.Setup(r => r.BuscarPorId(produto.Id)).Returns(produto);
        var resultado = controller.Delete(produto.Id);

        mockRepo.Verify(r => r.Remover(produto.Id), Times.Never);
        var redirectResult = Assert.IsType<RedirectToActionResult>(resultado);
        Assert.Equal("Index", redirectResult.ActionName);

        mockTempData.VerifySet(t => t["Erro"] = "O produto não pode ser excluído porque ainda possui itens em estoque.", Times.Once);
    }
}
