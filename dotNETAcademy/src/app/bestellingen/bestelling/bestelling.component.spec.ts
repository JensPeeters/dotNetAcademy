import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BestellingComponent } from './bestelling.component';
import { RouterModule } from '@angular/router';

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
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
