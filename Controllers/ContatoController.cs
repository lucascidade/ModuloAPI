using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModuloAPI.Context;
using ModuloAPI.Entities;

namespace ModuloAPI.Controllers
{
    [ApiController]
    [Route("Contato")]
    public class ContatoController : ControllerBase // feita a herança do controllerbase
    {
        //criação de variavel somente de leitura
        private readonly AgendaContext _context;
        public ContatoController(AgendaContext context) // CRIAÇÃO DE CONSTURTOR E PASSANDO O CONTEXT
        {
            //injeção de dependencia
            _context = context;
        }
        [HttpPost] //VERBOSE POST PARA INSERCAO DE INFORMAÇÕES
        public IActionResult Create(Contato contato) //CRIAÇÃO DE METHOD PARA CRIAÇÃO DE UM CONTATO NO BANCO AGENDA
        {
            _context.Add(contato);
            _context.SaveChanges();
            return Ok(contato);
        }

        [HttpGet]
        public IActionResult ObterId(int id)
        {
            var contato = _context.Contatos.Find(id); //faz a busca do ID no meu DBSET contatos.
            if (contato == null) return NotFound(); //verificacao de valro/validacao
            return Ok(contato); //retorna se verdadeiro
        }

        [HttpGet("ObterPorNome")]
        public IActionResult ObterPorNome(string nome)
        {
            var contatos = _context.Contatos.Where(x => x.Nome.Contains(nome));
            return Ok(contatos);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Contato contato)
        {
            //verificacao para realizar a atualização
            var contatoBanco = _context.Contatos.Find(id);
            if (contato == null) return NotFound();
            //contatoBanco vai receber os valores de contato.Nome que iremos passar via JSON
            contatoBanco.Nome = contato.Nome;
            contatoBanco.Telefone = contato.Telefone;
            contatoBanco.Ativo = contato.Ativo;
            //context que realizado a atualizao no banco das informacoes repassadas no JSON
            //as informacoes estao na entidade Contato e nosso context herda o construtor dela
            _context.Contatos.Update(contatoBanco);
            _context.SaveChanges();
            return Ok(contatoBanco);
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            var contatoBanco = _context.Contatos.Find(id);
            if (contatoBanco == null) return NotFound();
            _context.Contatos.Remove(contatoBanco);
            _context.SaveChanges();
            return NoContent();
        }
    }
}