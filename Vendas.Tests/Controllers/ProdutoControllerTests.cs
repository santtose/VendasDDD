using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.Core.Entities;
using Vendas.Core.Repositories;
using VendasDDD.Controllers;

namespace Vendas.Tests.Controllers
{
    public class ProdutoControllerTests
    {
        private readonly Mock<IProdutoRepository> _produtoRepositoryMock;
        private readonly ProdutoController _produtoController;

        public ProdutoControllerTests()
        {
            _produtoRepositoryMock = new Mock<IProdutoRepository>();
            _produtoController = new ProdutoController(_produtoRepositoryMock.Object);
        }

        [Fact]
        public async Task Get_ProdutoExistente_DeveRetornarOk()
        {
            // Arrange
            var produtoId = Guid.NewGuid();
            var produto = new Produto(produtoId, "Produto Teste", 100);
            _produtoRepositoryMock.Setup(repo => repo.ObterPorId(produtoId))
                                  .ReturnsAsync(produto);

            // Act
            var result = await _produtoController.Get(produtoId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduto = Assert.IsType<Produto>(okResult.Value);
            Assert.Equal(produtoId, returnedProduto.Id);
        }

        [Fact]
        public async Task Get_ProdutoInexistente_DeveRetornarNotFound()
        {
            // Arrange
            var produtoId = Guid.NewGuid();
            _produtoRepositoryMock.Setup(repo => repo.ObterPorId(produtoId))
                                  .ReturnsAsync((Produto)null);

            // Act
            var result = await _produtoController.Get(produtoId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_ProdutoValido_DeveRetornarCreated()
        {
            // Arrange
            var produto = new Produto("Produto Teste", 100);

            // Act
            var result = await _produtoController.Post(produto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedProduto = Assert.IsType<Produto>(createdAtActionResult.Value);
            Assert.Equal(produto.Id, returnedProduto.Id);
        }
    }
}
