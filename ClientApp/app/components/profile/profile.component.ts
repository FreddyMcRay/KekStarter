import { Component, OnDestroy } from '@angular/core';
import { Http, Headers, Response, Request, RequestOptions, RequestMethod } from '@angular/http';
import { ActivatedRoute } from '@angular/router';
import { CloudinaryOptions, CloudinaryUploader, CloudinaryImageComponent } from 'ng2-cloudinary';
import { Subscription } from 'rxjs/Subscription';


@Component({
    selector: 'profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.css']
})
export class ProfileComponent {
    private id: number;
    public user: UserProfile;
    public imageUrl: string = "https://res.cloudinary.com/dbsjugefb/image/upload/w_250,h_250,c_thumb,r_max/v1505042128/anonim_user_vdzhx0.jpg";
    private subscription: Subscription;
    public changeField: boolean = true;
    public instructionBool: boolean = true;
    public uploader: CloudinaryUploader = new CloudinaryUploader(
        new CloudinaryOptions({ cloudName: 'dbsjugefb', uploadPreset: 'bkydfdx3' })
    );

    constructor() {
        this.user = {
            id: 0,
            firstName: "Ivan",
            secondName: "Malich",
            urlPhoto: "",
            rating: 100,
            country: "Belarus",
            city: "Minsk",
            dataOfBirth: "10.10.1998",
            aboutMySelf: "scvsd",
            achivements: []
        };

        this.uploader.onSuccessItem = (item: any, response: string, status: number, headers: any): any => {
            let res: any = JSON.parse(response);
            this.imageUrl = "https://res.cloudinary.com/dbsjugefb/image/upload/w_250,h_250,c_thumb,r_max/v1505042128/" + res.public_id + ".jpg";
            return { item, response, status, headers };
        };
    }

    public change() {
        this.changeField = !this.changeField;
        console.log(this.user);
    }

    onChange(event: any) {
        this.uploader.uploadAll();
    }
}

class UserProfile {
    id: number;
    firstName: string;
    secondName: string;
    urlPhoto: string;
    rating: number;
    country: string;
    city: string;
    dataOfBirth: string;
    aboutMySelf: string;
    achivements: UserAchivement[];
}

class Achivment {
    id: number;
    name: string;
    urlImage: string;
    description: string;
}

class UserAchivement {
    id: number;
    achivment: Achivment;
}
class AuthUser {
    id: number = 0;
    role: string;
}