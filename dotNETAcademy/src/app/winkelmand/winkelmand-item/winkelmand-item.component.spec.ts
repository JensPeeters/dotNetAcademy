import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WinkelmandItemComponent } from './winkelmand-item.component';

describe('WinkelmandItemComponent', () => {
  let component: WinkelmandItemComponent;
  let fixture: ComponentFixture<WinkelmandItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WinkelmandItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WinkelmandItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
