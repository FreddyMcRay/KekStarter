import { Component, Output, EventEmitter } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { RestService } from "../../RestService/rest.service";
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
    @Output() myEvent = new EventEmitter();

    constructor(private restService: RestService, private router: Router, private activatedRoute: ActivatedRoute) {
        this.returnUrl = activatedRoute.snapshot.queryParams['returnUrl'] || '/';
    }

    registration() {
        this.restService.registration(this.model.name, this.model.email, this.model.username, this.model.password)
            .subscribe(data => {
                this.myEvent.emit();
            },
            error => {
            })
        this.model = {};
    }
}
