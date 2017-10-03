import { Injectable } from '@angular/core';
import { Component } from '@angular/core';
import { Http, Headers, Response, Request, RequestOptions, RequestMethod } from '@angular/http';
import "rxjs/Rx";

@Component({
})
@Injectable()
export class RestService {

    constructor(private http: Http) { }

    public login(model: any) {
        return this.http.post("api/Login", model)
            .map((response: Response) => {
                let user = response.json();
                console.log("rest " + user);
                if (user && user.role != 'Guest') {
                    localStorage.setItem('currentUser', JSON.stringify(user));
                }
                return user;
            });
    }

    public registration(model: any) {
        return this.http.post("api/Register", model);

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

    public getProjectById(id: string, userId: string) {
        return this.http.get('/api/getProjectById/' + id + '/' + userId);
    }

    public getCommentsByProject(projectId: string) {
        return this.http.get('api/getCommentsByProjects' + '/' + projectId);
    }

    public userFollowProject(followInfo: any) {
        return this.http.post('/api/FollowProject', followInfo);
    }

    public userUnFollowProject(followInfo: any) {
        return this.http.post('/api/UnFollowProject', followInfo);
    }

    public addRatingToProject(rating: any) {
        return this.http.post('/api/AddRating/', rating);
    }
    public sendCommentsOnServer(commentary: any) {
        return this.http.post('api/addCommentInProject/', commentary);
    }

    public removeCommentInProject(commentary: any) {
        return this.http.post('api/removeCommentInProject/', commentary).subscribe(result => {
            console.log("commend is delete");
        });
    }

    public logOut() {
        console.log('anal');
        return this.http.get('api/logOut');
    }

    public sendConfirmInfoToServer(info: any) {
        return this.http.post('api/sendConfirm', info);
    }

    public updateGoalsInProject(obj: any) {
        return this.http.post('api/UpdateGoals', obj);
    }

    public getNewsByProject(projectId: string) {
        return this.http.get('api/' + projectId);
    }

    public addPurchase(purchase: any) {
        return this.http.post('api/donateInProject', purchase);
    }
}
