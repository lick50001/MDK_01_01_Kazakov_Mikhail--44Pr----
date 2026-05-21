using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Postavki.Classes;
using Postavki.Context;
using Postavki.Models;

namespace Postavki.ViewModels
{
    public class VM_Main : Notification
    {
        private readonly WarehouseContext _db = new();

        public ObservableCollection<Supplier> Suppliers { get; set; }
        public ObservableCollection<Order> Orders { get; set; }
        public ObservableCollection<Supplier> AllSuppliers { get; set; }

        public RelayCommand OnAddSupplier { get; }
        public RelayCommand<Supplier> OnEditSupplier { get; }
        public RelayCommand<Supplier> OnSaveSupplier { get; }
        public RelayCommand<Supplier> OnDeleteSupplier { get; }

        public RelayCommand OnAddOrder { get; }
        public RelayCommand<Order> OnEditOrder { get; }
        public RelayCommand<Order> OnSaveOrder { get; }
        public RelayCommand<Order> OnDeleteOrder { get; }

        public VM_Main()
        {
            _db.Database.EnsureCreated();
            LoadData();

            OnAddSupplier = new RelayCommand(AddSupplier);
            OnAddOrder = new RelayCommand(AddOrder);

            OnEditSupplier = new RelayCommand<Supplier>(sup => sup.IsEnable = true);
            OnSaveSupplier = new RelayCommand<Supplier>(SaveSupplier);
            OnDeleteSupplier = new RelayCommand<Supplier>(DeleteSupplier);

            OnEditOrder = new RelayCommand<Order>(order => order.IsEnable = true);
            OnSaveOrder = new RelayCommand<Order>(SaveOrder);
            OnDeleteOrder = new RelayCommand<Order>(DeleteOrder);
        }

        private void LoadData()
        {
            var suppliers = _db.Suppliers.ToList();
            Suppliers = new(suppliers);
            AllSuppliers = new(suppliers);

            var orders = _db.Orders.Include(o => o.Supplier).ToList();
            Orders = new(orders);
        }

        private void AddSupplier()
        {
            var newSup = new Supplier { Name = "Новый поставщик" };
            _db.Suppliers.Add(newSup);
            _db.SaveChanges();
            Suppliers.Add(newSup);
            AllSuppliers.Add(newSup);
        }

        private void SaveSupplier(Supplier sup)
        {
            sup.IsEnable = false;
            _db.SaveChanges();
        }

        private void DeleteSupplier(Supplier sup)
        {
            if (MessageBox.Show("Удалить поставщика?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _db.Suppliers.Remove(sup);
                _db.SaveChanges();
                Suppliers.Remove(sup);
                AllSuppliers.Remove(sup);
            }
        }

        private void AddOrder()
        {
            if (!AllSuppliers.Any())
            {
                MessageBox.Show("Сначала добавьте поставщика!");
                return;
            }

            var newOrder = new Order
            {
                Name = "Новый заказ",
                SupplierId = AllSuppliers.First().Id
            };

            _db.Orders.Add(newOrder);
            _db.SaveChanges();

            _db.Entry(newOrder).Reference(o => o.Supplier).Load();
            Orders.Add(newOrder);
        }

        private void SaveOrder(Order order)
        {
            order.IsEnable = false;
            _db.SaveChanges();
        }

        private void DeleteOrder(Order order)
        {
            if (MessageBox.Show("Удалить заказ?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _db.Orders.Remove(order);
                _db.SaveChanges();
                Orders.Remove(order);
            }
        }
    }
}