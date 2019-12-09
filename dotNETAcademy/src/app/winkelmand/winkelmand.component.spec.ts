import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WinkelmandComponent } from './winkelmand.component';
import { HttpClientModule } from '@angular/common/http';
import { WinkelmandItemComponent } from './winkelmand-item/winkelmand-item.component';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule } from '@angular/forms';
import { MsalService } from '../services/msal.service';

describe('WinkelmandComponent', () => {
  let component: WinkelmandComponent;
  let fixture: ComponentFixture<WinkelmandComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [FormsModule, HttpClientModule, RouterTestingModule],
      declarations: [ WinkelmandComponent, WinkelmandItemComponent ],
      providers : [MsalService]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WinkelmandComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
