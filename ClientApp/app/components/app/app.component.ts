import { Component } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { RestService } from "../../RestService/rest.service";
import { ActivatedRoute, Router } from '@angular/router';
import { RoleService } from "../../RoleService/role.service";
import { AuthUser } from '../../models/user.models';
import { Message } from 'primeng/primeng';
import { MessageService } from '../../MessageService/message.service';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    providers: [RoleService, RestService],
})
export class AppComponent {
    angularClientSideData = 'Angular';
    text: string;
    subscription: Subscription;
    loading: boolean = false;
    returnUrl: string;
    guest: boolean = true;
    message: Message[] = [];
    user: AuthUser;
    public constructor(private messageService: MessageService, private router: Router, private service: RestService, private activeRoute: ActivatedRoute) {
        if (!(typeof localStorage === "undefined") && localStorage.getItem('currentUser')) {
            this.user = JSON.parse(localStorage.getItem('currentUser') || "");
            if (this.user.role !== "Guest") {
                this.guest = false;
            }
        } else {
            this.user = new AuthUser();
        }
        this.subscription = this.messageService.getMessage().subscribe(message => { this.message.push(message) });
        this.returnUrl = activeRoute.snapshot.queryParams["returnUrl"] || "/";
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
        if (value == false) {
            this.user = JSON.parse(localStorage.getItem('currentUser'));
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