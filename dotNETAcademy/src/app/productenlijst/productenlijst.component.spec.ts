import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductenlijstComponent, Filter } from './productenlijst.component';
import { FormsModule } from '@angular/forms';
import { ProductComponent } from './product/product.component';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Params, ActivatedRoute } from '@angular/router';
import { Subject, Observable } from 'rxjs';
import { ProductenService } from '../services/producten.service';
import { IProduct } from '../Interfaces/IProduct';
import 'rxjs/add/observable/of';

describe('ProductenlijstComponent', () => {
  let component: ProductenlijstComponent;
  let fixture: ComponentFixture<ProductenlijstComponent>;
  let params: Subject<Params>;
  let testProductservice: ProductenService = new ProductenService(null);
  
  beforeEach(async(() => {
    params = new Subject<Params>();
    TestBed.configureTestingModule({
      declarations: [ProductenlijstComponent, ProductComponent],
      imports: [FormsModule, HttpClientModule, RouterModule.forRoot([])],
      providers: [
        { provide: ActivatedRoute, useValue: { params: params }},
        { provide: ProductenService, useValue: testProductservice }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductenlijstComponent);
    component = fixture.componentInstance;
    let types = new Observable<string[]>();
    let cursussen = new Promise<IProduct[]>((resolve, reject) => {
      return testCursussen;
    });
    let trajecten = new Promise<IProduct[]>((resolve, reject) => {
      return testTrajecten;
    });
    spyOn(testProductservice, 'GetCursusTypes').and.returnValue(types)
    spyOn(testProductservice, 'GetBuyableCursussen').and.returnValue(cursussen);
    spyOn(testProductservice, 'GetTrajectTypes').and.returnValue(types);
    spyOn(testProductservice, 'GetBuyableTrajecten').and.returnValue(trajecten);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
 
  it('Should get cursussen when route is cursussen', () => {
    spyOn(component,'GetProducts').and.callThrough();
    spyOn(component,'GetBuyableCursussen').and.callThrough();
    params.next({ 'currentRoute': 'cursussen' });
    expect(component.GetProducts).toHaveBeenCalled();
    expect(component.GetBuyableCursussen).toHaveBeenCalled();
  });

  it('Should get trajecten when route is trajecten', () => {
    spyOn(component,'GetProducts').and.callThrough();
    spyOn(component,'GetBuyableTrajecten').and.callThrough();
    params.next({ 'currentRoute': 'trajecten' });
    expect(component.GetProducts).toHaveBeenCalled();
    expect(component.GetBuyableTrajecten).toHaveBeenCalled();
  });

  it('Should get zoekresultaten when route is zoekresultaten', () => {
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
    component.GetType();

    expect(component.GetType).toHaveBeenCalled();
    expect(component.productFilter.currentType).toBe("type=" + component.productFilter.types[1]);
    expect(component.GetProducts).toHaveBeenCalled();

  });

  it('Should show all the products of the list', () => {
    spyOn(component,'GetProducts').and.callThrough();
    params.next({ 'currentRoute': 'cursussen' });
    const htmlElement : HTMLElement = fixture.nativeElement;
    let cursussenAmount = htmlElement.querySelectorAll("section>div");
    fixture.detectChanges();

    expect(component.GetProducts).toHaveBeenCalled();
    expect(cursussenAmount.length).toBe(testCursussen.length);
  });

});

export const testCursus1: IProduct = {
  id: 15,
  prijs: 25,
  categorie: "Cursus",
  isBuyable: true,
  type: ".NET",
  titel: "Een cursus van .NET",
  beschrijving: "Dit is de korte beschrijving.",
  langeBeschrijving: "Dit is de lange beschrijving.",
  fotoURLCard: "https://via.placeholder.com/450x350.png/09f/fff?text=Foto van een product",
  cursussen: null
};
export const testCursus2: IProduct = {
  id: 16,
  prijs: 17,
  categorie: "Cursus",
  isBuyable: true,
  type: ".NET",
  titel: "Een 2de cursus van .NET",
  beschrijving: "Dit is de korte beschrijving van cursus 2.",
  langeBeschrijving: "Dit is de lange beschrijving van cursus 2.",
  fotoURLCard: "https://via.placeholder.com/450x350.png/09f/fff?text=Foto van een product",
  cursussen: null
};
export const testCursussen: IProduct[] = [testCursus1, testCursus2];

export const testTrajecten: IProduct[] = [{
  id: 15,
  prijs: 42,
  categorie: "Traject",
  isBuyable: true,
  type: ".NET",
  titel: "Een traject van .NET",
  beschrijving: "Dit is de korte beschrijving.",
  langeBeschrijving: "Dit is de lange beschrijving.",
  fotoURLCard: "https://via.placeholder.com/450x350.png/09f/fff?text=Foto van een product",
  cursussen: [testCursus1, testCursus2]
}];