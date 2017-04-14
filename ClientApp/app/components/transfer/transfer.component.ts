import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Transfer } from '../../models/transfer/transfer.model';
import { Account } from '../../models/account/account.model';
import { Router, ActivatedRoute } from '@angular/router';
import { TransferService } from '../../services/transfer/transfer.service';
import { AccountService } from '../../services/account/account.service';
import { TransferPendingService } from '../../services/transferSchema/transferPending.service';
import { ToastsManager } from 'ng2-toastr/ng2-toastr';
import * as moment from 'moment';

@Component({
    selector: 'transfer',
    template: require('./transfer.component.html')
})
export class TransferComponent {
    id: number;
    isFromPending: boolean;
    accounts: Account[] = [];
    myForm: FormGroup;
    submitted: boolean = false;

    constructor(
        private http: Http,
        private fb: FormBuilder,
        private route: ActivatedRoute,
        private transferService: TransferService,
        private accountService: AccountService,
        private transferPendingService: TransferPendingService,
        private router: Router,
        private toastr: ToastsManager) {
        this.myForm = fb.group({
            'id': [null],
            'accountFromId': [null, Validators.required],
            'accountToId': [null, Validators.required],
            'title': [null, Validators.required],
            'amount': [null, Validators.required],
            'transferDate': [moment().format('YYYY-MM-DDTHH:mm'), Validators.required]
        });
    }

    ngOnInit(): void {
        var accountPromise: Promise<Account[]> = this.accountService.getAccountList()
            .then(accounts => this.accounts = accounts);
        this.route.queryParams.subscribe(params => {
            this.id = params['id'];
            this.isFromPending = params['isFromPending'];
            if (this.id) {
                if (!this.isFromPending) {
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
                else {
                    var transferPromise: Promise<Transfer> = this.transferPendingService.getTransferPending(this.id);
                    Promise.all([
                        accountPromise,
                        transferPromise
                    ]).then(value => {
                        delete value[1].accountFrom;
                        delete value[1].accountTo;
                        this.myForm.setValue(value[1]);
                    })
                }
            }
        })
    }

    onSubmit(value: any): void {
        this.submitted = true;
        if (this.myForm.valid) {
            let idPromise: Promise<number>;
            if (this.isFromPending) {
                idPromise = this.transferPendingService.postTransferPending(this.myForm.value);
            }
            else {
                idPromise = this.transferService.postTransfer(this.myForm.value);
            }
            if (idPromise) {
                idPromise
                    .then(id => {
                        if (id) {
                            this.toastr.success('Transfer saved!', 'Success!');
                            this.router.navigate(['./transferList']);
                        }
                    });
            }
        }
        else {
            this.toastr.error('Please fill in required fields', 'Oops!');
        }
    }

    delete(event: any): void {
        if (this.id) {
            this.transferService.deleteTransfer(this.id)
                .then(ok => {
                    if (ok) {
                        this.toastr.success('Transfer deleted!', 'Success!');
                        this.router.navigate(['./transferList']);
                    }
                });
        }
        event.stopPropagation();
    }
}
