CREATE DATABASE MedDb;
USE MedDb;

CREATE TABLE MedicineStock(
	MedID INT IDENTITY(1,1) PRIMARY KEY,
	MedName VARCHAR(100) NOT NULL, 
	ChemicalComposition TEXT NOT NULL, 
	TargetAilment VARCHAR(255) NOT NULL, 
	DateOfExpiry DATE NOT NULL, 
	NumberOfTabletsInStock INT DEFAULT 0 CHECK (NumberOfTabletsInStock >= 0) NOT NULL
);

INSERT INTO MedicineStock (MedName, ChemicalComposition, TargetAilment, DateOfExpiry, NumberOfTabletsInStock)
VALUES ('Orthoherb', 'Cedrus deodara, Vitex negundo, Boswellia serrata, Zingiber officinale, Withania somnifera, Pongamia pinnata', 'Orthopaedics', '2025-12-31', 100),
('Cholecalciferol', 'Cholecalciferol (Vitamin D3)', 'Orthopaedics', '2025-06-15', 200),
('Gaviscon', 'Sodium alginate, Sodium bicarbonate, Calcium carbonate', 'General', '2025-03-01', 150),
('Dolo-650', 'Paracetamol (650 mg)', 'General', '2024-12-30', 500),
('Cyclopam', 'Dicyclomine hydrochloride (20 mg), Paracetamol (500 mg)', 'Gynaecology', '2025-09-20', 120),
('Hilact', 'Probiotic strains, Shatavari (Asparagus racemosus)', 'Gynaecology', '2025-05-10', 75);

SELECT * FROM MedicineStock;



