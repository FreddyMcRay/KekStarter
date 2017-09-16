import { Component, ElementRef, ViewChild } from '@angular/core';
import { Router } from "@angular/router";
import { Project, FinansalGoal } from '../../models/draft.models';
import { ProjectService } from '../../ProjectService/project.service';
import { AuthUser } from '../../models/user.models';
import { UserService } from '../../UserService/user.service';
import "rxjs/Rx";

@Component({
    selector: 'app-draft',
    templateUrl: './draft.component.html',
    styleUrls: ['./draft.component.css'],
})
export class DraftComponent {
    @ViewChild('begin') begin: ElementRef;
    project: Project;
    invalid = false;

    constructor(private projectService: ProjectService, private userService: UserService) {
        this.project = new Project();
    }

    deleteGoal(goal: FinansalGoal) {
        for (let i = 0; i < this.project.finansalGoals.length; i++) {
            if (this.project.finansalGoals[i].title === goal.title && this.project.finansalGoals[i].cost === goal.cost)
                this.project.finansalGoals.splice(i, 1);
        }
    }

    saveDraft() {
        this.projectService.saveDraft(this.project);
    }

    removeDraft() {
        this.projectService.removeDraft();
    }
}