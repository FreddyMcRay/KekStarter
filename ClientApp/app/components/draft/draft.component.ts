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
    tags: any[] = [];

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

    send() {
        this.addTags();
        if (!this.projectService.isValid(this.project)) {
            this.invalid = true;
            this.saveDraft();
            this.begin.nativeElement.click();
        } else {
            console.log(this.project);
            this.projectService.create(this.project).subscribe(
                data => {
                    console.log("OK");
                    console.log(data);
                },
                error => console.log(error)
            );
        }

    }

    addTags() {
        this.tags = this.project.tags;
        let ta: string[] = [];
        for (let tag1 of this.tags) {
            ta.push(tag1.value);
        }
        this.project.tags = ta;
        console.log(this.project.tags);
    }
}