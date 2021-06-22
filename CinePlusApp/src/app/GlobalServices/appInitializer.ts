import { Router } from "@angular/router";
import { AuthService } from "app/auth/auth.service";
import { SeatsService } from "./seats.service";
import { ShowingService } from "./showing-model.service";

export function appInitializer(authService: AuthService) {

    return () => new Promise<void>((resolve, reject) => {
        authService.refreshToken()
            .subscribe((response) => {
                this.authService.user = response;
                this.authService.isAuthenticated = true;
                this.router.navigate(['/']);
            })
            .add(resolve);

    });
}