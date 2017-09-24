import { Component, OnDestroy } from '@angular/core';
import { Http, Headers, Response, Request, RequestOptions, RequestMethod } from '@angular/http';
import { ActivatedRoute } from '@angular/router';
import { CloudinaryOptions, CloudinaryUploader, CloudinaryImageComponent } from 'ng2-cloudinary';
import { Subscription } from 'rxjs/Subscription';
import { RestService } from '../../RestService/rest.service';
import { UserProject } from '../../models/project.models';
import { UserProfile, UserAchivment, AuthUser } from '../../models/user.models';
import { UserService } from '../../UserService/user.service';
import { Language } from 'angular-l10n';

@Component({
    selector: 'profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnDestroy {
    @Language() lang;
    private id: number;
    public currentUser: AuthUser;
    public user: UserProfile = new UserProfile();
    public achivments: UserAchivment[];
    public projects: UserProject[] = [];
    public followedProjects: UserProject[] = [];
    private subscription: Subscription;
    public uploader: CloudinaryUploader = new CloudinaryUploader(
        new CloudinaryOptions({ cloudName: 'dbsjugefb', uploadPreset: 'bkydfdx3' })
    );

    constructor(private http: Http, private activateRoute: ActivatedRoute, private service: RestService, private userService: UserService) {
        this.currentUser = this.userService.getCurrentUser();
        this.subscription = activateRoute.params.subscribe(params => this.id = params['id']);
        console.log(this.id);

        this.service.getUserById(this.id.toString()).subscribe(result => {
            this.user = result.json();
            if (this.user.urlPhoto == null)
                this.user.urlPhoto = "https://res.cloudinary.com/dbsjugefb/image/upload/w_250,h_250,c_thumb,r_max/v1505042128/anonim_user_vdzhx0.jpg";
            console.log(this.user);
            console.log(this.user.achivments)
            console.log("GetUserById");
            this.achivments = this.user.achivments;
            this.projects = this.user.projects;
            this.followedProjects = this.user.followedProjects;
        });
        this.uploader.onSuccessItem = (item: any, response: string, status: number, headers: any): any => {
            let res: any = JSON.parse(response);
            this.user.urlPhoto = "https://res.cloudinary.com/dbsjugefb/image/upload/w_250,h_250,c_thumb,r_max/v1505042128/" + res.public_id + ".jpg";
            return { item, response, status, headers };
        };
    }

    public checkRole() {
        return (this.currentUser.role == 'Admin' || this.currentUser.id == this.user.id) ? true : false;
    }

    onChange(event: any) {
        this.uploader.uploadAll();
    }

    ngOnDestroy() {
        console.log(this.user);
        this.service.editProfile({ id: this.user.id.toString(), UrlPhoto: this.user.urlPhoto });
    }
}
