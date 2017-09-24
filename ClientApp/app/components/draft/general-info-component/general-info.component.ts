import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { Project, FinansalGoal } from '../../../models/draft.models'; 
import { CloudinaryOptions, CloudinaryUploader } from 'ng2-cloudinary';
import { Language } from 'angular-l10n';

@Component({
    selector: 'general-info',
    templateUrl: './general-info.component.html',
    styleUrls: ['./general-info.component.css']
})
export class GeneralInfoComponent implements OnInit {
    @Language() lang;
    @Input() project: Project;
    @Input() invalid: boolean;
    @ViewChild('fileSelect') fileSelect: ElementRef;
    @ViewChild('closeBtn') closeBtn: ElementRef;
    projectForm: FormGroup;
    finansalGoalForm: FormGroup;
    timeNow = new Date();

    public errorMessages = {
        'addTag': 'Your tag can have max 25 symbols',
    };
    public validators = [this.addTag];

    uploader: CloudinaryUploader = new CloudinaryUploader(
        new CloudinaryOptions({ cloudName: 'profunding', uploadPreset: 'profunding' })
    );

    constructor(private fb: FormBuilder) {
        this.uploader.onSuccessItem = (item: any, response: string, status: number, headers: any): any => {
            console.log(item);
            let res: any = JSON.parse(response);
            this.project.image = 'http://res.cloudinary.com/' + this.uploader.cloudName +
                '/image/upload/w_800,h_450,c_crop/v1505121387/' + res.public_id + '.jpg';
            return { item, response, status, headers };
        };
    }

    ngOnInit(): void {
        this.projectForm = this.fb.group({
            'title': [this.project.title, Validators.required],
            'description': [this.project.description, Validators.required]
        });
        this.finansalGoalForm = this.fb.group({
            'title': ['', Validators.required],
            'cost': ['', Validators.required]
        });
    }

    private addTag(control: FormControl) {
        if (control.value.length > 25) {
            return {
                'addTag': true
            };
        }
        return null;
    }

    load() {
        console.log('In load!');
        this.fileSelect.nativeElement.click();
    }

    upload() {
        console.log('In upload!');
        this.uploader.uploadAll();
    }

    drop() {
        this.closeBtn.nativeElement.click();
    }

    addGoal(form: FormGroup) {
        if (!form.valid) return;
        let goal: FinansalGoal = form.value;
        this.project.finansalGoals.push(goal);
        form.controls['title'].setValue("");
        form.controls['cost'].setValue("");
    }
}