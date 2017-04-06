import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Transfer } from '../../models/transfer/transfer.model';
import { Account } from '../../models/account/account.model';
import { Router, ActivatedRoute } from '@angular/router';
import { TransferService } from '../../services/transfer/transfer.service';
import { AccountService } from '../../services/account/account.service';

@Component({
    selector: 'transfer',
    template: require('./transfer.component.html')
})
export class TransferComponent {
    id: number;
    accounts: Account[] = [];
    myForm: FormGroup;
    submitted: boolean = false;
    saved: boolean = false;

    constructor(
        private http: Http,
        private fb: FormBuilder,
        private route: ActivatedRoute,
        private transferService: TransferService,
        private accountService: AccountService,
        private router: Router) {
        this.myForm = fb.group({
            'id': [null],
            'accountFromId': [null, Validators.required],
            'accountToId': [null, Validators.required],
            'title': [null, Validators.required],
            'amount': [null, Validators.required],
            'transferDate': [null, Validators.required]
        });
    }

    ngOnInit(): void {
        var accountPromise:Promise<Account[]> = this.accountService.getAccountList()
            .then(accounts => this.accounts = accounts);
        this.route.queryParams.subscribe(params => {
            this.id = params['id'];
            if (this.id) {
                var transferPromise: Promise<Transfer> = this.transferService.getTransfer(this.id);
                Promise.all([
                    accountPromise,
                    transferPromise
                ]).then(value => {
                    delete value[1].accountFrom;
                    delete value[1].accountTo;
                    this.myForm.setValue(value[1]);
                })
            }
        })
    }

    onSubmit(value: any): void {
        this.saved = false;
        this.submitted = true;
        if (this.myForm.valid) {
            this.transferService.postTransfer(this.myForm.value)
                .then(id => {
                    if (id) {
                        this.myForm.controls['id'].setValue(id);
                        this.id = id;
                        this.saved = true;
                    }
                });
        }
    }

    delete(event: any): void {
        if (this.id) {
            this.transferService.deleteTransfer(this.id)
                .then(ok => {
                    if (ok) {
                        this.router.navigate(['./transferList']);
                    }
                });
        }
        event.stopPropagation();
    }
}
