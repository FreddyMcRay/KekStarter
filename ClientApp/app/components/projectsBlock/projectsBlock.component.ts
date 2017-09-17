import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';
import { RestService } from '../../RestService/rest.service';
import { UserProject } from '../../models/project.models';


@Component({
    selector: 'projectsBlock',
    templateUrl: './projectsBlock.component.html',
    styleUrls: ['./projectsBlock.component.css']
})
export class ProjectsBlockComponent {
    projects: UserProject[];
    private defaultGetProject: string = "10";
    private defaultSkipProject: string = "0";
    public value: string;
    public type: string;
    public property: string;
    public subscription: Subscription;

    constructor(private service: RestService, private activateRoute: ActivatedRoute) {
        
    }

    ngOnInit() {
        this.subscription = this.activateRoute.params.subscribe(params => {
            this.type = params['type'];
            this.property = params['property'];
            this.value = params['value'];
            if (this.property == null)
                this.property = "all";
            this.service.getProjects(this.property, this.type, this.value, this.defaultGetProject, this.defaultSkipProject)
                .subscribe(result => {
                    this.projects = result.json();
                })
        });
    }
}
