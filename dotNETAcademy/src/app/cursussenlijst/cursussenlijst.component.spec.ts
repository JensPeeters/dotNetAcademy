import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CursussenlijstComponent } from './cursussenlijst.component';

describe('CursussenlijstComponent', () => {
  let component: CursussenlijstComponent;
  let fixture: ComponentFixture<CursussenlijstComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CursussenlijstComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CursussenlijstComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
