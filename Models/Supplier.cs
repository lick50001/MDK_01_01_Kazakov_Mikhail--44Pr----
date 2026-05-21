using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Postavki.Classes;

namespace Postavki.Models
{
    public class Supplier : Notification
    {
        public int Id { get; set; }
        private string name = "";
        public string Name
        {
            get => name;
            set { name = value; onPropertyChanged(); }
        }

        private string phone = "";
        public string Phone
        {
            get => phone;
            set { phone = value; onPropertyChanged(); }
        }
        private string address = "";
        public string Address 
        {
            get => address;
            set { address = value; onPropertyChanged(); }
        }
        private bool isEnable;
        public bool IsEnable
        {
            get => isEnable;
            set { isEnable = value; onPropertyChanged(); }
        }

        public string IsEnableText => IsEnable ? "Сохранить" : "Изменить";
    }
}
