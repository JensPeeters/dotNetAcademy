import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TrajectenlijstComponent } from './trajectenlijst.component';

describe('TrajectenlijstComponent', () => {
  let component: TrajectenlijstComponent;
  let fixture: ComponentFixture<TrajectenlijstComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TrajectenlijstComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TrajectenlijstComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
