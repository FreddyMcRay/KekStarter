import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Subscription } from 'rxjs/Subscription';
import { RestService } from "../../RestService/rest.service";
import { ActivatedRoute, Router } from '@angular/router';
import { RoleService } from "../../RoleService/role.service";
import { AuthUser } from '../../models/user.models';
import { Message } from 'primeng/primeng';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    providers: [RoleService, RestService],
})
export class AppComponent {
    angularClientSideData = 'Angular';
    text: string;
    loading: boolean = false;
    returnUrl: string;
    guest: boolean = true;
    message: Message[] = [];
    user: AuthUser;
    public constructor(private titleService: Title, private router: Router, private service: RestService, private activeRoute: ActivatedRoute) {
        if (!(typeof localStorage === "undefined") && localStorage.getItem('currentUser')) {
            this.user = JSON.parse(localStorage.getItem('currentUser') || "");
            if (this.user.role !== "Guest") {
                this.guest = false;
            }
        } else {
            this.user = new AuthUser();
        }

        this.returnUrl = activeRoute.snapshot.queryParams["returnUrl"] || "/";
    }

    public setTitle(newTitle: string) {
        this.titleService.setTitle(newTitle);

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