import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductenlijstComponent } from './productenlijst.component';
import { FormsModule } from '@angular/forms';
import { ProductComponent } from './product/product.component';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

describe('ProductenlijstComponent', () => {
  let component: ProductenlijstComponent;
  let fixture: ComponentFixture<ProductenlijstComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ProductenlijstComponent, ProductComponent],
      imports: [FormsModule, HttpClientModule, RouterModule.forRoot([])]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductenlijstComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
