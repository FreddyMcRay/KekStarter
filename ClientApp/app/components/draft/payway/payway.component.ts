import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Project, Payment } from '../../../models/draft.models';
import { Language } from 'angular-l10n';


@Component({
    selector: 'payway',
    templateUrl: './payway.component.html',
    styleUrls: ['./payway.component.css'],
})
export class PayWayComponent implements OnInit {
    @Language() lang;
    @Input() project: Project;
    checking: boolean = false;
    paywayForm: FormGroup;
    payment: Payment = new Payment();

    constructor(private fb: FormBuilder) { }

    ngOnInit() {
        this.paywayForm = this.fb.group({
            'cardNumber': [this.project.payment.cardNumber, Validators.required],
            'expirationDate': [this.project.payment.expirationDate, Validators.required],
            'cvCode': [this.project.payment.cvCode, Validators.required],
            'owner': [this.project.payment.owner, Validators.required]
        });
    }

    public checkCard() {
        if (this.paywayForm.valid) {
            this.payment = this.paywayForm.value;
            this.project.payment = this.payment;
            console.log(this.payment);
            this.checking = true;
        }
    }
}