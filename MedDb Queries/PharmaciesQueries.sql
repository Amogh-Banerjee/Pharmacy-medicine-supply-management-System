USE MedDb;

CREATE TABLE Pharmacies(
	PharmacyID INT IDENTITY(1,1) PRIMARY KEY,
	PharmacyName VARCHAR(20) NOT NULL
);

INSERT INTO Pharmacies (PharmacyName) VALUES 
('HealthCare Pharmacy'), ('CityMed Pharmacy'), ('Wellness Corner'), ('PharmaPlus'), ('Greenfield Pharmacy');

SELECT * FROM Pharmacies;