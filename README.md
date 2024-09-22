# Pharmacy Medicine Supply Management System

This project was developed by **Amogh Banerjee** as a Capstone Project, part of the final phase of **Wipro NGA Training** on .NET and Angular.

## 1. Setting Up the Project

### 1.1. Setting Up the `MedDb` Database

To get started with the database setup, ensure you have **Microsoft SQL Server** installed.

#### 1.1.1. Navigate to the `MedDb Queries` Folder  
This folder contains the SQL queries required to create and set up each table.

#### 1.1.2. Create the Database and Set Up the `MedicineStock` Table  
Execute the queries found in the `MedicineStockQueries.sql` file to set up the `MedicineStock` table.

#### 1.1.3. Set Up the Remaining Tables  
Execute the following SQL scripts, in this order, to set up the remaining tables:
- `MedicalRepQueries.sql` – for the `MedicalRep` table.
- `RepScheduleQueries.sql` – for the `RepSchedule` table.
- `MedicineDemandQueries.sql` – for the `MedicineDemand` table.
- `PharmaciesQueries.sql` – for the `Pharmacies` table.
- `PharmacyMedicineSupplyQueries.sql` – for the `PharmacyMedicineSupply` table.

### 1.2. Ensure MongoDB is Installed  
You'll need **MongoDB Community Server** installed for the `AuthorizationDb` database, used for managing authentication.

### 1.3. Setting Up the `PharmacyMedicineSupplyManagement` API

#### 1.3.1. Install Microsoft Visual Studio  
Ensure **Microsoft Visual Studio** is installed with the required workloads (ASP.NET and web development).

#### 1.3.2. Update SQL Server Connection String  
Navigate to the file:  
`SolutionPharmacyMedicineSupplyManagementAPI\PharmacyMedicineSupplyManagementAPI\appsettings.json`  
Modify the `DefaultConnection` string with your own SQL Server connection details.

### 1.4. Setting Up the `PharmacyMedicineSupplyPortal` Angular Project

#### 1.4.1. Ensure Angular CLI and Node.js are Installed  
Install **Angular CLI v14.2.5** and a compatible version of **Node.js**.

#### 1.4.2. Update the API URLs (If Needed)

##### 1.4.2.1. Retrieve API Endpoint URLs  
Start the APIs from Visual Studio by clicking the `https` button. You can find the endpoint URLs on the Swagger UI page.

##### 1.4.2.2. Update the API URLs in the Angular Project  
Navigate to:  
`PharmacyMedicineSupplyPortal\src\Services`  
Update the API URLs in each service file with the correct endpoints if needed.

## 2. Running the Project

### 2.1. Start SQL Server and MongoDB  
Ensure both **SQL Server** and **MongoDB** are running.

### 2.2. Start the APIs  
Open **AuthorizationAPI** and **PharmacyMedicineSupplyManagementAPI** in Visual Studio. Start both by clicking the `https` button.

### 2.3. Start the Angular Server  
Open **Command Prompt**, navigate to the `PharmacyMedicineSupplyPortal` folder, and run:  
```bash
ng serve
```

### 2.4. Access the Application  
Once the build completes and the server starts, open your web browser and navigate to:  
[http://localhost:4200/](http://localhost:4200/)

You can now use the **Pharmacy Medicine Supply Portal**!

