import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BestellingenComponent } from './bestellingen.component';

describe('BestellingenComponent', () => {
  let component: BestellingenComponent;
  let fixture: ComponentFixture<BestellingenComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BestellingenComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BestellingenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
