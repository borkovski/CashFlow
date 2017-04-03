import { Component, ViewChild, ElementRef, OnChanges, Input } from '@angular/core';
import { Transfer } from '../../models/transfer/transfer.model';
import { Point } from '../../models/chart/point.model';

@Component({
    selector: 'account-history-chart',
    template: require('./accountHistoryChart.component.html')
})
export class AccountHistoryChartComponent implements OnChanges {
    @ViewChild('chartCanvas') canvasRef: ElementRef;
    @Input() balancesPerDay: number[];
    canvasWidth: number = 400;
    canvasHeight: number = 400;

    constructor() {
    }

    transformToPoint(maxBalance: number): Point[] {
        var points: Point[] = [];
        for (let i = 0; i < this.balancesPerDay.length; i++) {
            var point: Point = new Point(this.canvasWidth / this.balancesPerDay.length * i, (1 - this.balancesPerDay[i] / maxBalance) * this.canvasHeight);
            points.push(point);
        }
        return points;
    }

    ngOnChanges() {
        var canvas = <HTMLCanvasElement>document.getElementById('myCanvas');
        var ctx = canvas.getContext('2d');
        ctx.clearRect(0, 0, this.canvasWidth, this.canvasHeight);
        if (this.balancesPerDay.length) {
            var maxBalance: number = Math.max(...this.balancesPerDay);
            var minBalance: number = Math.min(...this.balancesPerDay);
            var minPointHeight: number = minBalance < 0 ? minBalance * 1.1 : 0;
            var maxPointHeight: number = maxBalance > 0 ? maxBalance * 1.1 : 0;
            var points: Point[] = this.transformToPoint(maxBalance);
            if (points.length > 1) {
                ctx.strokeStyle = 'black';
                ctx.lineWidth = 20;
                ctx.beginPath();
                ctx.moveTo(points[0].x, points[0].y);
                console.log(points);
                for (let i = 1; i < points.length; i++) {
                    console.log(points[i]);
                    ctx.lineTo(points[i].x, points[i].y);
                }
                ctx.closePath();
                ctx.stroke();
            }
        }
    }
}
