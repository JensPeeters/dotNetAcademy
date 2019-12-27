import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BestellingInfoComponent } from './bestelling-info.component';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientModule } from '@angular/common/http';
import { IBestelling } from '../Interfaces/IBestelling';
import { BestellingenService } from '../services/bestellingen.service';
import 'rxjs/add/observable/of';
import { Observable } from 'rxjs';
import { DatePipe } from '@angular/common';
import { IProduct } from '../Interfaces/IProduct';
import { IProducten } from '../Interfaces/IProducten';

describe('BestellingInfoComponent', () => {
  let component: BestellingInfoComponent;
  let fixture: ComponentFixture<BestellingInfoComponent>;
  let testBestellingservice: BestellingenService = new BestellingenService(null);

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports : [ RouterTestingModule, HttpClientModule ],
      declarations: [ BestellingInfoComponent ],
      providers: [
        { provide: BestellingenService, useValue: testBestellingservice }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BestellingInfoComponent);
    component = fixture.componentInstance;
    spyOn(testBestellingservice, 'GetBestellingById').and.returnValue(Observable.of(testBestelling));
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should show the correct bestellingsnummer', () => {
    const htmlElement : HTMLElement = fixture.nativeElement;
    const text = htmlElement.querySelectorAll('p>span');
    expect(text[0].innerHTML).toBe("51215");
  });

  it('should show the correct datum in the correct format', () => {
    const htmlElement : HTMLElement = fixture.nativeElement;
    const text = htmlElement.querySelectorAll('p>span');
    let pipe = new DatePipe('en');
    let datum = pipe.transform(testBestelling.datum, 'd MMMM yyyy');
    let tijdstip = pipe.transform(testBestelling.datum, 'HH:mm');
    expect(text[1].innerHTML).toBe(datum + " om " + tijdstip);
  });

  it('should show all the bestellingen', () => {
    const htmlElement : HTMLElement = fixture.nativeElement;
    const divs = htmlElement.querySelectorAll('.imageScaler');
    expect(divs.length).toBe(testBestelling.producten.length);
  });

  it('should show the title and amount of all the bestellingen', () => {
    const htmlElement : HTMLElement = fixture.nativeElement;
    const divs = htmlElement.querySelectorAll('h5');
    expect(divs[0].innerHTML).toBe(testBestelling.producten[0].aantal + " x " + testBestelling.producten[0].product.titel);
    expect(divs[1].innerHTML).toBe(testBestelling.producten[1].aantal + " x " + testBestelling.producten[1].product.titel);
  });

});

export const testProduct: IProduct = {
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
}
export const testBestellingProducten: IProducten[] = [
  {
    id: 1,
    product: testProduct,
    aantal: 2
  },
  {
    id: 2,
    product: testProduct,
    aantal: 2
  },
];
export const testBestelling: IBestelling = {
  id: 51215,
  datum: new Date(),
  producten: testBestellingProducten,
  klant: null,
  totaalPrijs: 100
};