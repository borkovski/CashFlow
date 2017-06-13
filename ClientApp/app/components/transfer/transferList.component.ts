import { Component, OnInit } from '@angular/core';
import { Transfer } from '../../models/transfer/transfer.model';
import { TransferService } from '../../services/transfer/transfer.service';
import { GridDefinition } from '../../models/grid/gridDefinition.model';
import { ColumnDefinition } from '../../models/grid/columnDefinition.model';
import { KeyValuePair } from '../../models/grid/dataFilter.model';

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
            new ColumnDefinition('id', 'Id', 'number', true),
            new ColumnDefinition('title', 'Title', 'string', true),
            new ColumnDefinition('accountFrom', 'Account from', 'dictionary', true),
            new ColumnDefinition('accountTo', 'Account to', 'dictionary', true),
            new ColumnDefinition('amount', 'Amount', 'number', true),
            new ColumnDefinition('transferDate', 'Date', 'datetime-local', true),
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
        var keyValuePair = new KeyValuePair();
        keyValuePair.Key = "accountFromId";
        keyValuePair.Value = "1";
        this.gridDefinition.dataFilter.filterProperties.push(keyValuePair);
        this.transferService.getTransferList(this.gridDefinition.dataFilter)
            .then(pagedList => {
                this.transfers = pagedList.items;
                this.isLoading = false;
                this.gridDefinition.data = this.transfers;
                this.gridDefinition.totalCount = pagedList.totalCount;
            });
    }

    reloadSort(columnDefinition: ColumnDefinition) {
        if (columnDefinition.sortingEnabled) {
            if (this.gridDefinition.dataFilter.sortPropertyName == columnDefinition.dataKey) {
                this.gridDefinition.dataFilter.isDescending = !this.gridDefinition.dataFilter.isDescending;
            }
            this.gridDefinition.dataFilter.sortPropertyName = columnDefinition.dataKey;
            this.loadData();
        }
    }

    reloadFilter(dataKey: string, newValue) {
        if (this.gridDefinition.dataFilter.filterProperties.find(p => p.Key == dataKey)) {
            this.gridDefinition.dataFilter.filterProperties.find(p => p.Key == dataKey).Value = newValue;
        }
        else {
            var keyValuePair: KeyValuePair = new KeyValuePair();
            keyValuePair.Key = dataKey;
            keyValuePair.Value = newValue;
            this.gridDefinition.dataFilter.filterProperties.push(keyValuePair);
        }
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
