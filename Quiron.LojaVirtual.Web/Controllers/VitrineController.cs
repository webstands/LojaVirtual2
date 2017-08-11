using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quiron.LojaVirtual.Dominio.Repositorio;
using Quiron.LojaVirtual.Web.Models;

namespace Quiron.LojaVirtual.Web.Controllers
{
    public class VitrineController : Controller
    {
        private ProdutosRepositorio _repositorio;
        public int ProdutosPorPagina = 3;

        public ViewResult ListaProdutos(string categoria, int pagina = 1)
        {
            /*Instancia a classe*/
            _repositorio = new ProdutosRepositorio();
            /*Atribui o objeto a variave produtos que vai ser
             passada para a view*/

            ProdutosViewModel model = new ProdutosViewModel
            {
                Produtos = _repositorio.Produtos
                 .Where(p => categoria == null || p.Categoria == categoria)
                     .OrderBy(p => p.Descricao)
                     .Skip((pagina - 1) * ProdutosPorPagina)
                     .Take(ProdutosPorPagina),

                Paginacao = new Paginacao
                {
                    PaginaAtual = pagina,
                    ItensPorPagina = ProdutosPorPagina,
                    ItensTotal = categoria == null ? _repositorio.Produtos.Count() : _repositorio.Produtos.Count(e => e.Categoria == categoria)
                },

                CategoriaAtual = categoria
            };          

            return View(model);
        }
    }
}