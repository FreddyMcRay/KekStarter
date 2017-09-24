import { Component, Output, EventEmitter, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { RestService } from '../../RestService/rest.service';
import { MessageService } from '../../MessageService/message.service';
import { Language } from 'angular-l10n';
import 'rxjs/Rx';

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    @Language() lang;
    @Output() myEvent = new EventEmitter();
    model: Login = new Login();
    loginForm: FormGroup;
    returnUrl: string;

    constructor(private restService: RestService, private activatedRouter: ActivatedRoute,
        private router: Router, private messageService: MessageService, private fb: FormBuilder ) {
        this.returnUrl = activatedRouter.snapshot.queryParams['returnUrl'] || "/";
    }

    ngOnInit() {
        this.loginForm = this.fb.group({
            'login': [this.model.login, Validators.required],
            'password': [this.model.password, Validators.required]
        });
    }

    login() {
        if (this.loginForm.valid) {
            this.model = this.loginForm.value;
            this.restService.login(this.model)
                .subscribe(
                data => {
                    console.log("Login back to front");
                    this.myEvent.emit(false);
                    this.messageService.sendSuccessMessage("Login success");
                },
                error => {
                    this.myEvent.emit(true);
                    this.messageService.sendErrorMessage("Login failed");
                });
            this.loginForm.reset();
        }
    }
}

class Login {
    login: string;
    password: string;
}
