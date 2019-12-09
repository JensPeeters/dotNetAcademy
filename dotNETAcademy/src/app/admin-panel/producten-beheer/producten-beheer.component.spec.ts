import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RouterTestingModule } from '@angular/router/testing';

import { ProductenBeheerComponent } from './producten-beheer.component';

describe('ProductenBeheerComponent', () => {
  let component: ProductenBeheerComponent;
  let fixture: ComponentFixture<ProductenBeheerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports:[RouterTestingModule ],
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
