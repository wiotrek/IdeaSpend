import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-transactions-overview',
  templateUrl: './transactions-overview.component.html',
  styleUrls: ['./transactions-overview.component.css']
})
export class TransactionsOverviewComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  //function to return list of numbers from 0 to n-1 
  numSequence(n: number): Array<number> { 
    return Array(n); 
  } 
  
}
