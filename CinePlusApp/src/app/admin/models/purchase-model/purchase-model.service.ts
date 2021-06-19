import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Purchase } from './purchase-model.model';

@Injectable()
export class PurchaseService {
  private dataPath = 'https://localhost:5001/api/normalpurchases';
  private dataPath1 = 'https://localhost:5001/api/memberpurchases';

  constructor(private http: HttpClient) { }

 GetNormalPurchase() {
    return this.http.get<Purchase[]>(this.dataPath);

  }

  GetMemberPurchase() {
    return this.http.get<Purchase[]>(this.dataPath1);

  }

}