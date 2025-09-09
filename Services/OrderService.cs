using System;
using System.Linq;
using System.Threading.Tasks;
using DoAnTotNghiep.Data;
using DoAnTotNghiep.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DoAnTotNghiep.Services
{
    public class OrderService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly CartService _cartService;

        public OrderService(ApplicationDbContext dbContext, CartService cartService)
        {
            _dbContext = dbContext;
            _cartService = cartService;
        }

        public async Task<string?> PlaceOrderAsync(Order order, ClaimsPrincipal user)
        {
            var cartItems = _cartService.Items;
            if (!cartItems.Any()) 
            {
                return "Giỏ hàng của bạn đang trống.";
            }

            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            order.OrderDate = DateTime.Now;
            order.TotalAmount = _cartService.Total;
            order.Status = "Chờ xác nhận";
            order.UserId = userId;

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName ?? string.Empty,
                    Quantity = item.Quantity,
                    Price = item.Price
                };
                order.OrderDetails.Add(orderDetail);

                // --- TRỪ SỐ LƯỢỢNG TỒN KHO ---
                var productInDb = await _dbContext.Products.FindAsync(item.ProductId);
                if (productInDb == null)
                {
                    return $"Sản phẩm '{item.ProductName}' không còn tồn tại.";
                }
                
                if (productInDb.StockQuantity < item.Quantity)
                {
                    return $"Xin lỗi, sản phẩm '{item.ProductName}' không đủ số lượng tồn kho (chỉ còn {productInDb.StockQuantity}).";
                }
                
                productInDb.StockQuantity -= item.Quantity;
            }

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            _cartService.ClearCart();

            return null; // Trả về null nếu thành công
        }
    }
}

