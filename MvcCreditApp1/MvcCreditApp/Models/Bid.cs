using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace MvcCreditApp.Models
{
    public class Bid
    {
        // ID заявки
        public virtual int BidId { get; set; }
        // Имя заявителя
        [DisplayName("Имя заявителя")]
        public virtual string Name { get; set; }
        // Название кредита
        [DisplayName("Название кредита")]
        public virtual string CreditHead { get; set; }

        [DisplayName("Дата подачи заявки")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        // Дата подачи заявки
        public virtual DateTime bidDate { get; set; }
    }
}
