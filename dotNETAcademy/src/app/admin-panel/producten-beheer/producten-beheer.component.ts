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

  productAdd: IProduct = {
    cursussen: null,
    id: null,
    prijs: null,
    categorie: null,
    isBuyable: true,
    fotoURLCard: null,
    type: null,
    beschrijving: null,
    langeBeschrijving: null,
    titel: null,
  }
  RadioSelected: string = "Cursus";
  CursusTitel: string = "";
  VisibleProducten: IProduct[] = [];
  Producten: IProduct[] = [];
  Cursussen: IProduct[] = [];
  ToeTeVoegenCursussen: IProduct[] = [];

  LangeBeschrijving: string;

  constructor(private router: Router, private productenService: ProductenService) { }

  async ngOnInit() {
    await this.GetProducten();
  }
  VerwijderVanLijst(verwijderdeCursus: IProduct) {
    this.ToeTeVoegenCursussen.map(cursus => {
      if (cursus.titel == verwijderdeCursus.titel) {
        this.ToeTeVoegenCursussen = this.ToeTeVoegenCursussen.filter(obj => obj != verwijderdeCursus);
        this.Cursussen.push(verwijderdeCursus);
      }
    });
  }

  AddProductToDb() {
    this.productAdd.categorie = this.RadioSelected;
    if(this.productAdd.categorie == "Traject"){
      console.log(this.ToeTeVoegenCursussen);
      this.productAdd.cursussen = this.ToeTeVoegenCursussen;
    }
    console.log(this.productAdd);
    this.productenService.AddProduct(this.productAdd).subscribe(res =>{
      console.log("Succes");
      console.log(res);
    })
  }

  VoegCursusToe() {
    this.Cursussen.map(cursus => {
      if (cursus.titel == this.CursusTitel) {
        this.ToeTeVoegenCursussen.push(cursus);
        this.Cursussen = this.Cursussen.filter(obj => obj != cursus);
        this.CursusTitel = "";
      }
    });
    console.log(this.ToeTeVoegenCursussen);
  }

  DeleteProduct(product: IProduct) {
    this.productenService.DeleteProduct(product).subscribe(res => {
      this.GetProducten();
    });
  }

  async GetProducten() {
    this.Producten = [];
    this.Cursussen = await this.productenService.GetCursussenAdmin();
    this.Producten = this.Cursussen;
    this.Producten = this.Producten.concat(await this.productenService.GetTrajectenAdmin());
  }
}
