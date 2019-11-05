import { TestBed } from '@angular/core/testing';

import { WinkelmandService } from './winkelmand.service';

describe('WinkelmandService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: WinkelmandService = TestBed.get(WinkelmandService);
    expect(service).toBeTruthy();
  });
});
