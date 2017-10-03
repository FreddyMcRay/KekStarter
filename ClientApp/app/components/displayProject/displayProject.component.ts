import { Component, OnDestroy } from '@angular/core';
import { RestService } from "../../RestService/rest.service";
import { Subscription } from 'rxjs/Subscription';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthUser } from '../../models/user.models';
import { UserProjectFull, ProjectParseObject } from '../../models/project.models';
import { UserProfileMini } from '../../models/user.models';
import { FinansalGoal } from '../../models/draft.models';
import { ProjectService } from '../../ProjectService/project.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MessageService } from '../../MessageService/message.service';
import { UserService } from '../../UserService/user.service';
import { Language } from 'angular-l10n';

@Component({
    selector: 'displayProject',
    templateUrl: './displayProject.component.html',
    styleUrls: ['./displayProject.component.css']
})
export class DisplayProjectComponent implements OnDestroy {
    @Language() lang;
    id: number;
    private subscription: Subscription;
    parse: ProjectParseObject;
    currentUser: UserProfileMini;
    user: AuthUser = new AuthUser();
    project: UserProjectFull = new UserProjectFull();
    rating: number;
    inputSum: number = 0;
    guest: boolean = true;
    finansalGoalForm: FormGroup;
    preview: boolean = false;
    ratingLock: boolean = true;

    public checkRole() {
        return (this.user.role == 'Admin' || this.user.id == this.project.user.id) ? true : false;
    }

    constructor(private service: RestService, private activateRoute: ActivatedRoute, private projectService: ProjectService, private router: Router,
        private fb: FormBuilder, private messageService: MessageService, private userService: UserService) {

        this.user = this.userService.getCurrentUser();
        if (this.user.role != 'Guest')
            this.guest = false;
        console.log(this.user);

        this.subscription = activateRoute.params.subscribe(params => this.id = params['id']);
        console.log(this.id);

        if (this.id == 0) {
            this.preview = true;
            this.project = this.projectService.getPreviewProject(this.project);
        } else {
            this.preview = false;
            this.service.getProjectById(this.id.toString(), this.user.id.toString()).subscribe(result => {
                this.parse = result.json();
                console.log(this.parse);
                this.project = this.parse.project;
                this.project.tags = this.parse.tags;
                this.project.goals = this.parse.finansalGoal;
                this.project.goals = this.project.goals.sort((a, b): number => { return (a.cost > b.cost) ? 1 : -1; });
                console.log(this.project.goals);
                this.project.user = this.parse.creater;
                this.rating = this.project.userRating;
                if (this.rating != 0)
                    this.ratingLock = false;
            })
        }
        this.finansalGoalForm = this.fb.group({
            'title': ['', Validators.required],
            'cost': ['', Validators.required]
        });
    }

    deleteGoal(goal: FinansalGoal) {
        for (let i = 0; i < this.project.goals.length; i++) {
            if (this.project.goals[i].title === goal.title && this.project.goals[i].cost === goal.cost)
                this.project.goals.splice(i, 1);
        }
    }

    public addGoal(form: FormGroup) {
        if (!form.valid) return;
        let goal: FinansalGoal = form.value;
        this.project.goals.push(goal);
        form.controls['title'].setValue("");
        form.controls['cost'].setValue("");
    }

    public followProject() {
        this.service.userFollowProject({ UserId: this.user.id, ProjectId: this.project.id })
            .subscribe(data => {
                this.project.followed = true;
                this.messageService.sendSuccessMessage('You follow this project');
            },
            error => {
                this.messageService.sendErrorMessage('Error with following');
            }
            )
    }

    public unFollowProject() {
        this.service.userUnFollowProject({ UserId: this.user.id, ProjectId: this.project.id })
            .subscribe(data => {
                this.project.followed = false;
                this.messageService.sendSuccessMessage('You unfollow this project')
            },
            error => {
                this.messageService.sendErrorMessage('Error with unfollowing');
            }
            )
    }

    public refactor() {
        this.router.navigate(['/draft']);
    }

    addRatingToProject() {
        console.log('addRatingToProject');
        if (!(this.id == 0) && this.ratingLock) {
            this.service.addRatingToProject({ ProjectId: this.project.id.toString(), UserPofileId: this.user.id.toString(), Value: this.rating.toString() })
                .subscribe(result => {
                    this.project.rating = result.json();
                    console.log(this.project.rating);
                    this.ratingLock = false;
                })
        }
    }

    ngOnDestroy() {
        if (this.checkRole()) {
            this.service.updateGoalsInProject({ projectId: this.project.id, goals: this.project.goals }).subscribe(result => {
                console.log(result.json());
                console.log('nice update goals');
            })
        }
    }

    public addPurchase() {
        if (this.inputSum == 0) {
            this.messageService.sendErrorMessage('Error with purchasing');
        } else {
            this.service.addPurchase({ projectId: this.project.id, userId: this.user.id, purchase: this.inputSum })
                .subscribe(result => {
                    this.messageService.sendSuccessMessage('Thank you,' + this.user.login);
                })
        }
    }
}