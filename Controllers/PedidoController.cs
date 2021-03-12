using LanchesMac.Models;
using LanchesMac.Respositories;
using LanchesMac.Respositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LanchesMac.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
        }



        [HttpGet]
        [Authorize]
        public IActionResult Checkout()
        {
            return View();
        }
        
        [Authorize]
        [HttpPost]
        public IActionResult Checkout(Pedido pedido)
        {
            var items = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItens = items;

            if (_carrinhoCompra.CarrinhoCompraItens.Count == 0)
            {
                ModelState.AddModelError("", "Seu carrinho está vazio, que tal incluir um lanche...");
            }

            if (ModelState.IsValid)
            {
                _pedidoRepository.CriarPedido(pedido);
                _carrinhoCompra.LimparCarrinho();
                return RedirectToAction("CheckoutCompleto");
            }
            return View(pedido);

        }
            /*foreach(var item in items)
            {
                totalItensPedido += item.Quantidade;
                precoTotalPedido += (item.Lanche.Preco * item.Quantidade);

            }

            pedido.TotalItensPedido = totalItensPedido;*/

        public IActionResult CheckoutCompleto()
        {
            ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido : ";
            return View();
        }
        
        
    }
}
