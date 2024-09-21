import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MedicineStock } from 'src/Models/MedicineStock';
import { MedicineStockService } from 'src/Services/medicine-stock.service';

@Component({
  selector: 'app-medicine-demand',
  templateUrl: './medicine-demand.component.html',
  styleUrls: ['./medicine-demand.component.css']
})
export class MedicineDemandComponent implements OnInit {

  medicineForm!: FormGroup;
  medicines: MedicineStock[] = [];

  constructor(private fb: FormBuilder, private router: Router, private medicineStockService: MedicineStockService) { }

  ngOnInit(): void {
    this.medicineForm = this.fb.group({});

    this.medicineStockService.getMedicineStockInfo().subscribe((data) => {
      this.medicines = data;

      // Create form controls dynamically based on medicines
      this.medicines.forEach((medicine) => {
        this.medicineForm.addControl(
          `demand_${medicine.medId}`,
          this.fb.control('')
        );
      });
    });
  }


  onInputChange(index: number): void {
    const controlName = `demand_${index}`;
    let value = this.medicineForm.get(controlName)?.value;

    // Remove any non-numeric characters
    value = value.replace(/[^0-9]/g, '');

    // Ensure it is greater than zero
    value = value && parseInt(value, 10) > 0 ? parseInt(value, 10) : '';

    // Update the form control with the sanitized value
    this.medicineForm.get(controlName)?.setValue(value, { emitEvent: false });
  }

  onSubmit(): void {
    if (this.medicineForm.valid) {
      const demands = this.medicines
        .map((medicine) => {
          // Use the medicine ID to find the demand in the form
          const controlName = `demand_${medicine.medId}`;
          const demand = this.medicineForm.get(controlName)?.value;
  
          return {
            medId: medicine.medId,
            demandCount: demand ? parseInt(demand, 10) : 0
          };
        })
        .filter((demand) => demand.demandCount > 0); // Keep only valid demands
  
      if (demands.length > 0) {
        // Navigate to the pharmacy supply component and pass the demands and allStock
        this.router.navigate(['/medicineSupply'], {
          state: { demands: demands, allStock: this.medicines }
        });
      } else {
        alert('Please enter a demand greater than zero for at least one medicine.');
      }
    } else {
      console.log("Form is invalid");
    }
  }
  


}
