import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
    selector: 'transfer',
    template: require('./transfer.component.html')
})
export class TransferComponent {
    myForm: FormGroup;
    submitted: boolean = false;

    constructor(fb: FormBuilder) {
        this.myForm = fb.group({
            'title': [null, Validators.required],
            'amount': [null],
            'direction': [null],
            'date': [null],
            'repeat': [null],
            'repeatPeriod': [null],
            'repeatMonth': [null],
            'repeatDay': [null],
            'isContinuous': [null]
        });
        this.myForm.controls['repeat'].valueChanges.subscribe((value: boolean) => {
            if (!value) {
                this.myForm.controls['repeatPeriod'].setValue(null);
                this.myForm.controls['repeatMonth'].setValue(null);
                this.myForm.controls['repeatDay'].setValue(null);
            }
        });
        this.myForm.controls['repeatPeriod'].valueChanges.subscribe((value: string) => {
            if (value == '3') {
                this.myForm.controls['repeatMonth'].setValue(null);
            }
        });
    }

    onSubmit(value: any): void {
        this.submitted = true;
        if (this.myForm.valid) {
            console.log(value);
        }
    }
}
