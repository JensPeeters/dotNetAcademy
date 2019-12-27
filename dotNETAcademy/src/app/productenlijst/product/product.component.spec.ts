import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductComponent } from './product.component';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientModule } from '@angular/common/http';
import { MsalService } from 'src/app/services/msal.service';
import { testCursus1 } from '../productenlijst.component.spec';
import { CurrencyPipe } from '@angular/common';

describe('ProductComponent', () => {
  let component: ProductComponent;
  let fixture: ComponentFixture<ProductComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ HttpClientModule, RouterTestingModule ],
      declarations: [ ProductComponent ],
      providers: [MsalService]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductComponent);
    component = fixture.componentInstance;
    component.product = testCursus1;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should show correct type', () => {
    const htmlElement : HTMLElement = fixture.nativeElement;
    const text = htmlElement.querySelector('h6');
    expect(text.innerHTML).toBe("Type: " + testCursus1.type);
  });

  it('should show correct title', () => {
    const htmlElement : HTMLElement = fixture.nativeElement;
    const text = htmlElement.querySelector('h4');
    expect(text.innerHTML).toBe(testCursus1.titel);
  });

  it('should show correct beschrijving', () => {
    const htmlElement : HTMLElement = fixture.nativeElement;
    const text = htmlElement.querySelector('.card-text');
    expect(text.innerHTML).toBe(testCursus1.beschrijving);
  });

  it('should show correct prijs with pipe', () => {
    const htmlElement : HTMLElement = fixture.nativeElement;
    const text = htmlElement.querySelector('strong');
    let pipe = new CurrencyPipe('en');
    let prijs = pipe.transform(testCursus1.prijs, ' € ');
    expect(text.innerHTML).toBe(prijs);
  });

});
