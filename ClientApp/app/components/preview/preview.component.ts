import { Component, Input } from '@angular/core';



@Component({
    selector: 'preview',
    templateUrl: './preview.component.html',
    styleUrls: ['./preview.component.css']
})
export class PreviewComponent {
    @Input() project: UserProject;

}

class UserProject {
    id: number;
    urlImage: string;
    title: string;
    description: string;
    currentSum: string;
    leftOver: string;
}