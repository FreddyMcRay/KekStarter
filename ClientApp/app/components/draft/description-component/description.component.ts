import { Component, Input} from '@angular/core';
import { Project } from '../../../models/draft.models';
import { Language } from 'angular-l10n';


@Component({
    selector: 'description-component',
    templateUrl: './description.component.html',
    styleUrls: ['./description.component.css'],
})
export class DescriptionComponent {
    @Language() lang;
    @Input() project: Project;
    @Input() invalid: boolean;

}