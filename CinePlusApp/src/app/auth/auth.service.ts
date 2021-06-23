import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "environments/environment";
import { RequestUser } from "./requestUser.model";
import { Role } from "./role";
import { User } from "./user.model";

@Injectable()
export class AuthService {
    constructor(private http: HttpClient) { }
    isAuthenticated: boolean = environment.production;
    user: User = null;
    // user = new User(1, 'admin', 'admin', Role.Admin);
    singUpUser(name: string, password: string, password2: string) {
        return

    }

    Authenticate(user: RequestUser) {
        return this.http.post<User>(environment.apiUrl + '/users/authenticate', user, { withCredentials: true });
    }

    refreshToken() {
        return this.http.post(environment.apiUrl + '/users/refresh-token', {}, { withCredentials: true });
    }

    userRole() {
        if (this.isAuthenticated) {
            return this.user.role;
        }
        return ""
    }

}