import { TestBed, async, inject } from '@angular/core/testing';

import { MsalGuard } from './msal.guard';
import { HttpClientModule } from '@angular/common/http';
import { MsalService } from '../services/msal.service';

describe('MsalGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [MsalGuard, MsalService]
    });
  });

  it('should ...', inject([MsalGuard], (guard: MsalGuard) => {
    expect(guard).toBeTruthy();
  }));
});
