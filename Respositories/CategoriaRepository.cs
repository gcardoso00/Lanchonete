using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Respositories.Interfaces;
using System.Collections.Generic;


namespace LanchesMac.Respositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;
        public CategoriaRepository(AppDbContext contexto)
        {
            _context = contexto;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}
