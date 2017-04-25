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
        this.gridDefinition.dataFilter.take = 10;
        this.gridDefinition.dataFilter.skip = 0;
        this.gridDefinition.dataFilter.sortPropertyName = 'id';
        this.gridDefinition.dataFilter.isDescending = true;
        this.gridDefinition.pageSizes.push(100);
    }

    ngOnInit(): void {
        this.loadData();
    }

    loadData() {
        this.isLoading = true;
        this.transferService.getTransferList(this.gridDefinition.dataFilter)
            .then(pagedList => {
                this.transfers = pagedList.items;
                this.isLoading = false;
                this.gridDefinition.data = this.transfers;
                this.gridDefinition.totalCount = pagedList.totalCount;
            });
    }

    reload(clickedHeader: string) {
        if (this.gridDefinition.dataFilter.sortPropertyName == clickedHeader) {
            this.gridDefinition.dataFilter.isDescending = !this.gridDefinition.dataFilter.isDescending;
        }
        this.gridDefinition.dataFilter.sortPropertyName = clickedHeader;
        this.loadData();
    }

    setPage(page: number) {
        if (page < 1
            || page > this.gridDefinition.getTotalPages()
            || page == this.gridDefinition.getCurrentPageNumber()) {
            return;
        }
        this.gridDefinition.setCurrentPageNumber(page);
        this.loadData();
    }

    setPageSize(pageSize: number) {
        this.gridDefinition.dataFilter.take = pageSize;
        this.gridDefinition.dataFilter.skip = 0;
        this.loadData();
    }
}
