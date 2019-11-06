import { Component, OnInit } from '@angular/core';
import { ProductenService } from '../services/producten.service';
import { ActivatedRoute } from '@angular/router';
import {Location} from '@angular/common';

@Component({
  selector: 'app-product-info',
  templateUrl: './product-info.component.html',
  styleUrls: ['./product-info.component.scss']
})
export class ProductInfoComponent implements OnInit {

  constructor(private productService: ProductenService, private route: ActivatedRoute, private location: Location) { }
  currentRoute: string;
  productId: number;
  product;

  ngOnInit() {
    this.route.params.subscribe(routeParams =>{
      window.scrollTo(0, 0);
      this.currentRoute = routeParams.currentRoute;
      this.productId = +routeParams.productId
      if(this.currentRoute == "Cursus")
        this.GetCursus();
      else if (this.currentRoute == "Traject")
        this.GetTraject()
    })
  }

  async GetCursus(){
    this.product = await this.productService.GetCursusById(this.productId);
  }

  async GetTraject(){
    this.product = await this.productService.GetTrajectById(this.productId);
  }

  goBack(){
    this.location.back();
  }
}
