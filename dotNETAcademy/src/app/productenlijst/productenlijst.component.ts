import { Component, OnInit, OnDestroy } from '@angular/core';
import { ProductenService } from '../services/producten.service';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { ICursus } from '../Interfaces/ICursus';
import { ITraject } from '../Interfaces/ITraject';

@Component({
  selector: 'app-productenlijst',
  templateUrl: './productenlijst.component.html',
  styleUrls: ['./productenlijst.component.scss']
})
export class ProductenlijstComponent implements OnInit, OnDestroy {

  constructor(private productService: ProductenService, private route: ActivatedRoute) { }
  
  currentRoute: string;
  cursussen: ICursus[] = [];
  trajecten: ITraject[] = [];
  productFilter: Filter = new Filter();

  collapsedCursussen: boolean = false;
  collapsedTrajecten: boolean = false;
  subscription: Subscription;

  ngOnInit() {
    // this.route.paramMap.subscribe(params =>{
    //   this.currentRoute = params.get('currentRoute'); 
    // })
    this.subscription = this.route.params
    .subscribe(routeParams => {
      this.currentRoute = routeParams.currentRoute;
      this.cursussen = [];
      this.trajecten = [];
      if(this.currentRoute == "cursussen"){
        this.productService.GetCursusTypes().subscribe(res =>{
          this.productFilter.types = res;
        });
      }
      else if (this.currentRoute == "trajecten"){
        this.productService.GetTrajectTypes().subscribe(res =>{
          this.productFilter.types = res;
        });
      }
      else if (this.currentRoute == "zoekresultaten"){
        this.productFilter.searchParam = routeParams.searchParam;
        var tempCursusTypes =[];
        this.productService.GetCursusTypes().subscribe(res =>{tempCursusTypes = res;});
        var tempTrajectTypes = [];
        this.productService.GetTrajectTypes().subscribe(res =>{tempTrajectTypes = res;});
        this.productFilter.types = tempCursusTypes.concat(tempTrajectTypes);
      }
      this.GetProducts();
    });
  }
  ngOnDestroy(){
    this.subscription.unsubscribe(); //pipe kan niet gebruikt (refrehed de objecten niet wanneer je van cursussen naar trajecten gaat)
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
    this.cursussen = await this.productService.GetCursussen(`${this.productFilter.currentType}&titel=${this.productFilter.searchParam}`);
  }

  async GetTrajecten(){
    this.trajecten = await this.productService.GetTrajecten(`${this.productFilter.currentType}&titel=${this.productFilter.searchParam}`);
  }

  Sort(){
    this.productService.apiPageFilter.sortBy = this.productFilter.sortBy;
    this.GetProducts();
  }

  GetType(){
    if(this.productFilter.type != "Aanbevolen")
      this.productFilter.currentType = `type=${this.productFilter.type}`;
    else
      this.productFilter.currentType = "";
    this.GetProducts();
  }

  ChangeDirection(){
    if (this.productFilter.direction == "asc")
      this.productFilter.direction = "desc";
    else
      this.productFilter.direction = "asc";
    this.productService.apiPageFilter.direction = this.productFilter.direction;
    this.GetProducts();
  }
}
export class Filter{
  types: string[];
  type: string = "Aanbevolen";
  currentType: string = "";

  sortItems: string[] = ["Aanbevolen","Titel","Type"];
  sortBy: string = "Aanbevolen";

  direction: string = "asc"
  searchParam: string = "";
} 