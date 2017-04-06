import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { BaseService } from '../base/base.service';
import { TransferPeriod } from '../../models/transferSchema/transferPeriod.model';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class TransferPeriodService {

    getTransferPeriodList(): Promise<TransferPeriod[]> {
        let transferPeriods: TransferPeriod[] = [];
        transferPeriods.push(new TransferPeriod(1, 'Daily'));
        transferPeriods.push(new TransferPeriod(2, 'Monthly'));
        transferPeriods.push(new TransferPeriod(3, 'Quarterly'));
        return new Promise((resolve) =>
            setTimeout(() => resolve(transferPeriods), 5000)
        );
    }
}