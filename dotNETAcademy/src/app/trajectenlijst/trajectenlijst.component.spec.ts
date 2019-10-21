import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TrajectenlijstComponent } from './trajectenlijst.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ProductComponent } from '../product/product.component';

describe('TrajectenlijstComponent', () => {
  let component: TrajectenlijstComponent;
  let fixture: ComponentFixture<TrajectenlijstComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [FormsModule, HttpClientModule],
      declarations: [ TrajectenlijstComponent, ProductComponent ]
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
