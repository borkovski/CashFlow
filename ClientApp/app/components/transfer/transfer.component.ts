import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Transfer } from '../../models/transfer/transfer.model';

@Component({
    selector: 'transfer',
    template: require('./transfer.component.html')
})
export class TransferComponent {
    myForm: FormGroup;
    submitted: boolean = false;
    transfer: Transfer;

    constructor(public http: Http, fb: FormBuilder) {
        this.transfer = new Transfer();
        this.myForm = fb.group({
            'id': [null],
            'title': [this.transfer.title, Validators.required],
            'amount': [this.transfer.amount, Validators.required],
            'direction': [this.transfer.direction, Validators.required],
            'date': [this.transfer.date, Validators.required],
            'isRepeated': [this.transfer.isRepeated],
            'repeatPeriod': [this.transfer.repeatPeriod],
            'finishDate': [this.transfer.finishDate],
            'isContinuous': [this.transfer.isContinuous]
        });
        this.myForm.controls['isRepeated'].valueChanges.subscribe((value: boolean) => {
            if (!value) {
                this.myForm.controls['repeatPeriod'].setValue(null);
            }
        });
    }

    onSubmit(value: any): void {
        this.submitted = true;
        if (this.myForm.valid) {
            this.http.post('http://localhost:10503/api/Transfer', this.myForm.value)
                .subscribe((res: Response) => {
                    this.myForm.setValue(res.json());
                    this.myForm.controls['date'].setValue(res.json().date.substring(0, 10));
                    this.transfer = this.myForm.value;
                });
        }
    }
}
