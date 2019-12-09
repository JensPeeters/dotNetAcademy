import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-producten-beheer',
  templateUrl: './producten-beheer.component.html',
  styleUrls: ['./producten-beheer.component.scss']
})
export class ProductenBeheerComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
  }

}
