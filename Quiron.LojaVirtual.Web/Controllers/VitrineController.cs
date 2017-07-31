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
        public int ProdutosPorPagina = 8;

        public ViewResult ListaProdutos(int pagina = 1)
        {
            /*Instancia a classe*/
            _repositorio = new ProdutosRepositorio();
            /*Atribui o objeto a variave produtos que vai ser
             passada para a view*/

            ProdutosViewModel model = new ProdutosViewModel
            {
                Produtos = _repositorio.Produtos
                     .OrderBy(p => p.Nome)
                     .Skip((pagina - 1) * ProdutosPorPagina)
                     .Take(ProdutosPorPagina),

                Paginacao = new Paginacao
                {
                    PaginaAtual = pagina,
                    ItensPorPagina = ProdutosPorPagina,
                    ItensTotal = _repositorio.Produtos.Count()
                }
            };          

            return View(model);
        }
    }
}