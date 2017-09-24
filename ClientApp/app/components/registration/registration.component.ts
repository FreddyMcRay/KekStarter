import { Component, OnInit} from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { RestService } from '../../RestService/rest.service';
import { MessageService } from '../../MessageService/message.service';
import { Language } from "angular-l10n";
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import 'rxjs/Rx';

@Component({
    selector: 'registration',
    templateUrl: './registration.component.html',
    styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
    @Language() lang;
    model: Registration = new Registration();
    registrationForm: FormGroup;
    returnUrl: string;
    loading = false;

    constructor(private restService: RestService, private router: Router,
        private activatedRoute: ActivatedRoute, private messageService: MessageService, private fb: FormBuilder) {
        this.returnUrl = activatedRoute.snapshot.queryParams['returnUrl'] || '/';
    }

    ngOnInit() {
        this.registrationForm = this.fb.group({
            'name': [this.model.name, Validators.required],
            'email': [this.model.email, Validators.required],
            'login': [this.model.login, Validators.required],
            'password': [this.model.password, Validators.required]
        });
    }

    registration() {
        if (this.registrationForm.valid) {
            this.model = this.registrationForm.value;
            this.restService.registration(this.model)
                .subscribe(data => {
                    this.messageService.sendSuccessMessage('Check your email to confirm account')
                },
                error => {
                    this.messageService.sendErrorMessage('Registration failed');
                });
            this.registrationForm.reset();
        } else {
            this.messageService.sendErrorMessage('Wrong parameters');
        }
    }
}

class Registration {
    name: string;
    email: string;
    login: string;
    password: string;
}
