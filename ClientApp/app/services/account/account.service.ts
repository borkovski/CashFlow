import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { BaseService } from '../base/base.service';
import * as moment from 'moment';

import 'rxjs/add/operator/toPromise';
import { Account } from '../../models/account/account.model';

@Injectable()
export class AccountService extends BaseService<Account> {
    protected url = 'http://localhost:10503/api/Account';

    getAccountList(): Promise<Account[]> {
        return this.getList();
    }

    getAccount(accountId: number): Promise<Account> {
        return this.get(accountId);
    }

    postAccount(account: Account): Promise<number> {
        return this.post(account);
    }

    deleteAccount(accountId: number): Promise<boolean> {
        return this.delete(accountId);
    }
}