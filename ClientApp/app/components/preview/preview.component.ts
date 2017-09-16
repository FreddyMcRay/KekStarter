import { Component, Input } from '@angular/core';
import { Project } from '../../models/draft.models';


@Component({
    selector: 'preview',
    templateUrl: './preview.component.html',
    styleUrls: ['./preview.component.css']
})
export class PreviewComponent {
    @Input() project: Project;

}