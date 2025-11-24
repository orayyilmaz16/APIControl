using System;
using System.ComponentModel.DataAnnotations;

namespace APIControl.Application.DTOs
{
    public class UpdateProductDto
    {
        [Required(ErrorMessage = "Id zorunludur.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Ürün adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Ürün adı en fazla 100 karakter olabilir.")]
        public string Name { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır.")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stok negatif olamaz.")]
        public int Stock { get; set; }
    }
}
