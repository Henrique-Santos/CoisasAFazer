using CoisasAFazer.Core.Commands;
using CoisasAFazer.Infrastructure;
using CoisasAFazer.Services.Handlers;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CoisasAFazer.Testes
{
    public class ObtemCategoriaPorIdExecute
    {
        [Fact]
        public void QuandoIdForExistenteDeveChamarObtemCategoriaPorIdUmaUnicaVez()
        {
            //arange
            var idCategoria = 20;
            var comando = new ObtemCategoriaPorId(idCategoria);
            var mock = new Mock<IRepositorioTarefas>();
            var repo = mock.Object;
            var handler = new ObtemCategoriaPorIdHandler(repo);
            //act
            handler.Execute(comando);
            //asert
            mock.Verify(r => r.ObtemCategoriaPorId(idCategoria), Times.Once());
        }
    }
}
