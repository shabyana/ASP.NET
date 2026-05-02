using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppCoreProduct.Models;
using WebAppCoreProduct.Services;

namespace WebAppCoreProduct.Pages
{
    public class ProductModel : PageModel
    {
        public string? MessageRezult { get; private set; }

        [BindProperty]
        public Product Product { get; set; }

        private readonly Services.OperationPrice _servicePrice;
        public ProductModel(Services.OperationPrice servicePrice)
        {
            _servicePrice = servicePrice;
        }

        public void OnPostTax()
        {
            
            if (Product.Price == null || Product.Price < 0 || string.IsNullOrEmpty(Product.Name))
            {
                MessageRezult = "Переданы некорректные данные. Повторите ввод";
                return;
            }
            var result = _servicePrice.CalcTax(Product.Name, Product.Price);
            MessageRezult = $"Для товара {Product.Name} с ценой {Product.Price} цена с учетом налога получится {result}";

        }
        public void OnPostDiscont(double discont)
        {
            var result = _servicePrice.CalcPriceDiscount(Product.Price, discont);
            MessageRezult = $"Для товара {Product.Name} с ценой {Product.Price} и скидкой {discont} получится {result}";
        }
        public void OnPost()
        {
            if (!ModelState.IsValid)
                {
                MessageRezult = "Переданы некорректные данные. Повторите ввод";
                return;
            }
            var result = _servicePrice.CalcPrice(Product.Price);
            MessageRezult = $"Для товара {Product.Name} с ценой {Product.Price} скидка получится {result}";
        }
        //public void OnPost(string name, decimal? price)
        //{
        //    Product = new Product();
        //    if (price == null || price < 0 || string.IsNullOrEmpty(name))
        //    {
        //        MessageRezult = "Переданы некорректные данные. Повторите ввод";
        //        return;
        //    }
        //    var result = price * (decimal?)0.18;
        //    MessageRezult = $"Для товара {name} с ценой {price} скидка получится {result}";
        //    Product.Price = price;
        //    Product.Name = name;
        //}
        public void OnGet()
        {
            MessageRezult = "Для товара можно определить скидку";
        }

    }
}
