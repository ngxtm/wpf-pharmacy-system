# WPF Pharmacy Management System

A comprehensive pharmacy management system built with WPF (Windows Presentation Foundation) using .NET 9.0. This application provides a complete solution for managing pharmacy operations including product inventory, user accounts, order processing, and administrative functions.

## Features

### User Management
- **User Authentication**: Secure login system with role-based access control
- **Admin Panel**: Administrative interface for system management
- **User Dashboard**: Customer interface for browsing and purchasing products

### Product Management
- **Product Catalog**: Browse and search pharmaceutical products
- **Product Details**: Detailed information for each medication
- **Inventory Management**: Add, edit, and manage product inventory
- **Product Images**: Visual representation of medications

### Order Management
- **Shopping Cart**: Add products to cart and manage quantities
- **Order Processing**: Complete order transactions
- **Order History**: View past orders and transaction details

### Administrative Features
- **Product Administration**: Add new products and manage existing inventory
- **User Management**: Manage user accounts and permissions
- **System Configuration**: Configure application settings

## Technology Stack

- **Frontend**: WPF (Windows Presentation Foundation)
- **Backend**: .NET 9.0
- **Database**: SQL Server with Entity Framework Core 9.0
- **Architecture**: 3-Layer Architecture (Presentation, Business Logic, Data Access)

## Project Structure

```
wpf-pharmacy-system/
├── WpfApp1/                    # Presentation Layer (WPF UI)
│   ├── Views/                  # XAML Views and Code-behind
│   │   ├── LoginWindow.xaml    # User authentication
│   │   ├── AdminWindow.xaml    # Admin dashboard
│   │   ├── UserWindow.xaml     # Customer dashboard
│   │   ├── AddProductWindow.xaml # Product management
│   │   ├── ProductDetailWindow.xaml # Product details
│   │   └── OrderHistoryWindow.xaml # Order history
│   ├── ViewModels/             # MVVM ViewModels
│   ├── Converters/             # Value converters
│   ├── images/                 # Product images and UI assets
│   └── appsettings.json        # Application configuration
├── BLL/                        # Business Logic Layer
│   └── Services/               # Business services
├── DAL/                        # Data Access Layer
│   ├── Entities/               # Entity models
│   │   ├── Account.cs          # User account entity
│   │   ├── Product.cs          # Product entity
│   │   ├── Order.cs            # Order entity
│   │   ├── OrderItem.cs        # Order item entity
│   │   ├── CartItem.cs         # Shopping cart entity
│   │   └── PharmacyDbContext.cs # EF Core DbContext
│   └── Repositories/           # Data repositories
└── README.md
```

## Prerequisites

- **Operating System**: Windows 10/11
- **.NET Runtime**: .NET 9.0 or later
- **Database**: SQL Server (LocalDB, Express, or Full)
- **IDE**: Visual Studio 2022 (recommended) or Visual Studio Code

## Installation & Setup

### 1. Clone the Repository
```bash
git clone https://github.com/[username]/wpf-pharmacy-system.git
cd wpf-pharmacy-system
```

### 2. Database Setup
1. Ensure SQL Server is installed and running
2. Update the connection string in `WpfApp1/appsettings.json` if needed:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "server=(local); database=PharmacyDB; uid=sa; pwd=1234567890; TrustServerCertificate=True; Trusted_Connection=True;"
     }
   }
   ```

### 3. Database Migration
Open Package Manager Console in Visual Studio and run:
```powershell
Update-Database
```

### 4. Build and Run
1. Open `WpfApp1.sln` in Visual Studio
2. Build the solution (Ctrl+Shift+B)
3. Set `WpfApp1` as the startup project
4. Run the application (F5)

## Usage

### For Customers
1. **Login**: Use your credentials to access the system
2. **Browse Products**: View available medications and their details
3. **Add to Cart**: Select products and add them to your shopping cart
4. **Place Orders**: Complete your purchase through the checkout process
5. **View History**: Check your past orders and transaction details

### For Administrators
1. **Admin Login**: Access the system with administrative privileges
2. **Manage Products**: Add new medications, update existing products
3. **Inventory Control**: Monitor stock levels and update quantities
4. **User Management**: Manage customer accounts and permissions
5. **System Monitoring**: Oversee system operations and performance

## Database Schema

The system uses the following main entities:
- **Account**: User authentication and profile information
- **Product**: Pharmaceutical product details and inventory
- **Order**: Customer order information
- **OrderItem**: Individual items within an order
- **CartItem**: Shopping cart functionality

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/new-feature`)
3. Commit your changes (`git commit -am 'Add new feature'`)
4. Push to the branch (`git push origin feature/new-feature`)
5. Create a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Support

For support and questions, please open an issue in the GitHub repository or contact the development team.

## Screenshots

*Note: Add screenshots of the application interface here to showcase the UI*

---

**Developed with ❤️ using WPF and .NET 9.0**