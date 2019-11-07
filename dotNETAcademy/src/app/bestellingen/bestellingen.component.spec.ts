import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BestellingenComponent } from './bestellingen.component';
import { BestellingComponent } from './bestelling/bestelling.component';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { MsalService } from '../services/msal.service';

describe('BestellingenComponent', () => {
  let component: BestellingenComponent;
  let fixture: ComponentFixture<BestellingenComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BestellingenComponent, BestellingComponent ],
      imports: [RouterModule.forRoot([]), HttpClientModule],
      providers: [MsalService]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BestellingenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
