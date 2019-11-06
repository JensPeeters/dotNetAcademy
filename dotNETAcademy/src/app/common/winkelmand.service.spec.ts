import { TestBed } from '@angular/core/testing';

import { WinkelmandService } from './winkelmand.service';
import { HttpClientModule } from '@angular/common/http';

describe('WinkelmandService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [HttpClientModule]
  }));

  it('should be created', () => {
    const service: WinkelmandService = TestBed.get(WinkelmandService);
    expect(service).toBeTruthy();
  });
});
