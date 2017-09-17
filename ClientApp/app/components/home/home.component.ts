import { Component } from '@angular/core';
import { UserProject } from '../../models/project.models';
import { Http, Headers, Response, Request, RequestOptions, RequestMethod } from '@angular/http';
import { RestService } from "../../RestService/rest.service";

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent {
    public projects: ProjectList;
    public success: UserProject[];
    public newproj: UserProject[];

    constructor(private http: Http, private service: RestService) {
        this.service.getProjectsHome().subscribe(result => {
            this.projects = result.json();
            console.log(this.projects);
            this.success = this.projects.successfulProjects;
            this.newproj = this.projects.newProjects;
            console.log(this.success);
            console.log(this.newproj);
         });
    }
}

export class ProjectList {
    successfulProjects: UserProject[];
    newProjects: UserProject[];
}
