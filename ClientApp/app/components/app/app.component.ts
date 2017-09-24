import { Component } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { RestService } from '../../RestService/rest.service';
import { ActivatedRoute, Router } from '@angular/router';
import { RoleService } from '../../RoleService/role.service';
import { UserService } from '../../UserService/user.service';
import { AuthUser } from '../../models/user.models';
import { Message } from 'primeng/primeng';
import { MessageService } from '../../MessageService/message.service';
import { LocaleService, Language } from 'angular-l10n';
import { UserService } from '../../UserService/user.service';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    providers: [RoleService, RestService],
})
export class AppComponent {
    @Language() lang: string;
    angularClientSideData = 'Angular';
    text: string;
    subscription: Subscription;
    loading: boolean = false;
    returnUrl: string;
    guest: boolean = true;
    message: Message[] = [];
    user: AuthUser;
    public constructor(private messageService: MessageService, private router: Router,
       private service: RestService, private activeRoute: ActivatedRoute, private userService: UserService) {
            
this.user = this.userService.getCurrentUser();
        if (this.user.role != 'Guest')
            this.guest = false;
        this.subscription = this.messageService.getMessage().subscribe(message => { this.message.push(message) });
        this.returnUrl = activeRoute.snapshot.queryParams['returnUrl'] || '/';
        }
        this.returnUrl = activeRoute.snapshot.queryParams["returnUrl"] || "/";
    }

    selectLanguage(language: string) {
        this.locale.setCurrentLanguage(language);
        this.user.language = language;
        if (this.user.role != 'Guest') {
            localStorage.setItem('currentUser', JSON.stringify(this.user));
        }
    }

    public logOut() {
        localStorage.removeItem('currentUser');
        this.service.logOut().subscribe(result => console.log(result));
        this.guest = true;
        this.user = new AuthUser();
        this.router.navigate([this.returnUrl]);
    }

    public checkAdmin() {
        return (this.user.role == 'Admin') ? true : false;
    }

    handleEvent(value: boolean) {
        this.guest = value;
        this.loading = false;
        if (value == true) {
            this.message.push({ severity: 'error', summary: 'Error', detail: 'Login failed' });
        } else {
            if (value == false) {
                this.message.push({ severity: 'info', summary: 'Success', detail: 'Login success' });
                this.user = JSON.parse(localStorage.getItem('currentUser') || "");
                console.log(this.user);
            }
        }
        this.user = JSON.parse(localStorage.getItem('currentUser') || "");
        console.log(this.user);
    }

    registrationHandle(event: any) {
        this.message.push({ severity: 'info', summary: 'Success', detail: 'Check your email to confirm account' });
    }

    clear() {
        this.message = [];
    }
}