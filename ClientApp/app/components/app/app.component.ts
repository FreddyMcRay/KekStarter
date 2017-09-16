import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Subscription } from 'rxjs/Subscription';
import { RestService } from "../../RestService/rest.service";
import { ActivatedRoute, Router } from '@angular/router';
import { RoleService } from "../../RoleService/role.service";
import { AuthUser } from '../../models/user.models';


@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    providers: [RoleService, RestService],
})
export class AppComponent {
    angularClientSideData = 'Angular';
    text: string;
    loading: boolean = false;
    returnUrl: string;
    guest: boolean = true;
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
        this.guest = true;
        this.user = new AuthUser();
        this.router.navigate([this.returnUrl]);
    }

    handleEvent(value: boolean) {
        this.guest = value;
        this.loading = false;
        this.user = JSON.parse(localStorage.getItem('currentUser') || "");
        console.log(this.user);
    }
}