How to setup the Code:

📌 Prerequisites
Install Visual Studio 2022 (with .NET Core SDK).
Install SQL Server Express or use (localdb) (comes with VS).

📌 Steps to Run
Clone/Open Project

Open solution in Visual Studio 2022.
…\Companyplanner\CompanyPlanner\CompanyPlanner.sln

👉 Set Startup Project
Right-click CompanyPlanner → Set as Startup Project.

👉Database Setup
Open SQL Server Management Studio (or VS SQL Server Object Explorer).
Run the provided DatabaseScript to create CompanyPlannerDb and required tables.
Check Connection String (appsettings.json)

 "ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=CompanyPlannerDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
If using SQL Express:
 "Server=.;Database=CompanyPlannerDb;Trusted_Connection=True;"


👉Run the API
Select HTTPS profile in VS → Press F5.
Swagger/Endpoints will open in browser.

👉DatabaseScript:

CREATE TABLE Customers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL
);

CREATE TABLE CustomerImages (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT NOT NULL,
    ImageBase64 NVARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT FK_CustomerImages_Customers
        FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
        ON DELETE CASCADE
);


INSERT INTO Customers (Name)
VALUES (N'CompanyPlanner');

📌 Frontend Setup (React)
Open Terminal
Navigate to the React project folder:
 cd CompanyPlanner/frontend-code/client

👉Install Dependencies
 npm install
npm install axios react-responsive-carousel
Create .env or .env.local file
And and add 
REACT_APP_API_URL=https://localhost:7125

👉 Run the Frontend
 npm start
 
Access App
Open http://localhost:3000 in browser.

Frontend:

