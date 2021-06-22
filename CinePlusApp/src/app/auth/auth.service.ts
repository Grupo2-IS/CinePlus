import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "environments/environment";
import { RequestUser } from "./requestUser.model";
import { User } from "./user.model";

@Injectable()
export class AuthService {
    constructor(private http: HttpClient) { }
    isAuthenticated: boolean = false;
    user: User = null;
    singUpUser(name: string, password: string, password2: string) {
        return

    }

    Authenticate(user: RequestUser) {
        return this.http.post<User>(environment.apiUrl + '/users/authenticate', user, { withCredentials: true });
    }

    userRole() {
        if (this.isAuthenticated) {
            return this.user.role;
        }
        return ""
    }

}