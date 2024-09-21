import { TestBed } from '@angular/core/testing';

import { MedicineStockService } from './medicine-stock.service';

describe('MedicineStockService', () => {
  let service: MedicineStockService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MedicineStockService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
