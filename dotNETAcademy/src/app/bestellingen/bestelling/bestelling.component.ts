import { Component, OnInit, Input } from '@angular/core';
import { IBestelling } from 'src/app/Interfaces/IBestelling';

@Component({
  selector: 'app-bestelling',
  templateUrl: './bestelling.component.html',
  styleUrls: ['./bestelling.component.scss']
})
export class BestellingComponent implements OnInit {
  @Input() bestelling: IBestelling;
  
  constructor() { }

  ngOnInit() {
  }

}
