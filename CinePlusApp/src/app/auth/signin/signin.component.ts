import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { RequestUser } from '../requestUser.model';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent implements OnInit {
  focus;
  focus1;
  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
  }

  onSignin(form: NgForm) {
    const user = form.value.name;
    const pass = form.value.password;
    const requestUser = new RequestUser(user, pass);
    this.authService.Authenticate(requestUser).subscribe(
      (response) => {
        this.authService.user = response;
        this.authService.isAuthenticated = true;
        this.router.navigate(['/']);
        if (response['status'] === '400') {
          alert("Usuario o contraseña incorrecto.");
        }
      },
      (err) => alert("Usuario o contraseña incorrecto.")
    )
  }

}
