using System.ComponentModel.DataAnnotations;

namespace WebAppCoreProduct.Models
{
    public class Product
    {
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [StringLength(10, ErrorMessage = "Название товара не может превышать 15 символов")]
        [Display(Name = "Название товара")]
        public string Name { get; set; }
        [Range(0, 1000, ErrorMessage = "Вне разрешенного диапазона")]
        [Display(Name = "Цена товара")]
        public decimal? Price { get; set; }

    }
}
