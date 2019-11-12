import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WinkelmandItemComponent } from './winkelmand-item.component';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MsalService } from 'src/app/services/msal.service';

describe('WinkelmandItemComponent', () => {
  let component: WinkelmandItemComponent;
  let fixture: ComponentFixture<WinkelmandItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports : [ FormsModule, HttpClientModule, RouterTestingModule],
      declarations: [ WinkelmandItemComponent ],
      providers: [ MsalService ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WinkelmandItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
