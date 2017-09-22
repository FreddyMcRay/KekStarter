import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FinansalGoal } from "../../models/draft.models";

@Component({
    selector: 'finansal-goal',
    templateUrl: './finansal-goal.component.html',
    styleUrls: ['./finansal-goal.component.css']
})
export class FinansalGoalComponent {
    @Input() finansalGoal: FinansalGoal;
    @Input() editable: Boolean;
    @Output() removeFinansalGoal = new EventEmitter<FinansalGoal>()

    deleteGoal() {
        this.removeFinansalGoal.emit(this.finansalGoal);
    }

}