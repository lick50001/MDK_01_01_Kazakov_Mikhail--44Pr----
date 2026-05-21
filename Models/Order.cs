using System.ComponentModel.DataAnnotations.Schema;
using Postavki.Classes;

namespace Postavki.Models
{
    public class Order : Notification
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string Priority { get; set; } = "Средний";

        public int? SupplierId { get; set; }
        public Supplier? Supplier { get; set; }


        [NotMapped]
        public string IsEnableText
        {
            get => IsEnable ? "Сохранить" : "Изменить";
        }

        private bool isEnable;
        [NotMapped]
        public bool IsEnable
        {
            get => isEnable;
            set
            {
                isEnable = value;
                onPropertyChanged();
                onPropertyChanged(nameof(IsEnableText));
            }
        }
    }
}