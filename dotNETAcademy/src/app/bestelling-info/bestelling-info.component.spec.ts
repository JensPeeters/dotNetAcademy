import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BestellingInfoComponent } from './bestelling-info.component';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientModule } from '@angular/common/http';

describe('BestellingInfoComponent', () => {
  let component: BestellingInfoComponent;
  let fixture: ComponentFixture<BestellingInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports : [ RouterTestingModule, HttpClientModule ],
      declarations: [ BestellingInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BestellingInfoComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
