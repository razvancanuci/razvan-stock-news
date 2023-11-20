import { Component, Input, OnChanges, OnInit } from '@angular/core';

@Component({
  selector: 'app-pie-chart',
  templateUrl: './pie-chart.component.html',
  styleUrls: ['./pie-chart.component.css']
})
export class PieChartComponent implements OnInit, OnChanges {

  @Input()
  percentage!: number;

  chartData!: any;
  options!: any;

  constructor() { }

  ngOnInit(): void {
    this.assignChartData();
  }

  ngOnChanges() {
    this.assignChartData();
  }

  assignChartData() {
    this.chartData = {
      labels: ["With phone", "Without phone"],
      datasets: [
        {
          label: 'Percentage (%):',
          data: [this.percentage, 100.0 - this.percentage]
        },
      ]
    }
    this.options = {
      plugins: {
        title: {
          display: true,
          text: 'SUBSCRIBERS WITH PHONE NUMBER',
          fontSize: 24,
          color: '#FFFFFF'
        },
        legend: {
          position: 'bottom',
          labels: {
            color: '#FFFFFF'
          }
        }
      }
    };
  }
}
