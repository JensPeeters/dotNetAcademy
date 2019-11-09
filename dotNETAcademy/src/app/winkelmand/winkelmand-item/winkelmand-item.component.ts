import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { WinkelmandService } from 'src/app/services/winkelmand.service';
import { MsalService } from 'src/app/services/msal.service';

@Component({
  selector: 'app-winkelmand-item',
  templateUrl: './winkelmand-item.component.html',
  styleUrls: ['./winkelmand-item.component.scss']
})
export class WinkelmandItemComponent implements OnInit {
  @Input() product;
  @Output() childEvent = new EventEmitter();
  UserId: string;

  constructor(private winkelmandService: WinkelmandService,
    private msalService: MsalService) { }

  ngOnInit() {
    if (this.msalService.isLoggedIn()) {
      this.GetUserObjectId();
    }
  }
  GetUserObjectId() {
    this.UserId = this.msalService.getUserObjectId();
  }

  Herbereken() {
    this.childEvent.emit();
  }
  VoegToe() {
    this.product.aantal++;
    this.Herbereken();
  }
  VerwijderProduct() {
    this.winkelmandService.DeleteFromWinkelmand(this.UserId, this.product.id).subscribe( winkelmand => {
        this.HerlaadWinkelmand();
    });
  }

  HerlaadWinkelmand(){
    this.childEvent.emit();
  }

  NeemAf() {
    if (this.product.aantal > 0) {
      this.product.aantal--;
      this.Herbereken();
    }
  }

}
