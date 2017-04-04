import { Component, ViewChild, ElementRef, OnInit, Input } from '@angular/core';
import { Point } from '../../models/chart/point.model';
import { AccountBalanceHistory } from '../../models/account/accountBalanceHistory.model';
import * as moment from 'moment';

@Component({
    selector: 'account-history-chart',
    template: require('./accountHistoryChart.component.html')
})
export class AccountHistoryChartComponent {
    @Input() balancesPerDay: AccountBalanceHistory[];
    private canvasWidth: number = 400;
    private canvasHeight: number = 400;
    private ctx: any;
    private maxBalance(): number {
        var maxB = Math.max(...this.balancesPerDay.map(a => a.balance)) * 1.1;
        return maxB > 0 ? maxB : 0;
    }
    private minBalance(): number {
        var minB = Math.min(...this.balancesPerDay.map(a => a.balance)) * 1.1;
        return minB > 0 ? 0 : minB;
    }

    constructor() {
    }

    ngAfterViewInit() {
        var canvas: HTMLCanvasElement = document.getElementById('myCanvas') as HTMLCanvasElement;
        this.ctx = canvas.getContext('2d');
        this.ctx.clearRect(0, 0, this.canvasWidth, this.canvasHeight);

        if (this.balancesPerDay.length) {
            this.drawZeroLines();
            var points: Point[] = this.transformToPoint();
            if (points.length > 1) {
                this.ctx.strokeStyle = 'rgba(220,220,220,1)';
                for (let i = 1; i < points.length; i++) {
                    if (points[i].y != this.getZeroLineY()) {
                        this.ctx.beginPath();
                        this.ctx.moveTo(points[i].x, points[i].y);
                        this.ctx.lineTo(points[i].x, this.canvasHeight);
                        this.ctx.stroke();
                    }
                }

                this.ctx.strokeStyle = 'black';
                for (let i = 1; i < points.length; i++) {
                    this.ctx.beginPath();
                    this.ctx.moveTo(points[i - 1].x, points[i - 1].y);
                    this.ctx.lineTo(points[i].x, points[i].y);
                    this.ctx.stroke();
                }

                this.ctx.fillStyle = "black";
                this.ctx.font = "bold 10px Arial";
                for (let i = 0; i < points.length; i++) {
                    this.ctx.fillText(this.balancesPerDay[i].balance.toFixed(0), points[i].x, points[i].y + 12);
                    this.ctx.fillText(moment(this.balancesPerDay[i].balanceDate).format('YYYY-MM-DD'), points[i].x, this.canvasHeight - 12);
                }
            }
        }

    }

    drawZeroLines() {
        var zeroLine: number = this.getZeroLineY();
        this.ctx.beginPath();
        this.ctx.moveTo(0, zeroLine);
        this.ctx.lineTo(this.canvasWidth, zeroLine);
        this.ctx.moveTo(0, 0);
        this.ctx.lineTo(0, this.canvasHeight);
        this.ctx.setLineDash([5]);
        this.ctx.stroke();
        this.ctx.setLineDash([]);
    }

    getZeroLineY() {
        if (this.maxBalance() < 0) {
            return 0;
        }
        else if (this.minBalance() > 0) {
            return this.canvasHeight;
        }
        else {
            return this.maxBalance() / (this.maxBalance() - this.minBalance()) * this.canvasHeight;
        }
    }

    transformToPoint(): Point[] {
        var points: Point[] = [];
        for (let i = 0; i < this.balancesPerDay.length; i++) {
            var y: number = this.getY(this.balancesPerDay[i].balance);
            var point: Point = new Point(this.canvasWidth / this.balancesPerDay.length * i, y);
            points.push(point);
        }
        return points;
    }

    getY(balanceValue: number) {
        return (1 - (balanceValue - this.minBalance()) / (this.maxBalance() - this.minBalance())) * this.canvasHeight;
    }

}