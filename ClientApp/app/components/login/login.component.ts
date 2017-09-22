import { Component, Output, EventEmitter, Input } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { RestService } from "../../RestService/rest.service";
import { AlertService } from "../../AlertService/alert.service";
import "rxjs/Rx";

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent {
    @Output() myEvent = new EventEmitter();
    model: any = {};
    returnUrl: string;

    constructor(private restService: RestService, private alertService: AlertService, private activatedRouter: ActivatedRoute, private router: Router) {
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
                this.alertService.error(error);
            });
        this.model = {}; 
    }
}
