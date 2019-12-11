import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RouterTestingModule } from '@angular/router/testing';

import { ProductenBeheerComponent } from './producten-beheer.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

describe('ProductenBeheerComponent', () => {
  let component: ProductenBeheerComponent;
  let fixture: ComponentFixture<ProductenBeheerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports:[RouterTestingModule, FormsModule, HttpClientModule ],
      declarations: [ ProductenBeheerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductenBeheerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
