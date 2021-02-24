/* tslint:disable */
import {Component, OnInit, ViewChild} from '@angular/core';
import * as ApexCharts from 'apexcharts';

import {
  ChartComponent,
  ApexAxisChartSeries,
  ApexChart,
  ApexXAxis,
  ApexTitleSubtitle, ApexDataLabels, ApexStroke, ApexTooltip, ApexLegend
} from 'ng-apexcharts';

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  stroke: ApexStroke;
  xaxis: ApexXAxis;
  tooltip: ApexTooltip;
  legend: ApexLegend
};

@Component({
  selector: 'app-statistics',
  templateUrl: './statistics.component.html',
  styleUrls: ['./statistics.component.css']
})
export class StatisticsComponent implements OnInit {

  @ViewChild('chart') chart: ChartComponent;
  public chartOptions: ChartOptions;

  constructor() {
    this.chartOptions = {
      series: [
        {
          name: 'Wydano',
          data: [10, 21, 32, 43, 16, 32.14, 0, 0, 67.18, 8.53,
            5, 7.28, 11.17, 27.34, 38.99, 0, 18.50, 18.50, 47.58, 6
            , 0, 121.43, 28.12, 7.99, 21.15, 19.37, 61.08, 9.99, 0, 21.05]
        }
      ],
      chart: {
        height: 350,
        type: 'area',
        foreColor: '#fff'
      },
      dataLabels: {
        enabled: false
      },
      stroke: {
        curve: 'smooth'
      },
      xaxis: {
        categories: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15',
          '16', '17', '18', '19', '20', '21', '22', '23', '24', '25', '26', '27', '28', '29', '30'
        ]
      },
      tooltip: {
        x: {
          format: 'dd/MM/yyyy HH:mm'
        }
      },
      legend: {
        show: false
      }
    };
    const chart = new ApexCharts(document.querySelector('#chart'), this.chartOptions);
    chart.render();
  }

  ngOnInit(): void {
  }

}
