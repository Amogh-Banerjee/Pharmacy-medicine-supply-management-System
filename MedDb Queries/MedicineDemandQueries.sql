USE MedDb;

CREATE TABLE MedicineDemand(
	MedID INT PRIMARY KEY REFERENCES MedicineStock(MedID) NOT NULL,
	DemandCount INT DEFAULT 0 CHECK (DemandCount >= 0) NOT NULL
);

SELECT * FROM MedicineDemand;
