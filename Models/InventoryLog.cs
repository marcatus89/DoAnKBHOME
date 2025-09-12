using System;
using System.ComponentModel.DataAnnotations;

namespace DoAnTotNghiep.Models
{
    // Model để ghi lại lịch sử thay đổi tồn kho
    public class InventoryLog
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public int QuantityChange { get; set; } // Số lượng thay đổi (+ cho nhập, - cho xuất)
        public int NewQuantity { get; set; } // Số lượng tồn kho mới sau khi thay đổi

        [Required]
        public string Reason { get; set; } = string.Empty; // Lý do thay đổi (VD: Nhập hàng, Bán hàng, Kiểm kê)
        
        public DateTime Timestamp { get; set; } = DateTime.Now; // Thời điểm thay đổi
    }
}