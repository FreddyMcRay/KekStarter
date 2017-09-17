import { Component, OnInit } from '@angular/core';
import { RestService } from "../../RestService/rest.service";
import { Subscription } from 'rxjs/Subscription';
import { ActivatedRoute } from '@angular/router';
import { AuthUser } from '../../models/user.models';
import { UserProjectFull } from '../../models/project.models';

@Component({
    selector: 'displayProject',
    templateUrl: './displayProject.component.html',
    styleUrls: ['./displayProject.component.css']
})
export class DisplayProjectComponent {
    id: number;
    private subscription: Subscription;
    user: AuthUser;
    project: UserProjectFull;
    guest: boolean = true;

    constructor(private service: RestService, private activateRoute: ActivatedRoute) {
        if (!(typeof localStorage === "undefined") && localStorage.getItem('currentUser')) {
            this.guest = false;
            this.user = JSON.parse(localStorage.getItem('currentUser') || "");
        }
        this.subscription = activateRoute.params.subscribe(params => this.id = params['id']);
        console.log(this.id);
        this.service.getProjectById(this.id.toString()).subscribe(result => {
            this.project = result.json();
        })
    }


    public followProject() {
        this.service.userFollowProject(this.user.id.toString(), this.project.id.toString())
            .subscribe(data => { this.project.followed = true },
            error => { }
            )
    }

    public unFollowProject() {
        this.service.userUnFollowProject(this.user.id.toString(), this.project.id.toString())
            .subscribe(data => { this.project.followed = false },
            error => { }
            )
    }
}