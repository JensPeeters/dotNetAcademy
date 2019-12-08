import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfielComponent } from './profiel.component';
import { HttpClientModule, HttpClient, HttpHandler } from '@angular/common/http';
import { MsalService } from '../services/msal.service';

describe('ProfielComponent', () => {
  let component: ProfielComponent;
  let fixture: ComponentFixture<ProfielComponent>;
  let testMsalService: MsalService = new MsalService(null);

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule],
      declarations: [ ProfielComponent ],
      providers: [{ provide: MsalService, useValue: testMsalService }]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfielComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should get and show the correct userFirstName', () => {
    spyOn(testMsalService, 'isLoggedIn').and.returnValue(true);
    spyOn(testMsalService, 'getUserFirstName').and.returnValue("John");
    spyOn(testMsalService, 'getUserFamilyName').and.returnValue("Dev");
    spyOn(testMsalService, 'getUserEmail').and.returnValue("john.dev@hotmail.com");
    fixture.detectChanges();
    const htmlElement : HTMLElement = fixture.nativeElement;
    const text = htmlElement.querySelectorAll('.info');
    fixture.detectChanges();
    expect(text[0].innerHTML).toBe("John");
  });

  it('should get and show the correct userFamilyName', () => {
    spyOn(testMsalService, 'isLoggedIn').and.returnValue(true);
    spyOn(testMsalService, 'getUserFirstName').and.returnValue("John");
    spyOn(testMsalService, 'getUserFamilyName').and.returnValue("Dev");
    spyOn(testMsalService, 'getUserEmail').and.returnValue("john.dev@hotmail.com");
    fixture.detectChanges();
    const htmlElement : HTMLElement = fixture.nativeElement;
    const text = htmlElement.querySelectorAll('.info');
    fixture.detectChanges();
    expect(text[1].innerHTML).toBe("Dev");
  });

  it('should get and show the correct userEmail', () => {
    spyOn(testMsalService, 'isLoggedIn').and.returnValue(true);
    spyOn(testMsalService, 'getUserFirstName').and.returnValue("John");
    spyOn(testMsalService, 'getUserFamilyName').and.returnValue("Dev");
    spyOn(testMsalService, 'getUserEmail').and.returnValue("john.dev@hotmail.com");
    fixture.detectChanges();
    const htmlElement : HTMLElement = fixture.nativeElement;
    const text = htmlElement.querySelectorAll('.info');
    fixture.detectChanges();
    expect(text[2].innerHTML).toBe("john.dev@hotmail.com");
  });
  
});
