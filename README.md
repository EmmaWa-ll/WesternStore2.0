# WesternStore2.0

# Western Store – Console Application

This is a C# console application developed as part of a school assignment.
The application simulates a western themed store with user login, admin functionality and product handling.
All data is stored in MongoDB Atlas.

## Project Description
The application allows users to:
- Register and log in as customers
- Log in as an admin
- Access different menus depending on user role
- Store and retrieve data from MongoDB Atlas

User passwords are handled securely using hashing (BCrypt) and are never stored in plain text.

## Technologies Used
- C# (.NET Console Application)
- MongoDB Atlas
- BCrypt.Net (password hashing)

## Requirements
- .NET 
- MongoDB Atlas account (free tier is sufficient)

## How to Run the Application

### 1) Clone the repository
git clone <repository-url>

### 2) Configure MongoDB connection
Open the file:
appsettings.json

Add your own MongoDB Atlas connection string:

{
  "Mongo": {
    "ConnectionString": "",
    "DatabaseName": "WesternStoreDb"
  }
}

Note: The MongoDB connection string is intentionally not included in the repository for security reasons.

### 3) Run the application
Start the console application.

On first run the application will automatically:
- Create an admin account (if none exists)
- Seed products into the database (if the collection is empty)

## Admin Login (Auto Created)
An admin account is automatically created on the first run of the application for testing and evaluation purposes.

Username: admin
Password: admin4416

The password is hashed before being stored in the database.

## Security Notes
- No database credentials are stored in GitHub
- MongoDB connection strings are handled locally
- Passwords are stored as secure hashes using BCrypt
- The admin account is intended for demo/testing purposes only

## Project Structure
- Models – Data models (User, Product, etc.)
- Service – Business logic, authentication, seeders
- Menu – Console menus (Admin / Customer)
- Data – MongoDB CRUD functionality

## Notes
This project is developed for educational purposes only.
In a real production system, admin accounts and credentials would be handled differently (for example using environment variables or manual setup).

## Author
Emma Wall – .NET Cloud student
