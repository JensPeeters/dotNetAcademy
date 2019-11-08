import { Component, OnInit, Input } from '@angular/core';
import { IProduct, WinkelmandService } from 'src/app/services/winkelmand.service';
import { MsalService } from 'src/app/services/msal.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {
  @Input() product;
  UserId : string;

  constructor(private winkelmandService : WinkelmandService,
    private msalServ : MsalService) {
     }

  ngOnInit() {
    this.GetUserId();
  }
  GetUserId(){
    this.UserId = this.msalServ.getUserObjectId();
  }
  AddToCart(product : IProduct){
    this.winkelmandService.AddToWinkelmand(this.UserId,product.categorie,product.id,1).subscribe( res => {
      this.winkelmandService.ChangeAantal(res.producten.length.toString());
    });
  }

}
