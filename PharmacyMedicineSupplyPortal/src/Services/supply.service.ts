import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Supply } from 'src/Models/Supply';

@Injectable({
  providedIn: 'root'
})
export class SupplyService {

  private apiUrl = 'https://localhost:7297/api/PharmacyMedicineSupply/PharmacySupply';
  
  constructor(private http: HttpClient) { }

  getPharmacySupply(demands: any[], allStock: any[]): Observable<Supply[]> {
    const payload = { demands, allStock };
    return this.http.post<Supply[]>(this.apiUrl, payload);
  }
}
