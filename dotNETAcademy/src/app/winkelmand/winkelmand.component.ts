import { Component, OnInit } from '@angular/core';
import { WinkelmandService } from '../services/winkelmand.service';
import { MsalService } from '../services/msal.service';
import { IWinkelmand } from '../Interfaces/IWinkelmand';

@Component({
  selector: 'app-winkelmand',
  templateUrl: './winkelmand.component.html',
  styleUrls: ['./winkelmand.component.scss']
})
export class WinkelmandComponent implements OnInit {
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
    let Totaalprijs = 0;
     if(this.Winkelmand){
       for (let product of this.Winkelmand.producten){
         Totaalprijs += product.aantal * product.product.prijs;
       }
       this.Winkelmand.totaalPrijs = Totaalprijs;
     }
  }
  HerlaadWinkelmand(event){
    this.GetWinkelmandUser();
  }
  Herbereken(event){
    this.BerekenTotaalprijs();
  }

  private GetWinkelmandUser() {
    this.winkelmandService.GetWinkelmand(this.UserId).subscribe(res => {
      this.Winkelmand = res;
      this.winkelmandService.ChangeAantal(res.producten.length.toString());
      this.BerekenTotaalprijs();
    });
  }
}
