import { Component, Input, ElementRef, Output, EventEmitter } from '@angular/core';
import { CloudinaryOptions, CloudinaryUploader } from 'ng2-cloudinary';
import { Ng2FileDropAcceptedFile, Ng2FileDropRejectedFile } from 'ng2-file-drop';
import { Project } from '../../models/draft.models';
import { Language } from 'angular-l10n';

@Component({

    selector: 'drag-and-drop',
    templateUrl: './drag-and-drop.component.html',
    styleUrls: ['./drag-and-drop.component.css'],
})
export class DragAndDropComponent {
    @Language() lang;
    @Input() project: Project;
    @Output() download = new EventEmitter();

    public supportedFileTypes: string[] = ['image/png', 'image/jpeg', 'image/gif']

    uploader: CloudinaryUploader = new CloudinaryUploader(
        new CloudinaryOptions({ cloudName: 'dbsjugefb', uploadPreset: 'bkydfdx3' })
    );
    constructor() {
        this.uploader.onSuccessItem = (item: any, response: string, status: number, headers: any): any => {
            console.log(item);
            let res: any = JSON.parse(response);
            this.project.image = 'http://res.cloudinary.com/' + this.uploader.cloudName +
                '/image/upload/w_800,h_450/v1505306556/' + res.public_id + '.jpg';
            this.download.emit();
            return { item, response, status, headers };
        };
    }
    public dragFileAccepted(acceptedFile: Ng2FileDropAcceptedFile) {
        this.uploader.uploadAll();
    }

}