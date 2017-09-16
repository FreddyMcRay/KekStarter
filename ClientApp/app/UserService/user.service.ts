import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { AuthUser } from '../models/user.models';
import 'rxjs/add/operator/map';


@Injectable()
export class UserService {
    constructor(private http: Http) {
    }

    getCurrentUser() {
        if (!(typeof localStorage === 'undefined')) {
            return JSON.parse(localStorage.getItem('currentUser') || '');
        }
    }

    public jwt() {
        let currentUser = JSON.parse(localStorage.getItem('currentUser')||'');
        if (currentUser && currentUser.token) {
            let headers = new Headers({ 'authorization': currentUser.token });
            return new RequestOptions({ headers: headers });
        }
    }
}