import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  data: Date = new Date();
  focus;
  focus1;
  constructor() { }

  ngOnInit(): void {
  }

  onSignup(form: NgForm) {
    const name: string = form.value.name;
    const email: string = form.value.email;
    const pass: string = form.value.password;
    const pass2: string = form.value.password2;
    const msg: string = pass === pass2 ? "Your password match" : "You typed your password wrong."
    alert(name + '\n' + email + '\n' + msg);
  }

}
