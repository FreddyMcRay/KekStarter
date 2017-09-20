import { Component, OnDestroy } from '@angular/core';
import { RestService } from "../../RestService/rest.service";
import { Subscription } from 'rxjs/Subscription';
import { ActivatedRoute } from '@angular/router';
import { AuthUser } from '../../models/user.models';
import { UserProjectFull } from '../../models/project.models';
import { UserProfileMini } from '../../models/user.models';

@Component({
    selector: 'displayProject',
    templateUrl: './displayProject.component.html',
    styleUrls: ['./displayProject.component.css']
})
export class DisplayProjectComponent implements OnDestroy {
    id: number;
    private subscription: Subscription;
    user: AuthUser = new AuthUser();
    project: UserProjectFull = new UserProjectFull();
    rating: number;
    guest: boolean = true;
    creater: UserProfileMini;
    tags: string[];

    constructor(private service: RestService, private activateRoute: ActivatedRoute) {
        if (!(typeof localStorage === "undefined")) {
            this.guest = false;
            this.user = JSON.parse(localStorage.getItem('currentUser'));
        }
        console.log(this.user);
        this.subscription = activateRoute.params.subscribe(params => this.id = params['id']);
        console.log(this.id);
        this.service.getProjectById(this.id.toString(), this.user.id.toString()).subscribe(result => {
            this.project = result.json();
            this.rating = this.project.userRating;
            this.tags = this.project.tags;
            this.creater = this.project.user;
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

    ngOnDestroy() {
        this.service.addRatingToProject({ ProjectId: this.project.id.toString(), UserPofileId: this.user.id.toString(), Value: this.rating.toString() });
    }
}