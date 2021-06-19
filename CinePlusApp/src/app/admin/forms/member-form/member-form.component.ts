import { Component, OnInit } from '@angular/core';
import {NgForm} from '@angular/forms';

@Component({
  selector: 'app-member-form',
  templateUrl: './member-form.component.html',
  styleUrls: ['./member-form.component.css']
})
export class MemberFormComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  onSignin(form: NgForm) {
    const name = form.value.name;
    const nick = form.value.nick;
    const level = form.value.level;
    const role = form.value.role;
    const password = form.value.password;

  }

}
