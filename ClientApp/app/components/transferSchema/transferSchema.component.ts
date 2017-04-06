import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { TransferSchema } from '../../models/transferSchema/transferSchema.model';
import { TransferPeriod } from '../../models/transferSchema/transferPeriod.model';
import { Account } from '../../models/account/account.model';
import { Router, ActivatedRoute } from '@angular/router';
import { TransferSchemaService } from '../../services/transferSchema/transferSchema.service';
import { TransferPeriodService } from '../../services/transferSchema/transferPeriod.service';
import { AccountService } from '../../services/account/account.service';

@Component({
    selector: 'transferSchema',
    template: require('./transferSchema.component.html')
})
export class TransferSchemaComponent {
    id: number;
    accounts: Account[] = [];
    transferPeriods: TransferPeriod[] = [];
    myForm: FormGroup;
    submitted: boolean = false;
    saved: boolean = false;

    constructor(
        private http: Http,
        private fb: FormBuilder,
        private route: ActivatedRoute,
        private transferSchemaService: TransferSchemaService,
        private transferPeriodService: TransferPeriodService,
        private accountService: AccountService,
        private router: Router) {
        this.myForm = fb.group({
            'id': [null],
            'accountFromId': [null, Validators.required],
            'accountToId': [null, Validators.required],
            'title': [null, Validators.required],
            'amount': [null, Validators.required],
            'transferStartDate': [null, Validators.required],
            'transferPeriod': [null, Validators.required],
            'transferEndDate': [null]
        });
    }

    ngOnInit(): void {
        var accountPromise: Promise<Account[]> = this.accountService.getAccountList()
            .then(accounts => this.accounts = accounts);
        var transferPeriodPromise: Promise<TransferPeriod[]> = this.transferPeriodService.getTransferPeriodList()
            .then(transferPeriods => this.transferPeriods = transferPeriods);
        this.route.queryParams.subscribe(params => {
            this.id = params['id'];
            if (this.id) {
                var transferSchemaPromise: Promise<TransferSchema> = this.transferSchemaService.getTransferSchema(this.id);
                Promise.all([
                    accountPromise,
                    transferSchemaPromise,
                    transferPeriodPromise
                ]).then(value => {
                    delete value[1].lastTransferDate;
                    this.myForm.setValue(value[1]);
                })
            }
        })
    }

    onSubmit(value: any): void {
        this.saved = false;
        this.submitted = true;
        if (this.myForm.valid) {
            this.transferSchemaService.postTransferSchema(this.myForm.value)
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
            this.transferSchemaService.deleteTransferSchema(this.id)
                .then(ok => {
                    if (ok) {
                        this.router.navigate(['./transferSchemaList']);
                    }
                });
        }
        event.stopPropagation();
    }
}
