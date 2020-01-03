import { Component, OnInit, Input } from '@angular/core';
import { IBestelling } from 'src/app/Interfaces/IBestelling';

@Component({
  selector: 'app-report-bestelling',
  templateUrl: './report-bestelling.component.html',
  styleUrls: ['./report-bestelling.component.scss']
})
export class ReportBestellingComponent implements OnInit {
  @Input() bestelling: IBestelling;

  constructor() { }

  ngOnInit() {
  }

}
