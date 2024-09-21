import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MedicineStock } from 'src/Models/MedicineStock';

@Injectable({
  providedIn: 'root'
})
export class MedicineStockService {

  private apiUrl = 'https://localhost:7297/api/MedicineStock/MedicineStockInformation'; 
  constructor(private http: HttpClient) {}

  getMedicineStockInfo(): Observable<MedicineStock[]> {
    return this.http.get<MedicineStock[]>(this.apiUrl);
  }
}
