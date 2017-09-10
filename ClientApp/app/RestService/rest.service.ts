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
        this.http.post("api/Login", this.user)
            .subscribe(result => {
                console.log("after");
                console.log(result);
            });
    }

    public registration(username: string, email: string, name: string, password: string) {
        this.ruser.login = name;
        this.ruser.password = password;
        this.ruser.email = email;
        this.ruser.name = username;
        console.log(this.ruser);
        this.http.post("api/Register", this.ruser)
            .subscribe(result => {
                console.log("after");
                console.log(result);
            });
    }

    public getCurrentUser() {
        return this.http.get('api/getCurrentUser/');
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