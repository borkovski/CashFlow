import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import * as moment from 'moment';

import 'rxjs/add/operator/toPromise';
import { Transfer } from '../../models/transfer/transfer.model';

@Injectable()
export class TransferService {
    private transferUrl = 'http://localhost:10503/api/Transfer';

    constructor(private http: Http) { }

    getTransferList(): Promise<Transfer[]> {
        return this.http.get(this.transferUrl).toPromise() 
            .then(response => response.json() as Transfer[])
            .catch(this.handleError);
    }

    getTransfer(transferId: number): Promise<Transfer> {
        return this.http.get(this.transferUrl+'/'+transferId).toPromise()
            .then(response => {
                var transfer = response.json() as Transfer;
                if (transfer) {
                    transfer.transferDate = moment.utc(transfer.transferDate).local().format('YYYY-MM-DD');
                    console.log(transfer.transferDate);
                }
                return transfer;
            })
            .catch(this.handleError);
    }

    postTransfer(transfer: Transfer): Promise<number> {
        transfer.transferDate = moment(transfer.transferDate).toISOString();
        return this.http.post(this.transferUrl, transfer).toPromise()
            .then(response => response.json() as number)
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }
}