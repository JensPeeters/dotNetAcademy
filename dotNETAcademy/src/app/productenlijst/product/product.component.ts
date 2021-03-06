import { Component, OnInit, Input } from '@angular/core';
import { WinkelmandService } from 'src/app/services/winkelmand.service';
import { MsalService } from 'src/app/services/msal.service';
import { IProduct } from 'src/app/Interfaces/IProduct';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {
  @Input() product;
  UserId: string;

  constructor(private winkelmandService: WinkelmandService,
              private msalServ: MsalService) {
     }

  ngOnInit() {
    if (this.msalServ.isLoggedIn()) {
      this.GetUserId();
    }
  }
  GetUserId() {
    this.UserId = this.msalServ.getUserObjectId();
  }
  AddToCart(product: IProduct) {
    if (!this.msalServ.isAdmin()) {
      this.winkelmandService.AddToWinkelmand(this.UserId, product.categorie, product.id, 1).subscribe( res => {
        this.winkelmandService.ChangeAantal(res.producten.length.toString());
      });
    }
  }

  isAdmin() {
    return this.msalServ.admin;
  }

}
