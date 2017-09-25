import { Component, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { RestService } from '../../RestService/rest.service';
import { ActivatedRoute, Router } from '@angular/router';
import { RoleService } from '../../RoleService/role.service';
import { UserService } from '../../UserService/user.service';
import { AuthUser } from '../../models/user.models';
import { Message } from 'primeng/components/common/api';
import { MessageService } from '../../MessageService/message.service';
import { LocaleService, Language } from 'angular-l10n';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    providers: [RoleService, RestService],
})
export class AppComponent implements OnDestroy {
    @Language() lang;
    angularClientSideData = 'Angular';
    text: string;
    subscription: Subscription;
    loading: boolean = false;
    returnUrl: string;
    guest: boolean = true;
    message: Message[] = [];
    user: AuthUser;
    public constructor(private messageService: MessageService, private router: Router,
        private service: RestService, private activeRoute: ActivatedRoute, private userService: UserService, private locale: LocaleService) {
            
        this.user = this.userService.getCurrentUser()
        if (this.user.role != 'Guest')
            this.guest = false;
        this.selectLanguage(this.user.language);
        this.subscription = this.messageService.getMessage().subscribe(message => { this.message.push(message) });
        this.returnUrl = activeRoute.snapshot.queryParams['returnUrl'] || '/';
    }

    selectLanguage(language: string) {
        this.locale.setCurrentLanguage(language);
        this.user.language = language;
        if (this.user.role != 'Guest') {
            localStorage.setItem('currentUser', JSON.stringify(this.user));
        }
    }

    ngOnDestroy() {
        this.logOut();
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

    public checkUser() {
        return (this.user.role == 'User') ? true : false;
    }

    handleEvent(value: boolean) {
        this.guest = value;
        this.loading = false;
        if (value == false) {
            this.user = this.userService.getCurrentUser();
            console.log(this.user);
        } else {
            this.user = new AuthUser();
            console.log(this.user);
        }
    }


    clear() {
        this.message = [];
    }
}