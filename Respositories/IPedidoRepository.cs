using LanchesMac.Models;
using System.Collections.Generic;

namespace LanchesMac.Respositories.Interfaces
{
    public interface IPedidoRepository
    {
        void CriarPedido(Pedido pedido);
        Pedido GetPedidoById(int pedidoId);
        List<Pedido> GetPedidos();

    }
}
