using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DAL.Entities;
using BLL.Services;

namespace WpfApp1.Views
{
    public partial class OrderHistoryWindow : Window
    {
        private Account _account;
        private OrderService _orderService;
        private List<Order> _orders = new();
        private List<ProductPurchaseSummary> _allSummary = new();
        private List<ProductPurchaseSummary> _filteredSummary = new();

        public OrderHistoryWindow(Account account)
        {
            InitializeComponent();
            _account = account;
            _orderService = new OrderService();
            
            WelcomeText.Text = $"Chào mừng, {_account.FullName}! 👋";
            
            LoadOrders();
        }

        public class ProductPurchaseSummary
        {
            public string ProductName { get; set; } = string.Empty;
            public int TotalQuantity { get; set; }
            public decimal TotalSpent { get; set; }
        }

        private async void LoadOrders()
        {
            try
            {
                _orders = await _orderService.GetOrdersByAccountIdAsync(_account.Id);
                
                foreach (var order in _orders)
                {
                    order.OrderItems = await _orderService.GetOrderItemsByOrderIdAsync(order.Id);
                    order.TotalAmount = order.OrderItems.Sum(item => item.TotalPrice);
                }
                
                var allOrderItems = _orders.SelectMany(o => o.OrderItems);
                _allSummary = allOrderItems
                    .GroupBy(item => item.Product.Name)
                    .Select(g => new ProductPurchaseSummary
                    {
                        ProductName = g.Key,
                        TotalQuantity = g.Sum(x => x.Quantity),
                        TotalSpent = g.Sum(x => x.TotalPrice)
                    })
                    .OrderByDescending(x => x.TotalSpent)
                    .ToList();
                
                _filteredSummary = _allSummary.ToList();
                UpdateDisplay();
                
                if (_allSummary.Count == 0)
                {
                    ShowEmptyState();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải lịch sử đơn hàng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateDisplay()
        {
            OrdersItemsControl.ItemsSource = _filteredSummary;
            
            var totalItems = _filteredSummary.Sum(x => x.TotalQuantity);
            var totalSpent = _filteredSummary.Sum(x => x.TotalSpent);
            
            TotalItemsText.Text = $"Tổng: {totalItems} sản phẩm";
            TotalSpentText.Text = $"Tổng chi tiêu: {totalSpent:N0} đ";
            
            TotalOrdersText.Text = _orders?.Count.ToString() ?? "0";
            TotalProductsText.Text = _allSummary?.Count.ToString() ?? "0";
            GrandTotalText.Text = $"{_allSummary?.Sum(x => x.TotalSpent):N0} đ";
            
            if (_filteredSummary.Count == 0 && !string.IsNullOrEmpty(SearchTextBox.Text))
            {
                EmptyStateBorder.Visibility = Visibility.Visible;
                OrdersItemsControl.Visibility = Visibility.Collapsed;
            }
            else
            {
                EmptyStateBorder.Visibility = Visibility.Collapsed;
                OrdersItemsControl.Visibility = Visibility.Visible;
            }
        }

        private void ShowEmptyState()
        {
            EmptyStateBorder.Visibility = Visibility.Visible;
            OrdersItemsControl.Visibility = Visibility.Collapsed;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = SearchTextBox.Text?.ToLower() ?? "";
            
            if (string.IsNullOrEmpty(searchText))
            {
                _filteredSummary = _allSummary?.ToList() ?? new List<ProductPurchaseSummary>();
            }
            else
            {
                _filteredSummary = _allSummary?.Where(x => 
                    x.ProductName.ToLower().Contains(searchText))
                    .ToList() ?? new List<ProductPurchaseSummary>();
            }
            
            UpdateDisplay();
        }

        private void ViewDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is ProductPurchaseSummary summary)
            {
                var message = $"Chi tiết sản phẩm: {summary.ProductName}\n\n" +
                             $"📦 Tổng số lượng đã mua: {summary.TotalQuantity} sản phẩm\n" +
                             $"💰 Tổng số tiền đã chi: {summary.TotalSpent:N0} VNĐ\n" +
                             $"💊 Loại: Thuốc\n\n" +
                             $"Cảm ơn bạn đã tin tưởng sử dụng sản phẩm của chúng tôi! 🙏";
                
                MessageBox.Show(message, "Chi tiết sản phẩm", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            LoadOrders();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
} 