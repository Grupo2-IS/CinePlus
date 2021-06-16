import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CarteleraComponent } from './cartelera/cartelera.component';
import { ProyeccionCardComponent } from './cartelera/proyeccion-card/proyeccion-card.component';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ProjectionDetailsComponent } from './projection-details/projection-details.component';
import { SetPurchaseComponent } from './set-purchase/set-purchase.component';



@NgModule({
  declarations: [CarteleraComponent,
    ProyeccionCardComponent,
    ProjectionDetailsComponent,
    SetPurchaseComponent],
  imports: [
    CommonModule,
    NgbModule
  ]
})
export class UserModule { }
