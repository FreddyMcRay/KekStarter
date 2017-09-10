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

    public registration(name: string, email: string, username: string, password: string) {
        return this.http.post("", JSON.stringify({ name: name, email: email, username: username, password: password }))
            .map(result => { console.log(result.json()); });
    }

    public getCurrentUser() {
        return this.http.get('api/getCurrentUser/');
    }
}

export class User {
    login: string;
    password: string;
}