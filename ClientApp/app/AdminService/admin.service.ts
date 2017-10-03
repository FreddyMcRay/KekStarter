import { Injectable } from "@angular/core";
import { User } from "../models/user.models";
import { Http } from "@angular/http";

@Injectable()
export class AdminService {

    constructor(private http: Http) {
    }

    deleteUsers(users: User[]) {
        return this.http.post('api/RemoveUser', users);
    }

    blockUsers(users: User[]) {
        return this.http.post('api/LockUserAccount', users);
    }

    unblockUsers(users: User[]) {
        return this.http.post('api/UnlockUserAccount', users);
    }

    getAllUsers() {
        return this.http.get('api/GetAllUsers');
    }

    confirmUser(id: string) {
        return this.http.get('api/ConfirmUser/' + id);
    }

    unConfirmUser(id: string) {
        return this.http.get('api/UnConfirmUser/' + id);
    }

}