import { Component, OnInit, OnDestroy } from '@angular/core';
import { ProductenService } from '../services/producten.service';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { ICursus } from '../Interfaces/ICursus';
import { ITraject } from '../Interfaces/ITraject';
import { IProduct } from '../Interfaces/IProduct';

@Component({
  selector: 'app-productenlijst',
  templateUrl: './productenlijst.component.html',
  styleUrls: ['./productenlijst.component.scss']
})
export class ProductenlijstComponent implements OnInit, OnDestroy {

  constructor(private productService: ProductenService, private route: ActivatedRoute) { }
  
  currentRoute: string;
  cursussen: IProduct[] = [];
  trajecten: IProduct[] = [];
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
        var tempTrajectTypes = [];
        this.productService.GetCursusTypes().subscribe(res => {
          tempCursusTypes = res;
          this.productService.GetTrajectTypes().subscribe(res => {
            tempTrajectTypes = res;
            this.productFilter.types = tempCursusTypes.concat(tempTrajectTypes);
          });
        });
      }
      this.GetProducts();
    });
  }
  ngOnDestroy(){
    if(this.subscription)
      this.subscription.unsubscribe();
  }

  async GetProducts(){
    if(this.currentRoute == "cursussen"){
      this.GetBuyableCursussen();
    }
    else if (this.currentRoute == "trajecten"){
      this.GetBuyableTrajecten();
    }
    else if (this.currentRoute == "zoekresultaten"){
      this.GetBuyableCursussen();
      this.GetBuyableTrajecten();
    }
  }

  async GetBuyableCursussen(){
    this.cursussen = await this.productService.GetBuyableCursussen(`${this.productFilter.currentType}&titel=${this.productFilter.searchParam}`);
  }

  async GetBuyableTrajecten(){
    this.trajecten = await this.productService.GetBuyableTrajecten(`${this.productFilter.currentType}&titel=${this.productFilter.searchParam}`);
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
  ChangeBoolCursussen(){
    this.collapsedCursussen = !this.collapsedCursussen;
  }
  ChangeBoolTrajecten(){
    this.collapsedTrajecten = !this.collapsedTrajecten;
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