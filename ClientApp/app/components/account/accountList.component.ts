import { Component, OnInit } from '@angular/core';
import { Account } from '../../models/account/account.model';
import { AccountService } from '../../services/account/account.service';

@Component({
    selector: 'accountList',
    template: require('./accountList.component.html')
})
export class AccountListComponent implements OnInit {
    accounts: Account[] = [];

    constructor(private accountService: AccountService) {
    }

    ngOnInit(): void {
        this.accountService.getAccountList()
            .then(accounts => this.accounts = accounts);
    }
}
