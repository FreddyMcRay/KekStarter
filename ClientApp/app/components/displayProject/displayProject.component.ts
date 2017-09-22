import { Component, OnDestroy } from '@angular/core';
import { RestService } from "../../RestService/rest.service";
import { Subscription } from 'rxjs/Subscription';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthUser } from '../../models/user.models';
import { UserProjectFull, ProjectParseObject } from '../../models/project.models';
import { UserProfileMini } from '../../models/user.models';
import { Project, FinansalGoal } from '../../models/draft.models';
import { ProjectService } from '../../ProjectService/project.service';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";

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
    previewProject: Project;
    rating: number;
    guest: boolean = true;
    finansalGoalForm: FormGroup;
    creater: UserProfileMini = new UserProfileMini();
    tags: string[];
    targets: FinansalGoal[] = [];
    preview: boolean = false;

    public checkRole() {
        return (this.user.role == 'Admin' || this.user.id == this.creater.id) ? true : false;
    }

    constructor(private service: RestService, private activateRoute: ActivatedRoute, private projectService: ProjectService, private router: Router,
        private fb: FormBuilder) {
        if (!(typeof localStorage === "undefined")) {
            this.guest = false;
            this.user = JSON.parse(localStorage.getItem('currentUser'));
        }
        console.log(this.user);
        this.subscription = activateRoute.params.subscribe(params => this.id = params['id']);
        console.log(this.id);
        if (this.id == 0) {
            this.preview = true;
            this.getPreviewProject();
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
                this.tags = this.project.tags;
                this.targets = this.project.goals;
                console.log(this.targets);
                console.log(this.tags);
                this.creater = this.project.user;
                console.log(this.creater);
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
        this.targets.push(goal);
        form.controls['title'].setValue("");
        form.controls['cost'].setValue("");
    }

    public getPreviewProject() {
        this.previewProject = this.projectService.getDraft();
        this.previewProject = this.projectService.addTags(this.previewProject);
        this.project.title = this.previewProject.title;
        this.project.dateEnd = this.previewProject.completionDate.toDateString().split('T')[0];
        this.project.content = this.previewProject.content;
        this.project.requiredSum = this.previewProject.totalCost;
        this.project.goals = this.previewProject.finansalGoals;
        this.project.description = this.previewProject.description;
        this.project.image = this.previewProject.image;
        this.project.sponsors = 0;
        this.project.currentSum = 0;
        this.project.progress = '0';
        this.project.tags = this.previewProject.tags;
        this.project.followed = false;
        this.project.leftOver = 0;
    }

    public followProject() {
        this.service.userFollowProject({ UserId: this.user.id, ProjectId: this.project.id })
            .subscribe(data => { this.project.followed = true },
            error => { }
            )
    }

    public unFollowProject() {
        this.service.userUnFollowProject({ UserId: this.user.id, ProjectId: this.project.id })
            .subscribe(data => { this.project.followed = false },
            error => { }
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