import { Component, OnInit } from '@angular/core';
import { UserProject, HomeParseObject } from '../../models/project.models';
import { RestService } from '../../RestService/rest.service';
import { Language } from 'angular-l10n'; 
import 'rxjs/Rx';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
    @Language() lang;
    public projects: HomeParseObject;
    public success: UserProject[] = [];
    public newproj: UserProject[] = [];
    public tags: string[];

    constructor(private service: RestService) {
    }

    ngOnInit() {
        this.service.getProjectsHome().subscribe(result => {
            this.projects = result.json();
            this.success = this.projects.successfulProjects;
            this.newproj = this.projects.newProjects;
            this.tags = this.projects.tags;
            console.log(this.tags);
        });
    }
}
