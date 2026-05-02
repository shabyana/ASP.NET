namespace WebAppCoreProduct.Services
{
    public class OperationPrice
    {
        public decimal? CalcPrice(decimal? price) => price * (decimal?)0.18;
        public decimal? CalcPriceDiscount(decimal? price, double discont) => price * (decimal?)discont / 100;

        public decimal? CalcTax(string name, decimal? price) => (price * (decimal?)0.20) + price;
    }
}
