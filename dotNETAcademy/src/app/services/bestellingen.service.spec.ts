import { TestBed } from '@angular/core/testing';

import { BestellingenService } from './bestellingen.service';
import { HttpClientModule } from '@angular/common/http';

describe('BestellingenService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [HttpClientModule]
  }));

  it('should be created', () => {
    const service: BestellingenService = TestBed.get(BestellingenService);
    expect(service).toBeTruthy();
  });
});
