import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportBestellingComponent } from './report-bestelling.component';
import { RouterTestingModule } from '@angular/router/testing';

describe('ReportBestellingComponent', () => {
  let component: ReportBestellingComponent;
  let fixture: ComponentFixture<ReportBestellingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReportBestellingComponent ],
      imports: [ RouterTestingModule]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportBestellingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
