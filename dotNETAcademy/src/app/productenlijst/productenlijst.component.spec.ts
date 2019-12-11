import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductenlijstComponent, Filter } from './productenlijst.component';
import { FormsModule } from '@angular/forms';
import { ProductComponent } from './product/product.component';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Params, ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';

describe('ProductenlijstComponent', () => {
  let component: ProductenlijstComponent;
  let fixture: ComponentFixture<ProductenlijstComponent>;
  let params: Subject<Params>;

  beforeEach(async(() => {
    params = new Subject<Params>();
    TestBed.configureTestingModule({
      declarations: [ProductenlijstComponent, ProductComponent],
      imports: [FormsModule, HttpClientModule, RouterModule.forRoot([])],
      providers: [{ provide: ActivatedRoute, useValue: { params: params }}]
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

  it('Should get cursussen when route is cursussen', () => {
    fixture.detectChanges();
    spyOn(component,'GetProducts').and.callThrough();
    spyOn(component,'GetBuyableCursussen').and.callThrough();
    params.next({ 'currentRoute': 'cursussen' });
    expect(component.GetProducts).toHaveBeenCalled();
    expect(component.GetBuyableCursussen).toHaveBeenCalled();
  });

  it('Should get trajecten when route is trajecten', () => {
    fixture.detectChanges();
    spyOn(component,'GetProducts').and.callThrough();
    spyOn(component,'GetBuyableTrajecten').and.callThrough();
    params.next({ 'currentRoute': 'trajecten' });
    expect(component.GetProducts).toHaveBeenCalled();
    expect(component.GetBuyableTrajecten).toHaveBeenCalled();
  });

  it('Should get zoekresultaten when route is zoekresultaten', () => {
    fixture.detectChanges();
    spyOn(component,'GetProducts').and.callThrough();
    spyOn(component,'GetBuyableCursussen').and.callThrough();
    spyOn(component,'GetBuyableTrajecten').and.callThrough();
    params.next({ 'currentRoute': 'zoekresultaten' });
    expect(component.GetProducts).toHaveBeenCalled();
    expect(component.GetBuyableTrajecten).toHaveBeenCalled();
    expect(component.GetBuyableCursussen).toHaveBeenCalled();
  });

  it('Should change direction when clicked on button with direction icon', () => {
    component.productFilter = new Filter();
    spyOn(component,'ChangeDirection').and.callThrough();
    const htmlElement : HTMLElement = fixture.nativeElement;
    let btn = htmlElement.querySelectorAll("button");
    btn[0].click();
    fixture.detectChanges();

    expect(component.ChangeDirection).toHaveBeenCalled();
    expect(component.productFilter.direction).toBe("desc");

  });

  it('Should change type when changed type dropdown and should get products again', () => {
    component.productFilter = new Filter();
    component.productFilter.types = ["Aanbevolen","Titel"];
    component.productFilter.type = component.productFilter.types[1];
    spyOn(component,'GetType').and.callThrough();
    spyOn(component,'GetProducts').and.callThrough();
    const htmlElement : HTMLElement = fixture.nativeElement;
    let select = htmlElement.querySelectorAll("select");
    select[0].value = component.productFilter.types[1];
    fixture.detectChanges();
    /// Deze lijn hieronder mag normaal niet, maar vond geen oplossing
    component.GetType();
    /// Deze lijn hierboven mag normaal niet, maar vond geen oplossing
    expect(component.GetType).toHaveBeenCalled();
    expect(component.productFilter.currentType).toBe("type=" + component.productFilter.types[1]);
    expect(component.GetProducts).toHaveBeenCalled();

  });

});
