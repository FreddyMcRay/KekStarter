import { Component, Input } from '@angular/core';
import { UserProject } from '../../models/project.models';


@Component({
    selector: 'preview',
    templateUrl: './preview.component.html',
    styleUrls: ['./preview.component.css']
})
export class PreviewComponent {
    @Input() project: UserProject;

}