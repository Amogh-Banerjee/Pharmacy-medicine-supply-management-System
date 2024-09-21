import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Supply } from 'src/Models/Supply';
import { SupplyService } from 'src/Services/supply.service';

@Component({
  selector: 'app-medicine-supply',
  templateUrl: './medicine-supply.component.html',
  styleUrls: ['./medicine-supply.component.css']
})
export class MedicineSupplyComponent implements OnInit {

  pharmacySupplies: Supply[] = [];

  demands: any[];
  allStock: any[];

  constructor(private router: Router, private supplyService: SupplyService) {
    const nav = this.router.getCurrentNavigation();
    this.demands = nav?.extras.state?.['demands'] || [];
    this.allStock = nav?.extras.state?.['allStock'] || [];
  }

  ngOnInit(): void {
    if (this.demands.length > 0) {
      this.supplyService.getPharmacySupply(this.demands, this.allStock)
        .subscribe((response) => {
          this.pharmacySupplies = response;
        }, error => {
          console.error('Error fetching pharmacy supplies', error);
        });
    }
  }

}
