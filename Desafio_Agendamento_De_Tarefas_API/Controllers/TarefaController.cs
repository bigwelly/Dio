using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            try
            {
                var tarefa = _context.Tarefas.Find(id);
                if (tarefa == null)
                    return NotFound();

                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                var msg = "ocorreu um erro na sua requisição. tente novamente mais tarde";
                throw ex;
                return BadRequest(msg);
            }

        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            try
            {
                var listaTarefas = _context.Tarefas.ToList();

                return Ok(listaTarefas);
            }
            catch (Exception ex)
            {
                var msg = "ocorreu um erro na sua requisição. tente novamente mais tarde";
                throw ex;
                return BadRequest(msg);
            }

        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            try
            {
                var listaTarefas = _context.Tarefas.Where(x => x.Titulo == titulo);

                return Ok(listaTarefas);
            }
            catch (Exception ex)
            {
                var msg = "ocorreu um erro na sua requisição. tente novamente mais tarde";
                throw ex;
                return BadRequest(msg);
            }
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            try
            {
                var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);

                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                var msg = "ocorreu um erro na sua requisição. tente novamente mais tarde";
                throw ex;
                return BadRequest(msg);
            }
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            try
            {
                var listaTarefas = _context.Tarefas.Where(x => x.Status == status);

                return Ok(listaTarefas);
            }
            catch (Exception ex)
            {
                var msg = "ocorreu um erro na sua requisição. tente novamente mais tarde";
                throw ex;
                return BadRequest(msg);
            }
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            try
            {
                if (tarefa.Data == DateTime.MinValue)
                    return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

                _context.Tarefas.Add(tarefa);
                _context.SaveChanges();

                return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
            }
            catch (Exception ex)
            {
                var msg = "ocorreu um erro na sua requisição. tente novamente mais tarde";
                throw ex;
                return BadRequest(msg);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {

            try
            {
                var tarefaBanco = _context.Tarefas.Find(id);

                if (tarefaBanco == null)
                    return NotFound();

                if (tarefa.Data == DateTime.MinValue)
                    return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

                tarefaBanco.Titulo = tarefa.Titulo;
                tarefaBanco.Descricao = tarefa.Descricao;
                tarefaBanco.Data = tarefa.Data;
                tarefaBanco.Status = tarefa.Status;

                _context.Tarefas.Update(tarefaBanco);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                var msg = "ocorreu um erro na sua requisição. tente novamente mais tarde";
                throw ex;
                return BadRequest(msg);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                var tarefaBanco = _context.Tarefas.Find(id);

                if (tarefaBanco == null)
                    return NotFound();

                _context.Tarefas.Remove(tarefaBanco);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                var msg = "ocorreu um erro na sua requisição. tente novamente mais tarde";
                throw ex;
                return BadRequest(msg);
            }

        }
    }
}
