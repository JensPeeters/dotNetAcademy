import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfielComponent } from './profiel.component';
import { HttpClientModule } from '@angular/common/http';
import { MsalService } from '../services/msal.service';

describe('ProfielComponent', () => {
  let component: ProfielComponent;
  let fixture: ComponentFixture<ProfielComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule],
      declarations: [ ProfielComponent ],
      providers: [MsalService]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfielComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
