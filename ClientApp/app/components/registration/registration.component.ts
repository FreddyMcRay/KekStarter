import { Component } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { RestService } from "../../RestService/rest.service";
import { AlertService } from "../../AlertService/alert.service"
import "rxjs/Rx";

@Component({
    selector: 'registration',
    templateUrl: './registration.component.html',
    styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {
    model: any = {};
    returnUrl: string;
    loading = false;

    constructor(private restService: RestService, private router: Router, private activatedRoute: ActivatedRoute, private alertService: AlertService) {
        this.returnUrl = activatedRoute.snapshot.queryParams['returnUrl'] || '/';
    }

    registration() {
        this.restService.registration(this.model.name, this.model.email, this.model.username, this.model.password)
            .subscribe(data => {
                this.alertService.success("Check your email to confirm your account", true);
            },
            error => {
                this.alertService.error(error);
            })
    }
}
