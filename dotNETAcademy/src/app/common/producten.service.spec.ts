import { TestBed } from '@angular/core/testing';

import { ProductenService } from './producten.service';
import { HttpClientModule } from '@angular/common/http';

describe('ProductenService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [HttpClientModule]
  }));

  it('should be created', () => {
    const service: ProductenService = TestBed.get(ProductenService);
    expect(service).toBeTruthy();
  });
});
