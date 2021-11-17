using CoisasAFazer.Core.Models;
using CoisasAFazer.Infrastructure;
using CoisasAFazer.Services.Handlers;
using CoisasAFazer.WebApp.Controllers;
using CoisasAFazer.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace CoisasAFazer.Testes
{
    public class TarefasControllerEndpointCadastraTarefa
    {
        [Fact]
        public void DadaTarefaComInformacoesValidasDeveRetornar200()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CadastraTarefaHandler>>();
            var options = new DbContextOptionsBuilder<DbTarefasContext>()
                .UseInMemoryDatabase("DbTarefasContext")
                .Options;
            var contexto = new DbTarefasContext(options);
            var repo = new RepositorioTarefa(contexto);
            var controller = new TarefasController(repo, mockLogger.Object);
            var model = new CadastraTarefaVM();
            model.IdCategoria = 20;
            model.Titulo = "Estudar xUnit";
            model.Prazo = new DateTime(2021, 12, 12);
            contexto.Categorias.Add(new Categoria(20, "Estudar xUnit"));
            contexto.SaveChanges();

            //Act
            var retorno = controller.EndpointCadastraTarefa(model);

            //Assert
            Assert.IsType<OkResult>(retorno);
        }

        [Fact]
        public void QuandoExcecaoForLancadaDeveRetorarStatusCode500()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CadastraTarefaHandler>>();
            var mockRepositorio = new Mock<IRepositorioTarefas>();
            mockRepositorio.Setup(r => r.ObtemCategoriaPorId(20)).Returns(new Categoria(20, "Estudo"));
            mockRepositorio.Setup(r => r.IncluirTarefas(It.IsAny<Tarefa[]>())).Throws(new Exception("Houve um erro"));
            var controller = new TarefasController(mockRepositorio.Object, mockLogger.Object);
            var model = new CadastraTarefaVM();
            model.IdCategoria = 20;
            model.Titulo = "Estudar xUnit";
            model.Prazo = new DateTime(2021, 12, 12);

            //Act
            var retorno = controller.EndpointCadastraTarefa(model);

            //Assert
            Assert.IsType<StatusCodeResult>(retorno);
            var statusCodeRetornado = (retorno as StatusCodeResult).StatusCode;
            Assert.Equal(500, statusCodeRetornado);
        }
    }
}
