import { Component, Input } from '@angular/core';
import { Project } from '../../../models/draft.models';


@Component({
    selector: 'payway',
    templateUrl: './payway.component.html',
    styleUrls: ['./payway.component.css'],
})
export class PayWayComponent {
    @Input() project: Project;

}