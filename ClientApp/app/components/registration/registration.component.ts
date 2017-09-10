import { Component, ViewChild, ElementRef } from '@angular/core';
import { RestService } from "../../RestService/rest.service";

@Component({
    selector: 'registration',
    templateUrl: './registration.component.html',
    styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {
    model: any = {};
    loading = false;
    @ViewChild('closeBtn') closeBtn: ElementRef;

    constructor(private restService: RestService) { }

    registration() {
        this.restService.registration(this.model.name, this.model.email, this.model.username, this.model.password)
    }
}
