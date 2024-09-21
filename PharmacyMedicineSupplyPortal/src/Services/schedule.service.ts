import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MedicineStock } from 'src/Models/MedicineStock';
import { Schedule } from 'src/Models/Schedule';

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {

  private apiUrl = 'https://localhost:7297/api/MedicalRepresentativeSchedule/RepSchedule'; 

  constructor(private http: HttpClient) {}

  getRepSchedule(scheduleStartDate: string, allMedicines: MedicineStock[]): Observable<Schedule[]> {
    return this.http.post<Schedule[]>(
      `${this.apiUrl}?scheduleStartDate=${scheduleStartDate}`,
      allMedicines
    );
  }
}
