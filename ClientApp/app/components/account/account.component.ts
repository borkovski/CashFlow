import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Account } from '../../models/account/account.model';
import { Router, ActivatedRoute } from '@angular/router';
import { AccountService } from '../../services/account/account.service';

@Component({
    selector: 'account',
    template: require('./account.component.html')
})
export class AccountComponent implements OnInit {
    id: number;
    myForm: FormGroup;
    submitted: boolean = false;
    saved: boolean = false;
    account: Account;

    constructor(
        private http: Http,
        private fb: FormBuilder,
        private route: ActivatedRoute,
        private AccountService: AccountService,
        private router: Router) {
        this.account = new Account();
        this.myForm = fb.group({
            'id': [null],
            'name': [this.account.name, Validators.required],
            'accountType': [this.account.accountType, Validators.required],
            'currentBalance': [null]
        });
    }

    ngOnInit(): void {
        this.route.queryParams.subscribe(params => {
            this.id = params['id'];
            if (this.id) {
                this.AccountService.getAccount(this.id)
                    .then(account => {
                        this.account = account;
                        this.myForm.setValue(account);
                    });
            }
        });
    }

    onSubmit(value: any): void {
        this.saved = false;
        this.submitted = true;
        if (this.myForm.valid) {
            this.account = this.myForm.value;
            this.AccountService.postAccount(this.account)
                .then(id => {
                    if (id) {
                        this.myForm.controls['id'].setValue(id);
                        this.saved = true;
                    }
                });
        }
    }

    delete(event: any): void {
        if (this.account.id) {
            this.AccountService.deleteAccount(this.account.id)
                .then(ok => {
                    if (ok) {
                        this.router.navigate(['./accountList']);
                    }
                });
        }
        event.stopPropagation();
    }
}
