import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { BaseService } from '../base/base.service';
import * as moment from 'moment';

import 'rxjs/add/operator/toPromise';
import { Transfer } from '../../models/transfer/transfer.model';

@Injectable()
export class TransferService extends BaseService<Transfer> {
    protected url = 'http://localhost:10503/api/Transfer';
    
    getTransferList(): Promise<Transfer[]> {
        return this.getList().then(transfers => {
            transfers.map((transfer) => {
                transfer.transferDate = moment.utc(transfer.transferDate).local().format('YYYY-MM-DD HH:mm:ss');
            });
            return transfers;
        });
    }

    getTransfer(transferId: number): Promise<Transfer> {
        return this.get(transferId).then(transfer => {
            if (transfer) {
                transfer.transferDate = moment.utc(transfer.transferDate).local().format('YYYY-MM-DDTHH:mm:ss');
                return transfer;
            }
        })
            .catch(this.handleError);
    }

    postTransfer(transfer: Transfer): Promise<number> {
        transfer.transferDate = moment(transfer.transferDate).toISOString();
        return this.post(transfer);
    }

    deleteTransfer(transferId: number): Promise<boolean> {
        return this.delete(transferId);
    }
}