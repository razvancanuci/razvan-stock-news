import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { BarData } from 'src/app/subscribe/models/models';

@Component({
  selector: 'app-bar-chart',
  templateUrl: './bar-chart.component.html',
  styleUrls: ['./bar-chart.component.css']
})
export class BarChartComponent implements OnInit, OnChanges {

  @Input()
  barData!: BarData[];

  chartData!: any;
  options!: any;

  constructor() { }

  ngOnInit(): void {
    this.assignChartData();
  }

  ngOnChanges(): void {
    this.assignChartData();
  }

  assignChartData() {
    this.chartData = {
      labels: this.barData.map((bar) => bar.date),
      datasets: [
        {
          data: this.barData.map((bar) => bar.value),
          borderColor: "#FFFFFF",
          backgroundColor: "#FFFFFF"
        },
      ]
    }
    this.options = {
      plugins: {
        title: {
          display: true,
          text: 'SUBSCRIBED LAST 7',
          fontSize: 24,
          color: '#FFFFFF'
        },
        legend: {
          position: 'bottom',
          display: false
        },
      },
      scales: {
        x: {
          ticks: {
            color: '#FFFFFF'
          }
        },
        y: {
          ticks: {
            color: '#FFFFFF'
          }
        }
      }
    };
  }

}
