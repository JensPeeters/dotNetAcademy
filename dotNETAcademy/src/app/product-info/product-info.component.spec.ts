import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductInfoComponent } from './product-info.component';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientModule } from '@angular/common/http';
import { MsalService } from '../services/msal.service';
import { ActivatedRoute, Params } from '@angular/router';
import { Subject, Observable } from 'rxjs';
import { ProductenService } from '../services/producten.service';
import { ICursus } from '../Interfaces/ICursus';
import { ITraject } from '../Interfaces/ITraject';
import { WinkelmandService } from '../services/winkelmand.service';
import { IWinkelmand } from '../Interfaces/IWinkelmand';

describe('ProductInfoComponent', () => {
  let component: ProductInfoComponent;
  let fixture: ComponentFixture<ProductInfoComponent>;
  let params: Subject<Params>;
  let testProductservice: ProductenService = new ProductenService(null);
  let testWinkelmandService: WinkelmandService = new WinkelmandService(null);

  beforeEach(async(() => {
    params = new Subject<Params>();
    TestBed.configureTestingModule({
      imports : [ RouterTestingModule, HttpClientModule ],
      declarations: [ ProductInfoComponent ],
      providers : [ MsalService,
        { provide: ActivatedRoute, useValue: { params: params }},
        { provide: ProductenService, useValue: testProductservice },
        { provide: WinkelmandService, useValue: testWinkelmandService }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductInfoComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('Should get a cursus when route is Cursus', () => {
    let cursus = new Promise<ICursus>((resolve, reject) => {});
    spyOn(testProductservice, 'GetCursusById').and.returnValue(cursus);
    fixture.detectChanges();
    spyOn(component,'GetCursus').and.callThrough();
    params.next({ 'currentRoute': 'Cursus' });
    expect(component.GetCursus).toHaveBeenCalled();
  });

  it('Should get a traject when route is Traject', () => {
    let traject = new Promise<ITraject>((resolve, reject) => {});
    spyOn(testProductservice, 'GetTrajectById').and.returnValue(traject);
    fixture.detectChanges();
    spyOn(component,'GetTraject').and.callThrough();
    params.next({ 'currentRoute': 'Traject' });
    expect(component.GetTraject).toHaveBeenCalled();
  });

  it('Should add product to winkelmand when clicked on button named Toevoegen aan winkelwagen', () => {
    let product = new Observable<IWinkelmand>();
    spyOn(testWinkelmandService, 'AddToWinkelmand').and.returnValue(product);
    component.product = [
      {id: 2},
      {prijs: 50},
      {categorie: "string"},
      {fotoURLCard: "string"},
      {type: "string"},
      {beschrijving: "string"},
      {langeBeschrijving: "string"},
      {titel: "string"}
    ];
    fixture.detectChanges();
    spyOn(component,'AddToCart').and.callThrough();
    const htmlElement : HTMLElement = fixture.nativeElement;
    let btn = htmlElement.querySelectorAll("button");
    btn[1].click();
    fixture.detectChanges();

    expect(component.AddToCart).toHaveBeenCalled();
  });

  it('Should go back when clicked on button named Terug', () => {
    component.product = [
      {id: 2},
      {prijs: 50},
      {categorie: "string"},
      {fotoURLCard: "string"},
      {type: "string"},
      {beschrijving: "string"},
      {langeBeschrijving: "string"},
      {titel: "string"}
    ];
    fixture.detectChanges();
    spyOn(component,'goBack').and.callThrough();
    const htmlElement : HTMLElement = fixture.nativeElement;
    let btn = htmlElement.querySelectorAll("button");
    btn[0].click();
    fixture.detectChanges();

    expect(component.goBack).toHaveBeenCalled();
  });

});
