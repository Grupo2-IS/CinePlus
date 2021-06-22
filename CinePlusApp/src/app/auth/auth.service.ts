import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable()
export class AuthService {
    constructor(private http: HttpClient) { }

    singUpUser(name: string, password: string, password2: string) {
        return

    }

    isAuthenticated() {
        return true;
    }

    userRole() {
        return "admin";
    }

}