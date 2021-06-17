import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent implements OnInit {
  data: Date = new Date();
  focus;
  focus1;
  constructor() { }

  ngOnInit(): void {
  }

  onSignin(form: NgForm) {
    const user = form.value.name;
    const pass = form.value.password;
    alert("Hola " + user);
  }

}
