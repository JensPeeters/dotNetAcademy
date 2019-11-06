import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-winkelmand-item',
  templateUrl: './winkelmand-item.component.html',
  styleUrls: ['./winkelmand-item.component.scss']
})
export class WinkelmandItemComponent implements OnInit {
  @Input() product;
  @Output() childEvent = new EventEmitter();
  constructor() { }

  ngOnInit() {
  }
  Herbereken() {
    this.childEvent.emit();
  }
  VoegToe(aantal: number) {
    this.product.aantal++;
    this.Herbereken();
  }
  NeemAf(aantal: number) {
    if (this.product.aantal > 0) {
      this.product.aantal--;
      this.Herbereken();
    }
  }

}
