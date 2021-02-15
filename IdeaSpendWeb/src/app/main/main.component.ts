import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

  // TODO: change font family for information inside square

  greenTriangleImage = '../assets/main/green-triangle.png';
  redTriangleImage = '../assets/main/red-triangle.png';

  constructor() { }

  ngOnInit(): void {
  }

  public months: Array<string> = [
    "styczen", "luty", "marzec", "kwiecien", "maj",
    "czerwiec", "lipiec", "sierpien", "wrzesien", "pazdziernik",
    "listopad", "grudzien"
  ];

  public years: Array<number> = [
    2020, 2019, 2018
  ];

  public currency: Array<string> = [
    "PLN", "USD", "EUR"
  ];

}
