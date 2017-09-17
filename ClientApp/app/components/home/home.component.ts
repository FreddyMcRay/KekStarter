import { Component, OnInit } from '@angular/core';
import { UserProject, HomeParseObject } from '../../models/project.models';
import { RestService } from '../../RestService/rest.service';
import 'rxjs/Rx';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
    parseObject: HomeParseObject;
    successProjects: UserProject[];
    newProjects: UserProject[];
    tags: string[];

    constructor(private service: RestService) {
    }

    ngOnInit() {
        this.service.getProjectsHome().subscribe(result => {
            this.parseObject = result.json();
        });
        this.successProjects = this.parseObject.successProjects;
        this.newProjects = this.parseObject.newProjects;
        this.tags = this.parseObject.tags;
    }
}
