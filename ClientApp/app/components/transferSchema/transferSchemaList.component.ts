import { Component, OnInit } from '@angular/core';
import { TransferSchema } from '../../models/transferSchema/transferSchema.model';
import { TransferSchemaService } from '../../services/transferSchema/transferSchema.service';

@Component({
    selector: 'transferSchemaList',
    template: require('./transferSchemaList.component.html')
})
export class TransferSchemaListComponent implements OnInit {
    transferSchemas: TransferSchema[] = [];

    constructor(private transferSchemaService: TransferSchemaService) {
    }

    ngOnInit(): void {
        this.transferSchemaService.getTransferSchemaList()
            .then(transferSchemas => this.transferSchemas = transferSchemas);
    }
}
