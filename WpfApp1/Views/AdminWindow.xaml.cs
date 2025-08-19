using System.Windows;
using DAL.Entities;
using BLL.Services;
using DAL.Repositories;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace WpfApp1.Views
{
    public partial class AdminWindow : Window
    {
        private Account _account;
        private ProductService _productService;
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
        private ObservableCollection<Product> _allProducts = new ObservableCollection<Product>(); // Lưu toàn bộ sản phẩm để tìm kiếm

        public Account CurrentAccount;


        public AdminWindow(Account account)
        {
            InitializeComponent();
            _account = account;
            CurrentAccount = account;
            _productService = new ProductService(new DAL.Repositories.ProductRepository(new DAL.Entities.PharmacyDbContext()));
            DataContext = this;
            LoadProducts();
        }

        private async void LoadProducts()
        {
            Products.Clear();
            _allProducts.Clear();
            var products = await _productService.GetAllProductsAsync();
            foreach (var p in products)
            {
                Products.Add(p);
                _allProducts.Add(p);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddProductWindow();
            if (addWindow.ShowDialog() == true)
            {
                LoadProducts();
                MessageBox.Show("Danh sách sản phẩm đã được cập nhật!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.Tag is Product product)
            {
                var editWindow = new AddProductWindow(product);
                if (editWindow.ShowDialog() == true)
                {
                    await _productService.UpdateProductAsync(editWindow.EditedProduct);
                    LoadProducts();
                    MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private async void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.Tag is Product product)
            {
                if (MessageBox.Show($"Bạn có chắc muốn xóa sản phẩm '{product.Name}'?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    await _productService.DeleteProductAsync(product.Id);
                    LoadProducts();
                    MessageBox.Show("Đã xóa sản phẩm!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string keyword = SearchTextBox.Text.Trim().ToLower();
            Products.Clear();
            foreach (var p in _allProducts)
            {
                if (string.IsNullOrEmpty(keyword) || p.Name.ToLower().Contains(keyword))
                {
                    Products.Add(p);
                }
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            Application.Current.MainWindow = loginWindow;
            this.Close();
        }
    }
} 