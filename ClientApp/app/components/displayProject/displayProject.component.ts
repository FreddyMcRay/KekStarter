import { Component, OnDestroy } from '@angular/core';
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
export class DisplayProjectComponent implements OnDestroy {
    id: number;
    private subscription: Subscription;
    user: AuthUser;
    project: UserProjectFull;
    rating: number;
    guest: boolean = true;

    constructor(private service: RestService, private activateRoute: ActivatedRoute) {
        if (!(typeof localStorage === "undefined") && localStorage.getItem('currentUser')) {
            this.guest = false;
            this.user = JSON.parse(localStorage.getItem('currentUser') || "");
        }
        //this.project = {
        //    id: 2, urlImage: 'http://res.cloudinary.com/profunding/image/upload/v1504950919/default-bg.jpg',
        //    title: 'looool', sponsors: 123, currentSum: 200, requiredSum: 300, description: 'lkjasnbd.sadm', leftOver: 15,
        //    dateEnd: '25.09.2017', percent: '66,6', status: true, followed: true,
        //}

        //this.subscription = activateRoute.params.subscribe(params => this.id = params['id']);
        //console.log(this.id);
        //this.service.getProjectById(this.id.toString()).subscribe(result => {
        //    this.project = result.json();
        //})
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
        this.service.addRatingToProject(this.rating.toString());
    }
}