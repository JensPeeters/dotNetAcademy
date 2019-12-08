import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BestellingComponent } from './bestelling.component';
import { RouterModule } from '@angular/router';
import { IBestelling } from 'src/app/Interfaces/IBestelling';
import { DatePipe } from '@angular/common';

describe('BestellingComponent', () => {
  let component: BestellingComponent;
  let fixture: ComponentFixture<BestellingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BestellingComponent ],
      imports: [RouterModule.forRoot([])]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BestellingComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should show the correct bestellingsnummer', () => {
    component.bestelling = testBestelling;
    fixture.detectChanges();
    const htmlElement : HTMLElement = fixture.nativeElement;
    const text = htmlElement.querySelectorAll('.card-text>span');
    fixture.detectChanges();
    expect(text[2].innerHTML).toBe("Bestellingsnummer: 51215");
  });

  it('should show the correct datum in the correct format', () => {
    component.bestelling = testBestelling;
    fixture.detectChanges();
    const htmlElement : HTMLElement = fixture.nativeElement;
    const text = htmlElement.querySelectorAll('.card-text>span');
    fixture.detectChanges();
    let pipe = new DatePipe('en');
    expect(text[0].innerHTML).toBe(pipe.transform(testBestelling.datum, 'd MMMM yyyy' + " "));
  });

});

export const testBestelling: IBestelling = {
  id: 51215,
  datum: new Date(),
  producten: null,
  klant: null,
  totaalPrijs: 100
};