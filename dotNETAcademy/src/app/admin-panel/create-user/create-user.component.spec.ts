import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateUserComponent } from './create-user.component';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { UserService } from 'src/app/services/user.service';
import { Observable, throwError } from 'rxjs';

describe('CreateUserComponent', () => {
  let component: CreateUserComponent;
  let fixture: ComponentFixture<CreateUserComponent>;
  let testUserservice: UserService = new UserService(null);

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ RouterTestingModule, HttpClientModule, FormsModule ],
      declarations: [ CreateUserComponent ],
      providers: [
        { provide: UserService, useValue: testUserservice }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateUserComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should inform admin that userId should be filled in if empty', () => {
    spyOn(testUserservice, 'saveKlantInDb').and.returnValue(Observable.of("test"));
    spyOn(testUserservice, 'saveAdminInDb').and.returnValue(Observable.of("test"));
    fixture.detectChanges();
    component.azureIdParam = "test";
    const htmlElement : HTMLElement = fixture.nativeElement;
    const buttons = htmlElement.querySelectorAll('button');
    buttons[0].click();
    expect(component.createUserSucces).toBe(true);
    expect(component.userAlreadyExists).toBe(false);
  });

  it('should inform admin that user is successfully created if created is successful', () => {
    spyOn(testUserservice, 'saveKlantInDb').and.returnValue(Observable.of("test"));
    spyOn(testUserservice, 'saveAdminInDb').and.returnValue(Observable.of("test"));
    fixture.detectChanges();
    component.azureIdParam = "test";
    component.userType = "klant";
    const htmlElement : HTMLElement = fixture.nativeElement;
    const buttons = htmlElement.querySelectorAll('button');
    buttons[0].click();
    expect(component.createUserSucces).toBe(true);
    expect(component.userAlreadyExists).toBe(false);
  });

  it('should inform admin that admin is successfully created if created is successful', () => {
    spyOn(testUserservice, 'saveKlantInDb').and.returnValue(Observable.of("test"));
    spyOn(testUserservice, 'saveAdminInDb').and.returnValue(Observable.of("test"));
    fixture.detectChanges();
    component.azureIdParam = "test";
    component.userType = "admin";
    const htmlElement : HTMLElement = fixture.nativeElement;
    const buttons = htmlElement.querySelectorAll('button');
    buttons[0].click();
    expect(component.createUserSucces).toBe(true);
    expect(component.userAlreadyExists).toBe(false);
  });

  it('should inform admin that user already exists when code 409 is returned (conflict)', () => {
    spyOn(testUserservice, 'saveAdminInDb').and.returnValue(Observable.of("test"));
    fixture.detectChanges();
    component.azureIdParam = "test";
    component.userType = "klant";
    const mockErrorResponse = { status: 409, statusText: 'Conflict' };
    spyOn(testUserservice , 'saveKlantInDb').and.callFake(() => {
      return throwError(mockErrorResponse);
    });
    const htmlElement : HTMLElement = fixture.nativeElement;
    const buttons = htmlElement.querySelectorAll('button');
    buttons[0].click();
    expect(component.createUserSucces).toBe(false);
    expect(component.userAlreadyExists).toBe(true);
  });

  it('should inform admin that admin already exists when code 409 is returned (conflict)', () => {
    spyOn(testUserservice, 'saveKlantInDb').and.returnValue(Observable.of("test"));
    fixture.detectChanges();
    component.azureIdParam = "test";
    component.userType = "admin";
    const mockErrorResponse = { status: 409, statusText: 'Conflict' };
    spyOn(testUserservice , 'saveAdminInDb').and.callFake(() => {
      return throwError(mockErrorResponse);
    });
    const htmlElement : HTMLElement = fixture.nativeElement;
    const buttons = htmlElement.querySelectorAll('button');
    buttons[0].click();
    expect(component.createUserSucces).toBe(false);
    expect(component.userAlreadyExists).toBe(true);
  });
  
});
