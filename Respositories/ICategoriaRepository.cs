using LanchesMac.Models;
using System.Collections.Generic;

namespace LanchesMac.Respositories.Interfaces
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> Categorias { get; }
    }
}
