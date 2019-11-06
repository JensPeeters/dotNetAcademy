import { Component, OnInit } from '@angular/core';
import { WinkelmandService, IWinkelmand } from '../common/winkelmand.service';
import { MsalService } from '../services/msal.service';

@Component({
  selector: 'app-winkelmand',
  templateUrl: './winkelmand.component.html',
  styleUrls: ['./winkelmand.component.scss']
})
export class WinkelmandComponent implements OnInit {
  Totaalprijs : number = 0;
  Winkelmand : IWinkelmand;
  UserId : string;
  constructor(private winkelmandService: WinkelmandService,
    private msalService: MsalService) { }

  ngOnInit() {
    if(this.msalService.isLoggedIn()){
      this.GetUserObjectId();
    }
    this.GetWinkelmandUser();
  }
  GetUserObjectId(){
    this.UserId = this.msalService.getUserObjectId();
  }

  BerekenTotaalprijs(){
    this.Totaalprijs = 0;
     if(this.Winkelmand){
       for (let product of this.Winkelmand.producten){
         this.Totaalprijs += product.aantal * product.product.prijs;
       }
     }
  }
  Herbereken(event){
    this.BerekenTotaalprijs();
  }

  private GetWinkelmandUser() {
    this.winkelmandService.GetWinkelmand(this.UserId).subscribe(res => {
      this.Winkelmand = res;
      this.BerekenTotaalprijs();
    });
  }
}
