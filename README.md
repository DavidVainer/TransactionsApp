# 💱 Transactions App

## 📋 Overview
This application can create transactions against a banking provider, and modify or delete historical transactions.  
Historical transactions are displayed in  a table with their information and possible actions.

- **Server Side:** .NET Core 8  
- **Client Side:** Vue.js 2  
- **Database:** MSSQL (SQL Server)

## ✅ Prerequisites
Before installing and running the application, please make sure that:

- **.NET 8** is installed on your computer.
- **Node.js** is installed on your computer.
- **SQL Server** is installed on your computed.
- There's no **TransactionsAppDb** named database.
- The following ports are **not in use**:
  - **5001** (used by the server)
  - **8080** (used by the client)

## 🛠️ Installation
- Clone the project and navigate to the folder.
- ### **Setting up the Server**
   - Navigate to the server folder `TransactionsApp.Server/TransactionsApp.API`.
   - Open the `appsettings.json` file.
   - Update the `ConnectionStrings` section to point to your SQL Server instance. Example:
   ````
     "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Database=TransactionsAppDb;Trusted_Connection=True;TrustServerCertificate=TrueMultipleActiveResultSets=true"
      }
   ````
   - Execute the following command to apply migrations and create the database:
   ````
   dotnet tool install --global dotnet-ef --version 8.* // If not installed yet
   dotnet ef database update
   ````
   - Start the server using the following command:
   ````
   dotnet run
   ````
   - The server should start at `https://localhost:5001` by default.
- ### **Setting up the Client**
   - Navigate to the client Folder `TransactionsApp.Client/transactions-app-client`.
   - Run the following command to install all required dependencies:
   ````
   npm install
   ````
   - Start the client using the following command:
   ````
   npm run serve
   ````
   - The client should be available at `http://localhost:8080` by default.

## 💸 Using the app
To start transactioningt, follow these steps:

- Navigate to the GUI application at [TransactionsApp (Click me!)](http://localhost:8080/).
- To add a new transaction, click on **New Transaction** in the app bar.
   ![AppBar](./assets/appbar.png)
- Fill all of the fields according to the input restrictions and click on **Submit**.  
  After submittion it will show success / failure indication.
   ![Form](./assets/form.png)
- Navigate to the transactions history by clicking on **Transactions List** in the app bar.
- This page will display all transactions history with their details.
   ![List](./assets/list.png)

### **Transaction actions:**
- **To modify a transaction**:
   - Click on the modification button.
   - You'll be redirected to the transaction form page.
   - Modify the enabled inputs and submit.
   ![Modify](./assets/modify.png)
- **To cancel (delete) a transaction:**
   - Click on the cancelation button.
   - Confirm the confirmation window.
   - The transaction will be deleted (soft delete) from the list.
   ![Delete](./assets/delete.png)


## 🧪 Run unit tests
- Go to `TransactionsApp.Server` folder inside the project.
- Execute `dotnet test` in a command line.

-------
![App](./assets/app.png)