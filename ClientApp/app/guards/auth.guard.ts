import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthUser } from '../models/user.models';
import { UserService } from '../UserService/user.service';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private router: Router, private userService: UserService) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let user: AuthUser = this.userService.getCurrentUser();
        if ( user.role == 'AuthUser' || user.role == 'Admin') {
            return true;
        }
        this.router.navigate(['/home'], { queryParams: { returnUrl: state.url } });
        return false;
    }
}