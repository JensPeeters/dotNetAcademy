import { Component, OnInit } from '@angular/core';
import { ProductenService, Traject, Cursus } from '../common/producten.service';

@Component({
  selector: 'app-trajectenlijst',
  templateUrl: './trajectenlijst.component.html',
  styleUrls: ['./trajectenlijst.component.scss']
})
export class TrajectenlijstComponent implements OnInit {

  constructor(private productService: ProductenService) { }
  
  products: Traject[] = [];

  ngOnInit() {
    this.GetCursussen();
  }

  async GetCursussen(){
    this.products = await this.productService.GetTrajecten();
  }

}
