import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Subscription } from 'rxjs/Subscription';
import { RestService } from "../../RestService/rest.service";
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { RoleService } from "../../RoleService/role.service";


@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    providers: [RoleService, RestService],
})
export class AppComponent {
    angularClientSideData = 'Angular';
    text: string;
    results: string[];
    user: AuthUser = new AuthUser();
    public constructor( private titleService: Title, private router: Router, private service: RestService) {

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
        //this.user = { id: 1, role: "user" };
        //RoleService.setCurrentAuthUser(this.user);
        //console.log(this.user);
    }

    public setTitle(newTitle: string) {
        this.titleService.setTitle(newTitle);

    }

    public checkRole(): boolean {
        let AuthUser = RoleService.getCurrentAuthUser();
        return (this.user.role == "Admin" || this.user.id == AuthUser.id) ? true : false;
    }

}

class AuthUser {
    id: number = 0;
    role: string;
}