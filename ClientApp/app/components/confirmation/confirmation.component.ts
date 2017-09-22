import { Component } from '@angular/core';
import { CloudinaryOptions, CloudinaryUploader } from "ng2-cloudinary";
import { Ng2FileDropAcceptedFile, Ng2FileDropRejectedFile } from 'ng2-file-drop';

@Component({
    selector: 'confirmation',
    templateUrl: 'confirmation.component.html',
    styleUrls: ['confirmation.component.css']
})

export class ConfirmationComponent {

    public scanImage: string;

    public supportedFileTypes: string[] = ['image/png', 'image/jpeg', 'image/gif']

    uploader: CloudinaryUploader = new CloudinaryUploader(
        new CloudinaryOptions({ cloudName: 'dbsjugefb', uploadPreset: 'bkydfdx3' })
    );
    constructor() {
        this.uploader.onSuccessItem = (item: any, response: string, status: number, headers: any): any => {
            console.log(item);
            let res: any = JSON.parse(response);
            this.scanImage = 'http://res.cloudinary.com/' + this.uploader.cloudName +
                '/image/upload/w_800,h_450/v1505306556/' + res.public_id + '.jpg';
            console.log(this.scanImage);
            return { item, response, status, headers };
        };
    }
    public dragFileAccepted(acceptedFile: Ng2FileDropAcceptedFile) {
        this.uploader.uploadAll();
    }
   
}