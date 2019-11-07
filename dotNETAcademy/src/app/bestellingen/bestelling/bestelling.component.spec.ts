import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BestellingComponent } from './bestelling.component';

describe('BestellingComponent', () => {
  let component: BestellingComponent;
  let fixture: ComponentFixture<BestellingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BestellingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BestellingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
