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

@Component({
    selector: 'displayProject',
    templateUrl: './displayProject.component.html',
    styleUrls: ['./displayProject.component.css']
})
export class DisplayProjectComponent implements OnDestroy {
    id: number;
    private subscription: Subscription;
    parse: ProjectParseObject;
    currentUser: UserProfileMini;
    user: AuthUser = new AuthUser();
    project: UserProjectFull = new UserProjectFull();
    rating: number;
    guest: boolean = true;
    finansalGoalForm: FormGroup;
    preview: boolean = false;

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
                this.project.user = this.parse.creater;
                this.rating = this.project.userRating;
            })
        }
        this.finansalGoalForm = this.fb.group({
            'title': ['', Validators.required],
            'cost': ['', Validators.required]
        });
    }

    addGoal(form: FormGroup) {
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

    refactor() {
        this.router.navigate(['/draft']);
    }

    ngOnDestroy() {
        if (!(this.id == 0)) {
            this.service.addRatingToProject({ ProjectId: this.project.id.toString(), UserPofileId: this.user.id.toString(), Value: this.rating.toString() });
        }
        if (this.checkRole()) {

        }
    }
}