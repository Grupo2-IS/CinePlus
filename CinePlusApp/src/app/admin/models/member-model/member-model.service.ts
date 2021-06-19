import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Member } from './member.model';

@Injectable()
export class MemberService {
  private dataPath = 'https://localhost:5001/api/members';

  constructor(private http: HttpClient) { }

  GetMember() {
    return this.http.get<Member[]>(this.dataPath);
  }

}