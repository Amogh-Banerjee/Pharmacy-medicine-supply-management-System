USE MedDb;

CREATE TABLE MedicalRep(
	MedRepID INT IDENTITY(1,1) PRIMARY KEY,
	MedRepName VARCHAR(20) NOT NULL,
);

INSERT INTO MedicalRep (MedRepName) VALUES
('R1'), ('R2'), ('R3');

SELECT * FROM MedicalRep;