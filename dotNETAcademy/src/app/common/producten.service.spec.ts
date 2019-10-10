import { TestBed } from '@angular/core/testing';

import { ProductenService } from './producten.service';

describe('ProductenService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ProductenService = TestBed.get(ProductenService);
    expect(service).toBeTruthy();
  });
});
