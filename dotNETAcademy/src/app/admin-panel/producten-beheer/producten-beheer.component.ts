import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProductenService } from 'src/app/services/producten.service';
import { IProduct } from 'src/app/Interfaces/IProduct';

@Component({
  selector: 'app-producten-beheer',
  templateUrl: './producten-beheer.component.html',
  styleUrls: ['./producten-beheer.component.scss']
})
export class ProductenBeheerComponent implements OnInit {

  VisibleProducten : IProduct[] = [];
  Producten : IProduct[] = [];

  constructor(private router: Router, private productenService : ProductenService) { }

  async ngOnInit() {
    await this.GetProducten();
  }

  DeleteProduct(product : IProduct){
    console.log(product);
    this.productenService.DeleteProduct(product).subscribe(res =>{
      console.log(res);
        this.GetProducten();
    });
  }

  async GetProducten(){
    this.Producten = [];
    this.Producten = await this.productenService.GetCursussen();
    this.Producten = this.Producten.concat(await this.productenService.GetTrajecten());
  }

}
