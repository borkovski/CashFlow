import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import { AccountHistory } from '../../models/account/accountHistory.model';
import { AccountService } from '../../services/account/account.service';

@Component({
    selector: 'accountHistory',
    template: require('./accountHistory.component.html')
})
export class AccountHistoryComponent implements OnInit {
    id: number;
    accountHistory: AccountHistory;

    constructor(
        private http: Http,
        private route: ActivatedRoute,
        private AccountService: AccountService,
        private router: Router) {
    }
    
    ngOnInit(): void {
        this.route.queryParams.subscribe(params => {
            this.id = params['id'];
            if (this.id) {
                this.AccountService.getAccountHistory(this.id)
                    .then(account => {
                        this.accountHistory = account;
                    });
            }
        });
    }
}
