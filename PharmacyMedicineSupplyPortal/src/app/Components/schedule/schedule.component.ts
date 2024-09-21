import { Component, OnInit } from '@angular/core';
import { MedicineStock } from 'src/Models/MedicineStock';
import { Schedule } from 'src/Models/Schedule';
import { MedicineStockService } from 'src/Services/medicine-stock.service';
import { ScheduleService } from 'src/Services/schedule.service';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css']
})
export class ScheduleComponent implements OnInit {

  ngOnInit(): void {
    this.medicineStockService.getMedicineStockInfo().subscribe((data) => {
      this.medicineStock = data;
    });
  }

  startDate: string = '';  
  minDate: string = '';

  medicineStock: MedicineStock[] = [];

  schedules: Schedule[] = [];  

  constructor(private medicineStockService: MedicineStockService, private scheduleService: ScheduleService) {
    this.setMinDate();
  }

  setMinDate() {
    const today = new Date();
    const dd = String(today.getDate()).padStart(2, '0');
    const mm = String(today.getMonth() + 1).padStart(2, '0'); // January is 0!
    const yyyy = today.getFullYear();
    this.minDate = `${yyyy}-${mm}-${dd}`;
  }

  preventTyping(event: KeyboardEvent) {
    event.preventDefault();
  }

  onSubmit() {
    if (this.startDate && this.medicineStock.length > 0) {
      this.scheduleService
        .getRepSchedule(this.startDate, this.medicineStock)
        .subscribe((data) => {
          this.schedules = data;
        });
    }
  }

  

}
