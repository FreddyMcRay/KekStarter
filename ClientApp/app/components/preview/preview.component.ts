import { Component, Input, OnInit } from '@angular/core';
import { UserProject } from '../../models/project.models';


@Component({
    selector: 'preview',
    templateUrl: './preview.component.html',
    styleUrls: ['./preview.component.css']
})
export class PreviewComponent implements OnInit {
    @Input() project: UserProject;
    progress: number;

    ngOnInit() {
        this.progress = this.project.progress;
    }
}