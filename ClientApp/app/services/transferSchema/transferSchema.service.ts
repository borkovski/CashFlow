import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { BaseService } from '../base/base.service';
import * as moment from 'moment';

import 'rxjs/add/operator/toPromise';
import { TransferSchema } from '../../models/transferSchema/transferSchema.model';

@Injectable()
export class TransferSchemaService extends BaseService<TransferSchema> {
    protected url = 'http://localhost:10503/api/TransferSchema';

    getTransferSchemaList(): Promise<TransferSchema[]> {
        return this.getList();
    }

    getTransferSchema(transferSchemaId: number): Promise<TransferSchema> {
        return this.get(transferSchemaId).then(transferSchema => {
            if (transferSchema) {
                transferSchema.transferStartDate = moment.utc(transferSchema.transferStartDate).local().format('YYYY-MM-DDTHH:mm:ss');
                transferSchema.transferEndDate = moment.utc(transferSchema.transferEndDate).local().format('YYYY-MM-DDTHH:mm:ss');
                transferSchema.lastTransferDate = moment.utc(transferSchema.lastTransferDate).local().format('YYYY-MM-DDTHH:mm:ss');
                return transferSchema;
            }
        })
            .catch(this.handleError);
    }

    postTransferSchema(transferSchema: TransferSchema): Promise<number> {
        transferSchema.transferStartDate = moment(transferSchema.transferStartDate).toISOString();
        transferSchema.transferEndDate = moment(transferSchema.transferEndDate).toISOString();
        transferSchema.lastTransferDate = moment(transferSchema.lastTransferDate).toISOString();
        return this.post(transferSchema);
    }

    deleteTransferSchema(transferSchemaId: number): Promise<boolean> {
        return this.delete(transferSchemaId);
    }
}