import { Component, OnInit } from '@angular/core';
import { Transfer } from '../../models/transfer/transfer.model';
import { TransferPendingService } from '../../services/transferSchema/transferPending.service';

@Component({
    selector: 'transferPendingList',
    template: require('./transferPendingList.component.html')
})
export class TransferPendingListComponent implements OnInit {
    transfers: Transfer[] = [];

    constructor(private transferPendingService: TransferPendingService) {
    }

    ngOnInit(): void {
        this.transferPendingService.getTransferPendingList()
            .then(transfers => this.transfers = transfers);
    }
}
