import { Component, OnInit } from '@angular/core';
import { BestellingenService } from '../services/bestellingen.service';
import { MsalService } from '../services/msal.service';
import { IBestelling } from '../Interfaces/IBestelling';

@Component({
  selector: 'app-bestellingen',
  templateUrl: './bestellingen.component.html',
  styleUrls: ['./bestellingen.component.scss']
})
export class BestellingenComponent implements OnInit {

  Bestellingen: IBestelling[] = [];
  UserId: string;
  constructor(private bestService: BestellingenService, private msalService: MsalService) { }

  ngOnInit() {
    if (this.msalService.isLoggedIn()) {
      this.GetUserObjectId();
    }
    this.bestService.GetBestellingen(this.UserId).subscribe( res => {
      this.Bestellingen = res;
    });
  }
  GetUserObjectId() {
    this.UserId = this.msalService.getUserObjectId();
  }
}
