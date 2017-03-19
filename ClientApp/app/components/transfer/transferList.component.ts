import { Component, OnInit } from '@angular/core';
import { Transfer } from '../../models/transfer/transfer.model';
import { TransferService } from '../../services/transfer/transfer.service';

@Component({
    selector: 'transferList',
    template: require('./transferList.component.html')
})
export class TransferListComponent implements OnInit {
    transfers: Transfer[] = [];

    constructor(private transferService: TransferService) {
    }

    ngOnInit(): void {
        this.transferService.getTransferList()
            .then(transfers => this.transfers = transfers);
    }
}
