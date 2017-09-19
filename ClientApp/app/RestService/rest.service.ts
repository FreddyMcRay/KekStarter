import { Injectable } from '@angular/core';
import { Component } from '@angular/core';
import { Http, Headers, Response, Request, RequestOptions, RequestMethod } from '@angular/http';
import "rxjs/Rx";

@Component({
})
@Injectable()
export class RestService {

    constructor(private http: Http) { }
    public user: User = new User();
    public ruser: RUser = new RUser();

    public login(username: string, password: string) {
        this.user.login = username;
        this.user.password = password;
        console.log(this.user);
        return this.http.post("api/Login", this.user)
            .map((response: Response) => {
                let user = response.json();
                console.log("rest " + user);
                if (user && user.role != 'Guest') {
                    localStorage.setItem('currentUser', JSON.stringify(user));
                }
                return user;
            });
    }

    public registration(username: string, email: string, name: string, password: string) {
        this.ruser.login = name;
        this.ruser.password = password;
        this.ruser.email = email;
        this.ruser.name = username;
        console.log(this.ruser);
        return this.http.post("api/Register", this.ruser);

    }

    public getUserById(id: string) {
        return this.http.get('api/getUserById/' + id);
    }

    public editProfile(user: any) {
        console.log("UserServiceRest");
        console.log(user);
        this.http.post("api/EditProfile", user).subscribe(result => {
            console.log(result.json());
        });
    }

    public getProjectsHome() {
        return this.http.get('api/GetProjects');
    }

    public getProjects(property: string, type: string, value: string, take: string, skip: string) {
        return this.http.get('/api/getProjects/' + take + '/' + skip + '/' + property + '/' + type + '/' + value);
    }

    public getProjectById(id: string) {
        return this.http.get('/api/getProjectById/' + id);
    }

    public getCommentsByProject(take: string, skip: string, projectId: string) {
        return this.http.get('api/getCommentsByProjects' + '/' + projectId + '/' + skip + '/' + take);
    }

    public userFollowProject(userId: string, projectId: string) {
        return this.http.get('/api/followProject/' + userId + '/' + projectId);
    }

    public userUnFollowProject(userId: string, projectId: string) {
        return this.http.get('/api/unFollowProject/' + userId + '/' + projectId);
    }

    public addRatingToProject(rating: string) {
        this.http.get('/api/addRatingToProject/' + rating).subscribe(result => {
            console.log('nice rating');
        })
    }
    public sendCommentsOnServer(commentary: any) {
        this.http.post('api/addCommentInProject/', commentary).subscribe(result => {
            console.log("hey");
        });
    }

    public removeCommentInProject(commentary: any) {
        return this.http.post('api/removeCommentInProject/', commentary).subscribe(result => {
            console.log("commend is delete");
        });
    }
}

export class User {
    login: string;
    password: string;
}

export class RUser {
    name: string;
    email: string;
    login: string;
    password: string;
}
