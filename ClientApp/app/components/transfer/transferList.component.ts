import { Component, OnInit } from '@angular/core';
import { Transfer } from '../../models/transfer/transfer.model';
import { TransferService } from '../../services/transfer/transfer.service';
import { GridDefinition } from '../../models/grid/gridDefinition.model';
import { ColumnDefinition } from '../../models/grid/columnDefinition.model';

@Component({
    selector: 'transferList',
    template: require('./transferList.component.html')
})
export class TransferListComponent implements OnInit {
    isLoading: boolean;
    transfers: Transfer[] = [];
    gridDefinition: GridDefinition = new GridDefinition();

    constructor(private transferService: TransferService) {
        this.gridDefinition.columnDefinitions = [
            new ColumnDefinition('id', 'Id', null, false),
            new ColumnDefinition('title', 'Title', null, false),
            new ColumnDefinition('accountFrom', 'Account from', null, false),
            new ColumnDefinition('accountTo', 'Account to', null, false),
            new ColumnDefinition('amount', 'Amount', null, false),
            new ColumnDefinition('transferDate', 'Date', null, false),
        ];
        this.gridDefinition.dataFilter.take = 50;
        this.gridDefinition.dataFilter.skip = 0;
        this.gridDefinition.dataFilter.sortPropertyName = 'id';
        this.gridDefinition.dataFilter.isDescending = true;
    }

    ngOnInit(): void {
        this.loadData();
    }

    loadData() {
        this.isLoading = true;
        this.transferService.getTransferList(this.gridDefinition.dataFilter)
            .then(transfers => {
                this.transfers = transfers;
                this.isLoading = false;
                this.gridDefinition.data = this.transfers;
            });
    }

    reload(clickedHeader: string) {
        this.gridDefinition.dataFilter.sortPropertyName = clickedHeader;
        this.loadData();
    }
}
