# Pharmacy-medicine-supply-management-System
This project was created by Amogh Banerjee.
It was done as a Capstone Project, which was a part of the final phase of Wipro NGA Training on Dot Net and Angular.

1. Setting up this project

1.1 Setting up the `MedDb` database
To set up the database, you need to ensure that you have Microsoft SQL Server installed.

1.1.1 Navigate to the folder `MedDb Queries`
The queries to create and set up each table are present here.

1.1.2 Create the database and set up the `MedicineStock` table
Execute the queries present in the `MedicineStockQueries.sql` file.

1.1.3 Set up all the remaining tables
Execute the queries present in the following files, one by one:
    `MedicalRepQueries.sql` for `MedicalRep` table.
    `RepScheduleQueries.sql` for `RepSchedule` table.
    `MedicineDemandQueries.sql` for `MedicineDemand` table.
    `PharmaciesQueries.sql` for `Pharmacies` table.
    `PharmacyMedicineSupplyQueries.sql` for `PharmacyMedicineSupply` table.

1.2 Ensure that you have MongoDB Community Server installed
It will be used for `AuthorizationDb` database.

1.3 Setting up the `PharmacyMedicineSupplyManagement` API

1.3.1 Ensure that you have Microsoft Visual Studio installed with the required workloads.

1.3.2 Update the connection string for SQL Server
Navigate to the file `SolutionPharmacyMedicineSupplyManagementAPI\PharmacyMedicineSupplyManagementAPI\appsettings.json`.
Change the value of "DefaultConnection" to the connection string of your database.

1.4 Setting up the `PharmacyMedicineSupplyPortal` Angular project

1.4.1 Ensure that you have Angular CLI v14.2.5 and a compatible version of Node installed.

1.4.2 Update the API URLs if needed

1.4.2.1 Get the API endpoint URLs
Open the APIs in Visual Studio and start them by clicking the `https` button.
Get the endpoint URLs from the Swagger UI.

1.4.2.2 Update the API URLs in the Angular project
Navigate to `PharmacyMedicineSupplyPortal\src\Services` and for each service, update the API URLs if needed.

2. Running this project

2.1 Ensure that SQL Server and MongoDb Server are running.

2.2 Start the APIs
Open the projects `AuthorizationAPI` and `PharmacyMedicineSupplyManagementAPI` in Visual Studio and start them by clicking the `https` button.

2.3 Start the Angular Server
Open Command Prompt and navigate to the folder `PharmacyMedicineSupplyPortal`.
Enter the command `ng serve`.

2.4 Use the App
Once the build finishes and the server starts,
open your web browser and navigate to `http://localhost:4200/`.

Now you can use the Pharmacy Medicine Supply Portal!