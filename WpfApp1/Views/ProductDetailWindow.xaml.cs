using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DAL.Entities;
using System.IO;

namespace WpfApp1.Views
{
    public class AddToCartEventArgs : EventArgs
    {
        public Product Product { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = "";
    }

    public partial class ProductDetailWindow : Window
    {
        private Product _product;
        public event EventHandler<AddToCartEventArgs> AddToCartRequested;

        public ProductDetailWindow(Product product)
        {
            InitializeComponent();
            _product = product;
            LoadProductDetails();
        }

        private void LoadProductDetails()
        {
            if (_product == null) return;

            ProductName.Text = _product.Name;
            ProductCategory.Text = _product.Category;
            ProductManufacturer.Text = _product.Manufacturer;
            ProductPrice.Text = $"{_product.Price:N0} VNĐ";
            ProductDescription.Text = _product.Description;
            ProductCreatedAt.Text = _product.CreatedAt.ToString("dd/MM/yyyy");
            ProductUpdatedAt.Text = _product.UpdatedAt.ToString("dd/MM/yyyy");

            ProductStock.Text = $"{_product.StockQuantity} sản phẩm";
            if (_product.StockQuantity > 10)
            {
                ProductStock.Foreground = new SolidColorBrush(Color.FromRgb(40, 167, 69));
            }
            else if (_product.StockQuantity > 0)
            {
                ProductStock.Foreground = new SolidColorBrush(Color.FromRgb(255, 193, 7));
            }
            else
            {
                ProductStock.Foreground = new SolidColorBrush(Color.FromRgb(220, 53, 69));
                ProductStock.Text += " (Hết hàng)";
                AddToCartButton.IsEnabled = false;
                AddToCartButton.Content = "Hết hàng";
                AddToCartButton.Background = new SolidColorBrush(Color.FromRgb(108, 117, 125));
            }

            if (_product.IsActive)
            {
                ProductStatus.Text = "Đang bán";
                ProductStatus.Foreground = new SolidColorBrush(Color.FromRgb(40, 167, 69));
                StatusIndicator.Fill = new SolidColorBrush(Color.FromRgb(40, 167, 69));
            }
            else
            {
                ProductStatus.Text = "Ngừng bán";
                ProductStatus.Foreground = new SolidColorBrush(Color.FromRgb(220, 53, 69));
                StatusIndicator.Fill = new SolidColorBrush(Color.FromRgb(220, 53, 69));
                AddToCartButton.IsEnabled = false;
                AddToCartButton.Content = "Ngừng bán";
                AddToCartButton.Background = new SolidColorBrush(Color.FromRgb(108, 117, 125));
            }

            LoadProductImage();
        }

        private void LoadProductImage()
        {
            bool imageLoaded = false;
            
            try
            {
                if (!string.IsNullOrEmpty(_product.ImageUrl))
                {
                    string fullPath;
                    if (Path.IsPathRooted(_product.ImageUrl))
                    {
                        fullPath = _product.ImageUrl;
                    }
                    else
                    {
                        var possiblePaths = new[]
                        {
                            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _product.ImageUrl),
                            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images", _product.ImageUrl),
                            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WpfApp1", "images", _product.ImageUrl),
                            _product.ImageUrl
                        };

                        fullPath = null;
                        foreach (var path in possiblePaths)
                        {
                            if (File.Exists(path))
                            {
                                fullPath = path;
                                break;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(fullPath) && File.Exists(fullPath))
                    {
                        var bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(fullPath);
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.EndInit();
                        ProductImage.Source = bitmap;
                        imageLoaded = true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load image: {ex.Message}");
            }

            PlaceholderBorder.Visibility = imageLoaded ? Visibility.Collapsed : Visibility.Visible;
            ProductImage.Visibility = imageLoaded ? Visibility.Visible : Visibility.Collapsed;
        }

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            var eventArgs = new AddToCartEventArgs 
            { 
                Product = _product, 
                Success = false,
                Message = ""
            };
            
            AddToCartRequested?.Invoke(this, eventArgs);
            
            if (eventArgs.Success)
            {
                MessageBox.Show(eventArgs.Message, 
                              "Thông báo", 
                              MessageBoxButton.OK, 
                              MessageBoxImage.Information);
                this.Close();
            }
            else if (!string.IsNullOrEmpty(eventArgs.Message))
            {
                MessageBox.Show(eventArgs.Message, 
                              "Thông báo", 
                              MessageBoxButton.OK, 
                              MessageBoxImage.Warning);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}