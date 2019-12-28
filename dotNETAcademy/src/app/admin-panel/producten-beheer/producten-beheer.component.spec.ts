import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RouterTestingModule } from '@angular/router/testing';

import { ProductenBeheerComponent } from './producten-beheer.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { IProduct } from 'src/app/Interfaces/IProduct';
import { ProductenService } from 'src/app/services/producten.service';
import { Observable } from 'rxjs';

describe('ProductenBeheerComponent', () => {
  let component: ProductenBeheerComponent;
  let fixture: ComponentFixture<ProductenBeheerComponent>;
  let testProductservice: ProductenService = new ProductenService(null);

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports:[RouterTestingModule, FormsModule, HttpClientModule ],
      declarations: [ ProductenBeheerComponent ],
      providers: [
        { provide: ProductenService, useValue: testProductservice }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductenBeheerComponent);
    component = fixture.componentInstance;
    let cursussen = new Promise<IProduct[]>((resolve, reject) => {
      return testCursussen;
    });
    let trajecten = new Promise<IProduct[]>((resolve, reject) => {
      return testTrajecten;
    });
    let observable = new Observable<IProduct>();
    spyOn(testProductservice, 'GetCursussenAdmin').and.returnValues(cursussen);
    spyOn(testProductservice, 'GetTrajectenAdmin').and.returnValues(trajecten);
    fixture.detectChanges();
    component.Producten = testCursussen.concat(testTrajecten);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should show all the products (trajecten + cursussen', () => {
    const htmlElement : HTMLElement = fixture.nativeElement;
    let totalProducts = htmlElement.querySelectorAll("tbody>tr");
    
    expect(totalProducts.length).toBe(testCursussen.length + testTrajecten.length);
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
  cursussen: null,
  orderNumber: 1
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
  cursussen: null,
  orderNumber: 2
};
export const testTraject1 : IProduct = {
  id: 15,
  prijs: 42,
  categorie: "Traject",
  isBuyable: true,
  type: ".NET",
  titel: "Een traject van .NET",
  beschrijving: "Dit is de korte beschrijving.",
  langeBeschrijving: "Dit is de lange beschrijving.",
  fotoURLCard: "https://via.placeholder.com/450x350.png/09f/fff?text=Foto van een product",
  cursussen: [testCursus1, testCursus2],
  orderNumber: 1
};
export const testTraject2 : IProduct = {
  id: 16,
  prijs: 22,
  categorie: "Traject",
  isBuyable: true,
  type: ".NET",
  titel: "Een 2de traject van .NET",
  beschrijving: "Dit is de korte beschrijving van het 2de traject.",
  langeBeschrijving: "Dit is de lange beschrijving van het 2de traject.",
  fotoURLCard: "https://via.placeholder.com/450x350.png/09f/fff?text=Foto van een product",
  cursussen: [testCursus1, testCursus2],
  orderNumber: 2
};
export const testCursussen : IProduct[] = [testCursus1, testCursus2];
export const testTrajecten : IProduct[] = [testTraject1, testTraject2]