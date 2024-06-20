#Store Application

Overview
Welcome to OnlineStore Application, an advanced e-commerce platform developed using .NET Core 8.
Our application is designed to provide a seamless shopping experience for buyers, offering a comprehensive suite of features for product management, 
transaction handling, and exceptional customer service.


Features
User Management: Secure registration and login functionality for buyers and sellers.
Product Catalog: Easily create, update, and manage product listings with detailed descriptions, images, and pricing.
Shopping Cart: Add, update, and remove items from the cart with real-time price updates.
Order Management: Streamlined order processing, from placement to shipment tracking.
Payment Integration: Secure payment gateway integration supporting multiple payment methods.
Search and Filter: Advanced search and filtering options to help users find products quickly.
Analytics Dashboard: Sales and customer analytics for sellers to monitor performance and trends.

1- Installation
Prerequisites
.NET Core 8 SDK
SQL Server or other supported database
Node.js (for front-end assets management)
npm (for front-end assets management)

2- Steps
Clone the repository

bash
git clone https://github.com/ayonis/StoreApplication.git
cd StoreApplication
Set up the database

Create a new database in your SQL Server instance.
Update the connection string in appsettings.json with your database details.
json

{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=YourInstanceName;Initial Catalog=Store_DB;Integrated Security=True;TrustServerCertificate=True;"
  }
}

3-Install dependencies

- Restore .NET dependencies:
   dotnet restore
  
- Install front-end dependencies:
  npm install
  
- Apply migrations
  dotnet ef database update

- Run the application
  dotnet run
  
Access the application
Open your browser and navigate to http://localhost:[Port] (or the port specified in your launch settings).

Usage
  Owner Guide
  Register/Login: Create an account or log in if you already have one.
  Add Products: Navigate to the 'Add Product' section and fill in the details.
  Manage Orders: View and manage orders from the 'Orders' section.
  View Analytics: Check your sales and customer insights in the 'Analytics' section.
  
Buyer Guide
  Register/Login: Create an account or log in if you already have one.
  Browse Products: Use the search and filter options to find products.
  Add to Cart: Add desired products to your shopping cart.
  Make Order: Proceed to checkout, fill in the necessary details, and make a payment.

Contributing
  We welcome contributions from the community! Please follow these steps to contribute:
Fork the repository.
  Create a new branch for your feature or bug fix.
  Commit your changes and push the branch to your fork.
  Create a pull request detailing your changes.

Contact
  For any queries or support, please contact us at abdelrhmansayedyounis@gmail.com.

Thank you for using StoreApplication! Happy shopping!



