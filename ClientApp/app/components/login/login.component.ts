import { Component, Output, EventEmitter, Input } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { RestService } from "../../RestService/rest.service";
import { Language } from 'angular-l10n';
import "rxjs/Rx";

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent {
    @Language() lang;
    @Output() myEvent = new EventEmitter();
    model: any = {};
    returnUrl: string;

    constructor(private restService: RestService, private activatedRouter: ActivatedRoute, private router: Router) {
        this.returnUrl = activatedRouter.snapshot.queryParams['returnUrl'] || "/";
    }

    login() {
        console.log("aaaaa");
        this.restService.login(this.model.username, this.model.password)
            .subscribe(
            data => {
                console.log("Login back to front");
                this.myEvent.emit(false)
            },
            error => {
                this.myEvent.emit(true);
            });
        this.model = {}; 
    }
}
