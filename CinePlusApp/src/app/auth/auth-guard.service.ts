import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from "@angular/router";
import { environment } from "environments/environment";
import { AuthService } from "./auth.service";

@Injectable()
export class AuthGard implements CanActivate {

    constructor(private authService: AuthService, private router: Router) { }
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if (!environment.production) {
            return true;
        }
        if (!this.authService.isAuthenticated) {
            this.router.navigate(['/signin']);
            return false;
        }
        else if (route.data.roles && route.data.roles.indexOf(this.authService.userRole()) === -1) {
            this.router.navigate(['/']);
            return false;
        }
        return true;
    }
}