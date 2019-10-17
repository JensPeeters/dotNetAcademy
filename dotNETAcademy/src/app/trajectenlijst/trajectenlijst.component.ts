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
  types: string[] = ["Aanbevolen","Full Stack","Visual Studio","Angular"];
  type: string = "Aanbevolen";
  currentType: string = "";
  sortItems: string[] = ["Aanbevolen","Titel","Type"];
  sortBy: string = "Aanbevolen";
  direction: string = "asc"

  ngOnInit() {
    this.GetTrajecten();
  }

  async GetTrajecten(){
    this.products = await this.productService.GetTrajecten(this.currentType);
  }
  Sort(){
    this.productService.sortBy = this.sortBy;
    this.GetTrajecten();
  }
  GetType(){
    if(this.type != "Aanbevolen")
      this.currentType = `type=${this.type}`;
    else
      this.currentType = "";
    this.GetTrajecten();
  }
  ChangeDirection(){
    if (this.direction == "asc")
      this.direction = "desc";
    else
      this.direction = "asc";
    this.productService.direction = this.direction;
    this.GetTrajecten();
  }
}
