import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CursussenlijstComponent } from './cursussenlijst.component';
import { ProductComponent } from '../product/product.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

describe('CursussenlijstComponent', () => {
  let component: CursussenlijstComponent;
  let fixture: ComponentFixture<CursussenlijstComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [FormsModule, HttpClientModule],
      declarations: [ CursussenlijstComponent, ProductComponent ]
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
