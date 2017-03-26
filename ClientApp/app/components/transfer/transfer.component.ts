import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Transfer } from '../../models/transfer/transfer.model';
import { ActivatedRoute } from '@angular/router';
import { TransferService } from '../../services/transfer/transfer.service';

@Component({
    selector: 'transfer',
    template: require('./transfer.component.html')
})
export class TransferComponent {
    id: number;
    myForm: FormGroup;
    submitted: boolean = false;
    saved: boolean = false;
    transfer: Transfer;

    constructor(public http: Http, fb: FormBuilder, private route: ActivatedRoute, private transferService: TransferService) {
        this.transfer = new Transfer();
        this.myForm = fb.group({
            'id': [null],
            'accountFromId': [this.transfer.accountFromId, Validators.required],
            'accountToId': [this.transfer.accountToId, Validators.required],
            'title': [this.transfer.title, Validators.required],
            'amount': [this.transfer.amount, Validators.required],
            'transferDate': [this.transfer.transferDate, Validators.required]
        });
    }

    ngOnInit(): void {
        this.route.queryParams.subscribe(params => {
            this.id = params['id'];
            if (this.id) {
                this.transferService.getTransfer(this.id)
                    .then(transfer => {
                        this.transfer = transfer;
                        this.myForm.setValue(transfer);
                    });
            }
        });
    }

    onSubmit(value: any): void {
        this.saved = false;
        this.submitted = true;
        if (this.myForm.valid) {
            this.transfer = this.myForm.value;
            this.transferService.postTransfer(this.transfer)
                .then(id => {
                    if (id) {
                        this.myForm.controls['id'].setValue(id);
                        this.saved = true;
                    }
                });
        }
    }
}
