import { Component, OnInit } from '@angular/core';
import { ProductenService, Cursus } from '../common/producten.service';

@Component({
  selector: 'app-cursussenlijst',
  templateUrl: './cursussenlijst.component.html',
  styleUrls: ['./cursussenlijst.component.scss']
})
export class CursussenlijstComponent implements OnInit {

  constructor(private productService: ProductenService) { }
  products: Cursus[] = [];

  ngOnInit() {
    this.GetCursussen();
  }

  async GetCursussen(){
    this.products = await this.productService.GetCursussen();
  }
}
