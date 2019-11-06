import { Component, OnInit } from '@angular/core';
import { ProductenService, Traject, Cursus } from '../services/producten.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-productenlijst',
  templateUrl: './productenlijst.component.html',
  styleUrls: ['./productenlijst.component.scss']
})
export class ProductenlijstComponent implements OnInit {

  constructor(private productService: ProductenService, private route: ActivatedRoute) { }
  
  currentRoute: string;
  cursussen: Cursus[] = [];
  trajecten: Traject[] = [];
  types: string[] = [];
  type: string = "Aanbevolen";
  currentType: string = "";
  sortItems: string[] = ["Aanbevolen","Titel","Type"];
  sortBy: string = "Aanbevolen";
  direction: string = "asc"
  searchParam: string = "";
  collapsedCursussen: boolean = false;
  collapsedTrajecten: boolean = false;

  ngOnInit() {
    // this.route.paramMap.subscribe(params =>{
    //   this.currentRoute = params.get('currentRoute'); 
    // })
    this.route.params.subscribe(routeParams => {
      this.currentRoute = routeParams.currentRoute;
      this.cursussen = [];
      this.trajecten = [];
      if(this.currentRoute == "cursussen"){
        this.types = this.productService.cursusTypes;
      }
      else if (this.currentRoute == "trajecten"){
        this.types = this.productService.TrajectTypes;
      }
      else if (this.currentRoute == "zoekresultaten"){
        this.searchParam = routeParams.searchParam;
        this.types = this.productService.cursusTypes.concat(this.productService.TrajectTypes);
      }
      this.GetProducts();
    });
  }

  async GetProducts(){
    if(this.currentRoute == "cursussen"){
      this.GetCursussen();
    }
    else if (this.currentRoute == "trajecten"){
      this.GetTrajecten();
    }
    else if (this.currentRoute == "zoekresultaten"){
      this.GetCursussen();
      this.GetTrajecten();
    }
  }

  async GetCursussen(){
    this.cursussen = await this.productService.GetCursussen(`${this.currentType}&titel=${this.searchParam}`);
  }

  async GetTrajecten(){
    this.trajecten = await this.productService.GetTrajecten(`${this.currentType}&titel=${this.searchParam}`);
  }

  Sort(){
    this.productService.sortBy = this.sortBy;
    this.GetProducts();
  }

  GetType(){
    if(this.type != "Aanbevolen")
      this.currentType = `type=${this.type}`;
    else
      this.currentType = "";
    this.GetProducts();
  }

  ChangeDirection(){
    if (this.direction == "asc")
      this.direction = "desc";
    else
      this.direction = "asc";
    this.productService.direction = this.direction;
    this.GetProducts();
  }
  Changecollapsedcursussen(){
    this.collapsedCursussen != this.collapsedCursussen;
  }
  Changecollapsedctrajecten(){
    this.collapsedTrajecten != this.collapsedTrajecten;
  }
}
