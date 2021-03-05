using LanchesMac.Models;
using System.Collections.Generic;

namespace LanchesMac.Respositories
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> Categorias { get; }
    }
}
