using Microsoft.AspNetCore.Mvc;
using CoisasAFazer.WebApp.Models;
using CoisasAFazer.Core.Commands;
using CoisasAFazer.Services.Handlers;
using CoisasAFazer.Infrastructure;

namespace CoisasAFazer.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        [HttpPost]
        public IActionResult EndpointCadastraTarefa(CadastraTarefaVM model)
        {
            var cmdObtemCateg = new ObtemCategoriaPorId(model.IdCategoria);
            var categoria = new ObtemCategoriaPorIdHandler().Execute(cmdObtemCateg);
            if (categoria == null)
            {
                return NotFound("Categoria não encontrada");
            }

            var comando = new CadastraTarefa(model.Titulo, categoria, model.Prazo);

            var tarefa = new RepositorioTarefa();
            var handler = new CadastraTarefaHandler(tarefa);
            handler.Execute(comando);
            return Ok();
        }
    }
}