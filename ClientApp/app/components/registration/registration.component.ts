import { Component} from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { RestService } from '../../RestService/rest.service';
import { MessageService } from '../../MessageService/message.service';
import { Language } from "angular-l10n";
import 'rxjs/Rx';

@Component({
    selector: 'registration',
    templateUrl: './registration.component.html',
    styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {
    @Language() lang;
    model: any = {};
    returnUrl: string;
    loading = false;

    constructor(private restService: RestService, private router: Router,
        private activatedRoute: ActivatedRoute, private messageService: MessageService) {
        this.returnUrl = activatedRoute.snapshot.queryParams['returnUrl'] || '/';
    }

    registration() {
        this.restService.registration(this.model.name, this.model.email, this.model.username, this.model.password)
            .subscribe(data => {
                this.messageService.sendSuccessMessage('Check your email to confirm account')
            },
            error => {
                this.messageService.sendErrorMessage('Registration failed');
            })
        this.model = {};
    }
}
