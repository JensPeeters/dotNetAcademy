import { Component, OnInit, Input } from '@angular/core';
import { Cursus, Traject } from '../common/producten.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {
  @Input() product;
  constructor() { }

  ngOnInit() {
  }

}
