import { Component, OnInit } from '@angular/core';
import { ProductenService } from '../services/producten.service';
import { ActivatedRoute } from '@angular/router';
import {Location} from '@angular/common';
import { MsalService } from '../services/msal.service';
import { WinkelmandService } from '../services/winkelmand.service';
import { IProduct } from '../Interfaces/IProduct';

@Component({
  selector: 'app-product-info',
  templateUrl: './product-info.component.html',
  styleUrls: ['./product-info.component.scss']
})
export class ProductInfoComponent implements OnInit {

  constructor(private productService: ProductenService, private route: ActivatedRoute,
              private location: Location, private msalService: MsalService,
              private winkelmandService: WinkelmandService) { }
  currentRoute: string;
  productId: number;
  UserId: string;
  product;

  ngOnInit() {
    this.route.params.subscribe(routeParams => {
      window.scrollTo(0, 0);
      this.currentRoute = routeParams.currentRoute;
      this.productId = +routeParams.productId
      if(this.currentRoute == "Cursus")
        this.GetCursus();
      else if (this.currentRoute == "Traject")
        this.GetTraject()
    })
    if(this.msalService.isLoggedIn())
      this.GetUserId();

  }
  GetUserId() {
    this.UserId = this.msalService.getUserObjectId();
  }

  async GetCursus() {
    this.product = await this.productService.GetCursusById(this.productId);
  }

  async GetTraject() {
    this.product = await this.productService.GetTrajectById(this.productId);
  }

  AddToCart(product: IProduct) {
    if (!this.msalService.isAdmin()) {
      this.winkelmandService.AddToWinkelmand(this.UserId, product.categorie, product.id, 1).subscribe( res => {
        this.winkelmandService.ChangeAantal(res.producten.length.toString());
      });
    }
  }

  goBack() {
    this.location.back();
  }

  isAdmin() {
    return this.msalService.admin;
  }
}
