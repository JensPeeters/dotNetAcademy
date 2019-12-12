import { Component, OnInit, Input } from '@angular/core';
import { IBestelling } from '../Interfaces/IBestelling';
import {Location} from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { BestellingenService } from '../services/bestellingen.service';

@Component({
  selector: 'app-bestelling-info',
  templateUrl: './bestelling-info.component.html',
  styleUrls: ['./bestelling-info.component.scss']
})
export class BestellingInfoComponent implements OnInit {

  constructor(private route: ActivatedRoute, private location: Location, private bestellingService: BestellingenService) { }

  bestellingId: number;
  bestelling: IBestelling;

  ngOnInit() {
    this.route.params.subscribe(routeParams =>{
      this.bestellingId = +routeParams.bestellingId;
      this.GetBestelling();
    })
  }

  GetBestelling(){
    this.bestellingService.GetBestellingById(this.bestellingId).subscribe(res =>{
      this.bestelling = res;
    });
  }

  goBack() {
    this.location.back();
  }

}
