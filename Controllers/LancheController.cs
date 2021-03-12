using LanchesMac.Models;
using LanchesMac.Respositories.Interfaces;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LanchesMac.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private ICategoriaRepository _categoriaRepository { get; }
        public LancheController(ILancheRepository lancheRepository, ICategoriaRepository categoriaRepository)
        {
            _lancheRepository = lancheRepository;
            _categoriaRepository = categoriaRepository;

        }

        public IActionResult List(string categoria)
        {
            string _categoria = categoria;
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if(string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
                categoria = "Todos os lanches";
            }
            else
            {
                /*  if(string.Equals("Normal", _categoria, StringComparison.OrdinalIgnoreCase))
                  {
                      lanches = _lancheRepository.Lanches.Where(l=> l.Categoria.CategoriaNome.Equals("Normal")).OrderBy(l => l.Nome);
                  }
                  else 
                  {
                      lanches = _lancheRepository.Lanches.Where(l=> l.Categoria.CategoriaNome.Equals("Natural")).OrderBy(l => l.Nome);
                  }*/

                lanches = _lancheRepository.Lanches.Where(p => p.Categoria.CategoriaNome.Equals(categoria)).OrderBy(p => p.Nome);

                categoriaAtual = _categoria;

            }


            var lancheslistViewModel = new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };
            
            
            return View(lancheslistViewModel);
        }

        public ViewResult Details(int LancheId)
        {
            var lanche = _lancheRepository.Lanches.FirstOrDefault(l => l.LancheId == LancheId);
            if(lanche == null)
            {
                return View("~/View/Error/Error.cshtml");
            }
            return View(lanche);
        }

        public ViewResult Search(string searchString)
        {
            string _searchString = searchString;
            IEnumerable<Lanche> lanches;
            string _categoriaAtual = string.Empty;

            if(string.IsNullOrEmpty(_searchString))
            {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
            }
            else
            {
                lanches = _lancheRepository.Lanches.Where(l=> l.Nome.ToLower().Contains(_searchString.ToLower()));
            }
        
            return View("~/Views/Lanche/List.cshtml", new LancheListViewModel { Lanches = lanches, CategoriaAtual="Todos os Lanches" });
        }

    }
}
