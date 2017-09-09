import { Component, ViewChild, ElementRef } from '@angular/core';
import { RestService } from "../../RestService/rest.service";

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent {
    model: any = {};
    loading = false;
    @ViewChild('closeBtn')
    closeBtn: ElementRef;

    constructor(private restService: RestService) {}

    login() {
        this.loading = true;
        this.restService.login(this.model.username, this.model.password)
            .subscribe(data => {
                    this.closeBtn.nativeElement.click();
                },
                error => {
                    console.log(error);
                    this.loading = false;
                }
            )
    }
}
