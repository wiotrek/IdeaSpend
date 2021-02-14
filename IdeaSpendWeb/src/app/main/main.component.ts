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

}
