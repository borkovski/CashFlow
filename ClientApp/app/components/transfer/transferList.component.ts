import { Component, OnInit } from '@angular/core';
import { Transfer } from '../../models/transfer/transfer.model';
import { TransferService } from '../../services/transfer/transfer.service';

@Component({
    selector: 'transferList',
    template: require('./transferList.component.html')
})
export class TransferListComponent implements OnInit {
    isLoading: boolean;
    transfers: Transfer[] = [];

    constructor(private transferService: TransferService) {
    }

    ngOnInit(): void {
        this.isLoading = true;
        this.transferService.getTransferList()
            .then(transfers => {
                this.transfers = transfers;
                this.isLoading = false;
            });
    }
}
