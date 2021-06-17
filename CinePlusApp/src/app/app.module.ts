import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; // this is needed!
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app.routing';
//import { ComponentsModule } from './components/components.module';
import { ExamplesModule } from './examples/examples.module';

import { AppComponent } from './app.component';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { MainComponent } from './main/main.component';
import { BookHeaderComponent } from './shared/book-header/book-header.component';

import { ArtistModelComponent } from './admin/models/artist-model/artist-model.component';
import { FilmModelComponent } from './admin/models/film-model/film-model.component';
import { MemberModelComponent } from './admin/models/member-model/member-model.component';
import { ShowingModelComponent } from './admin/models/showing-model/showing-model.component';
import { PurchaseModelComponent } from './admin/models/purchase-model/purchase-model.component';
import { AdminMainComponent } from './admin/admin-main/admin-main.component';
import { FilmFormComponent } from './admin/forms/film-form/film-form.component';
import { ArtistFormComponent } from './admin/forms/artist-form/artist-form.component';
import { MemberFormComponent } from './admin/forms/member-form/member-form.component';
import { PurchaseFormComponent } from './admin/forms/purchase-form/purchase-form.component';
import { ShowingFormComponent } from './admin/forms/showing-form/showing-form.component';
import { AdminComponent } from './admin/admin.component';


import { CarteleraComponent } from './user/cartelera/cartelera.component';
import { ProyeccionCardComponent } from './user/cartelera/proyeccion-card/proyeccion-card.component';
import { UserComponent } from './user/user.component';
import { ProjectionDetailsComponent } from './user/projection-details/projection-details.component';
import { SetPurchaseComponent } from './user/set-purchase/set-purchase.component';
import { UserPaymentComponent } from './user/user-payment/user-payment.component';


@NgModule({
    declarations: [
        AppComponent,
        NavbarComponent,
        MainComponent,
        BookHeaderComponent,
        ArtistModelComponent,
        FilmModelComponent,
        MemberModelComponent,
        ShowingModelComponent,
        PurchaseModelComponent,
        AdminMainComponent,
        FilmFormComponent,
        ArtistFormComponent,
        MemberFormComponent,
        PurchaseFormComponent,
        ShowingFormComponent,
        AdminComponent,
        CarteleraComponent,
        ProyeccionCardComponent,
        ProjectionDetailsComponent,
        SetPurchaseComponent,
        UserComponent,
        UserPaymentComponent
    ],
    imports: [
        BrowserAnimationsModule,
        NgbModule,
        FormsModule,
        RouterModule,
        AppRoutingModule,
        //ComponentsModule,
        ExamplesModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
