using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Respositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanchesMac.Respositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly CarrinhoCompra _carrinhoCompra;
        public PedidoRepository(AppDbContext appDbContext, CarrinhoCompra carrinhoCompra )
        {
            _appDbContext = appDbContext;
            _carrinhoCompra = carrinhoCompra;
        }

        public Pedido GetPedidoById(int pedidoId)
        {
            var pedido = _appDbContext.Pedidos.Include(pd => pd.PedidoItens
                         .Where(pd => pd.PedidoId == pedidoId))
                         .FirstOrDefault(p => p.PedidoId == pedidoId);

            return pedido;
        }

        public List<Pedido> GetPedidos()
        {
            return _appDbContext.Pedidos.ToList();
        }

        public void CriarPedido(Pedido pedido)
        {
            pedido.PedidoEnviado = DateTime.Now;
            _appDbContext.Pedidos.Add(pedido);
            _appDbContext.SaveChanges();


            var carrinhoCompraItens = _carrinhoCompra.CarrinhoCompraItens;

            foreach(var carrinhoItem in carrinhoCompraItens)
            {
                var pedidoDetalhe = new PedidoDetalhe()
                {
                    Quantidade = carrinhoItem.Quantidade,
                    LancheId = carrinhoItem.Lanche.LancheId,
                    PedidoId = pedido.PedidoId,
                    Preco = carrinhoItem.Lanche.Preco


                };

                _appDbContext.PedidoDetalhes.Add(pedidoDetalhe);

            }
            _appDbContext.SaveChanges();
        }



    }
}
