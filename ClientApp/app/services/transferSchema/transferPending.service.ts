import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { BaseService } from '../base/base.service';
import * as moment from 'moment';

import 'rxjs/add/operator/toPromise';
import { Transfer } from '../../models/transfer/transfer.model';

@Injectable()
export class TransferPendingService extends BaseService<Transfer> {
    protected url = 'http://localhost:10503/api/TransferPending';

    getTransferPendingList(): Promise<Transfer[]> {
        return this.getList().then(tl => tl.map(t => {
            t.transferDate = moment.utc(t.transferDate).local().format('YYYY-MM-DDTHH:mm:ss');
            return t;
        }
        ));
    }

    getTransferPending(transferSchemaId: number): Promise<Transfer> {
        return this.get(transferSchemaId).then(transfer => {
            if (transfer) {
                transfer.transferDate = moment.utc(transfer.transferDate).local().format('YYYY-MM-DDTHH:mm:ss');
                return transfer;
            }
        })
            .catch(this.handleError);
    }

    postTransferPending(transfer: Transfer): Promise<number> {
        transfer.transferDate = moment(transfer.transferDate).toISOString();
        return this.post(transfer);
    }

    //deleteTransferSchema(transferSchemaId: number): Promise<boolean> {
    //    return this.delete(transferSchemaId);
    //}
}