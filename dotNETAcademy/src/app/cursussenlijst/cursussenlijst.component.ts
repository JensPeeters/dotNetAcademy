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
  types: string[] = ["Aanbevolen",".NET","Web"];
  type: string = "Aanbevolen";
  currentType: string = "";
  sortItems: string[] = ["Aanbevolen","Titel","Type"];
  sortBy: string = "Aanbevolen";
  direction: string = "asc"

  ngOnInit() {
    this.GetCursussen();
  }

  async GetCursussen(){
    this.products = await this.productService.GetCursussen(this.currentType);
  }
  Sort(){
    this.productService.sortBy = this.sortBy;
    this.GetCursussen();
  }
  GetType(){
    if(this.type != "Aanbevolen")
      this.currentType = `type=${this.type}`;
    else
      this.currentType = "";
    this.GetCursussen();
  }
  ChangeDirection(){
    if (this.direction == "asc")
      this.direction = "desc";
    else
      this.direction = "asc";
    this.productService.direction = this.direction;
    this.GetCursussen();
  }
}
