﻿import { Injectable } from '@angular/core';
import { Project } from "../models/draft.models";
import { Http, Response } from "@angular/http";
import { UserService } from "../UserService/user.service";
import 'rxjs/add/operator/map';


@Injectable()
export class ProjectService {

    constructor(private http: Http,
        private userService: UserService) {
    }

    saveDraft(project: Project) {
        localStorage.setItem('draft', JSON.stringify(project));
        localStorage.setItem('completionDate', JSON.stringify(project.completionDate));
    }

    getDraft() {
        let project = JSON.parse(localStorage.getItem('draft'));
        if (!(typeof localStorage.getItem('completionDate') === 'undefined')) {
            let completionDate = JSON.parse(localStorage.getItem('completionDate'))
            if (completionDate && project)
                project.completionDate = new Date(completionDate);
        } else {
            project.completionDate = new Date();
        }
        return project ? project : new Project();
    }

    removeDraft() {
        if (localStorage.getItem('draft')) {
            localStorage.getItem('completionDate');
            localStorage.removeItem('draft');
        }
    }

    isValid(project: Project) {
        let now = new Date();
        if (typeof project.title === "undefined" ||
            project.title === "" ||
            typeof project.description === "undefined" ||
            project.description === "" ||
            project.finansalGoals.length === 0 ||
            project.tags.length === 0 ||
            typeof project.completionDate === "undefined" ||
            project.completionDate < now) {
            return false;
        }
        return true;
    }


    create(project: Project) {
        return this.http.post('api/CreateProject', project);
    }

    addTags(project: Project): Project {
        let tags: any[] = [];
        tags = project.tags;
        let ta: string[] = [];
        for (let tag1 of tags) {
            ta.push(tag1.value);
        }
        project.tags = ta;
        console.log(project.tags);
        return project;
    }
}