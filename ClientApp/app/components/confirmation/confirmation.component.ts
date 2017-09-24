import { Component } from '@angular/core';
import { CloudinaryOptions, CloudinaryUploader } from 'ng2-cloudinary';
import { Ng2FileDropAcceptedFile, Ng2FileDropRejectedFile } from 'ng2-file-drop';
import { RestService } from '../../RestService/rest.service';
import { AuthUser } from '../../models/user.models';
import { UserService } from '../../UserService/user.service';
import { Router } from '@angular/router';
import { Language } from 'angular-l10n';

@Component({
    selector: 'confirmation',
    templateUrl: 'confirmation.component.html',
    styleUrls: ['confirmation.component.css']
})

export class ConfirmationComponent {
    @Language() lang;
    public scanImage: string;
    public successUpload: boolean = false;
    public user: AuthUser
    public supportedFileTypes: string[] = ['image/png', 'image/jpeg', 'image/gif']

    uploader: CloudinaryUploader = new CloudinaryUploader(
        new CloudinaryOptions({ cloudName: 'dbsjugefb', uploadPreset: 'bkydfdx3' })
    );
    constructor(private service: RestService, private userService: UserService, private router: Router) {
        this.user = this.userService.getCurrentUser();

        this.uploader.onSuccessItem = (item: any, response: string, status: number, headers: any): any => {
            console.log(item);
            let res: any = JSON.parse(response);
            this.scanImage = 'http://res.cloudinary.com/' + this.uploader.cloudName +
                '/image/upload/w_800,h_450/v1505306556/' + res.public_id + '.jpg';
            console.log(this.scanImage);
            this.successUpload = true;
            return { item, response, status, headers };
        };
    }
    public dragFileAccepted(acceptedFile: Ng2FileDropAcceptedFile) {
        this.uploader.uploadAll();
    }

    public sendToServer() {
        this.service.sendConfirmInfoToServer({ userId: this.user.id, scanImage: this.scanImage }).subscribe(
            result => {
                this.user.onCheck = true;
                localStorage.setItem('currentUser', JSON.stringify(this.user));
                console.log('nice confirmation');
                this.router.navigate(['/home']);
            })
    }

    public declineSending() {
        this.successUpload = false;
        this.scanImage = '';
    }
   
}