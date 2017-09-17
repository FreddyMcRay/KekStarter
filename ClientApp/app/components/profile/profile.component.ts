import { Component, OnDestroy } from '@angular/core';
import { Http, Headers, Response, Request, RequestOptions, RequestMethod } from '@angular/http';
import { ActivatedRoute } from '@angular/router';
import { CloudinaryOptions, CloudinaryUploader, CloudinaryImageComponent } from 'ng2-cloudinary';
import { Subscription } from 'rxjs/Subscription';
import { RestService } from '../../RestService/rest.service';
import { UserProject } from '../../models/project.models';
import { UserProfile, UserAchivment } from '../../models/user.models';


@Component({
    selector: 'profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnDestroy {
    private id: number;
    public user: UserProfile;
    public achivments: UserAchivment[];
    public projects: UserProject[];
    public imageUrl: string = "https://res.cloudinary.com/dbsjugefb/image/upload/w_250,h_250,c_thumb,r_max/v1505042128/anonim_user_vdzhx0.jpg";
    private subscription: Subscription;
    public uploader: CloudinaryUploader = new CloudinaryUploader(
        new CloudinaryOptions({ cloudName: 'dbsjugefb', uploadPreset: 'bkydfdx3' })
    );

    constructor(private http: Http, private activateRoute: ActivatedRoute, private service: RestService) {
        this.subscription = activateRoute.params.subscribe(params => this.id = params['id']);
        console.log(this.id);
        this.service.getUserById(this.id.toString()).subscribe(result => {
            this.user = result.json();
            if (this.user.UrlPhoto == null)
                this.user.UrlPhoto = "https://res.cloudinary.com/dbsjugefb/image/upload/w_250,h_250,c_thumb,r_max/v1505042128/anonim_user_vdzhx0.jpg";
            console.log(this.user);
            console.log(this.user.achivments)
            console.log("GetUserById");
            this.achivments = this.user.achivments;
            this.projects = this.user.projects;
        });
        this.uploader.onSuccessItem = (item: any, response: string, status: number, headers: any): any => {
            let res: any = JSON.parse(response);
            this.imageUrl = "https://res.cloudinary.com/dbsjugefb/image/upload/w_250,h_250,c_thumb,r_max/v1505042128/" + res.public_id + ".jpg";
            return { item, response, status, headers };
        };
    }

    onChange(event: any) {
        this.uploader.uploadAll();
    }

    ngOnDestroy() {
        console.log(this.user);
        this.service.editProfile(this.user);
    }
}
