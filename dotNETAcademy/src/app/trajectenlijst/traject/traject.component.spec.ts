import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TrajectComponent } from './traject.component';

describe('TrajectComponent', () => {
  let component: TrajectComponent;
  let fixture: ComponentFixture<TrajectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TrajectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TrajectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
