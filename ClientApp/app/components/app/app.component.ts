import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Subscription } from 'rxjs/Subscription';
import { RestService } from "../../RestService/rest.service";
import { ActivatedRoute, Router } from '@angular/router';
import { RoleService } from "../../RoleService/role.service";


@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    providers: [RoleService, RestService],
})
export class AppComponent {
    angularClientSideData = 'Angular';
    text: string;
    returnUrl: string;
    guest: boolean = true;
    user: AuthUser = new AuthUser();
    public constructor( private titleService: Title, private router: Router, private service: RestService, private activeRoute: ActivatedRoute) {

        console.log("CheckRoleServiceInAppComponent");
        console.log(RoleService.getCurrentAuthUser());
        //this.service.getCurrentUser().subscribe(result => {
        //    this.user = result.json();
        //    if (this.user == null) {
        //        this.user = new AuthUser();
        //        this.user.id = 0;
        //        this.user.role = "Guest";
        //    }
        //    RoleService.setCurrentAuthUser(this.user);
        //});
        this.returnUrl = activeRoute.snapshot.queryParams["returnUrl"] || "/";
        this.user = RoleService.getCurrentAuthUser();
        this.guest = (this.user.role == "Guest") ? true : false;
    }

    public setTitle(newTitle: string) {
        this.titleService.setTitle(newTitle);

    }

    public logOut() {
        this.user = {id: 0, role: "Guest"};
        RoleService.setCurrentAuthUser(this.user);
        this.guest = true;
        this.router.navigate([this.returnUrl]);
        console.log(this.user);
    }
}

class AuthUser {
    id: number = 0;
    role: string;
}