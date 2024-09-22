USE MedDb;

CREATE TABLE PharmacyMedicineSupply(
	SupplyID INT IDENTITY(1,1) PRIMARY KEY,
	PharmacyID INT REFERENCES Pharmacies(PharmacyID) NOT NULL,
	MedID INT REFERENCES MedicineStock(MedID) NOT NULL,
	SupplyCount INT DEFAULT 0 CHECK (SupplyCount >= 0) NOT NULL,
	CONSTRAINT UQ_Pharmacy_Medicine UNIQUE (PharmacyID, MedID)
);

SELECT * FROM PharmacyMedicineSupply;
