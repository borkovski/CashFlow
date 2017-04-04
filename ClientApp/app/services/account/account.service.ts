import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { BaseService } from '../base/base.service';
import * as moment from 'moment';

import 'rxjs/add/operator/toPromise';
import { Account } from '../../models/account/account.model';
import { AccountHistory } from '../../models/account/accountHistory.model';

@Injectable()
export class AccountService extends BaseService<Account> {
    protected url = 'http://localhost:10503/api/Account';

    getAccountList(): Promise<Account[]> {
        return this.getList();
    }

    getAccount(accountId: number): Promise<Account> {
        return this.get(accountId);
    }

    getAccountHistory(accountId: number): Promise<AccountHistory> {
        return this.http.get(this.url + 'History/' + accountId).toPromise()
            .then(response => {
                return response.json() as AccountHistory;
            })
            .then(accountHistory => {
                if (accountHistory) {
                    accountHistory.incomingTransfers = accountHistory.incomingTransfers.map(transfer => {
                        transfer.transferDate = moment.utc(transfer.transferDate).local().format('YYYY-MM-DD HH:mm:ss');
                        return transfer;
                    });
                    accountHistory.outgoingTransfers = accountHistory.outgoingTransfers.map(transfer => {
                        transfer.transferDate = moment.utc(transfer.transferDate).local().format('YYYY-MM-DD HH:mm:ss');
                        return transfer;
                    });
                    return accountHistory;
                }
            })
            .catch(this.handleError);
    }

    postAccount(account: Account): Promise<number> {
        return this.post(account);
    }

    deleteAccount(accountId: number): Promise<boolean> {
        return this.delete(accountId);
    }
}