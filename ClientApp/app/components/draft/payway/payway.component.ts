import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { Project, Payment } from '../../../models/draft.models';


@Component({
    selector: 'payway',
    templateUrl: './payway.component.html',
    styleUrls: ['./payway.component.css'],
})
export class PayWayComponent implements OnInit {
    @Input() project: Project;
    checking: boolean = false;
    paywayForm: FormGroup;
    payment: Payment = new Payment();

    constructor(private fb: FormBuilder) { }

    ngOnInit() {
        this.paywayForm = this.fb.group({
            'cardNumber': [this.payment.cardNumber, Validators.required],
            'expirationDate': [this.payment.expirationDate, Validators.required],
            'cvCode': [this.payment.cvCode, Validators.required],
            'owner': [this.payment.owner, Validators.required]
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