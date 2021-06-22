import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; // this is needed!
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app.routing';
import { HttpClientModule } from "@angular/common/http";

//import { ComponentsModule } from './components/components.module';
// import { ExamplesModule } from './examples/examples.module';

import { AppComponent } from './app.component';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { MainComponent } from './main/main.component';
import { BookHeaderComponent } from './shared/book-header/book-header.component';

import { FilmModelComponent } from './admin/models/film-model/film-model.component';
import { MemberModelComponent } from './admin/models/member-model/member-model.component';
import { ShowingModelComponent } from './admin/models/showing-model/showing-model.component';
import { PurchaseModelComponent } from './admin/models/purchase-model/purchase-model.component';
import { AdminMainComponent } from './admin/admin-main/admin-main.component';
import { FilmFormComponent } from './admin/forms/film-form/film-form.component';
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
import { SigninComponent } from './auth/signin/signin.component';
import { SignupComponent } from './auth/signup/signup.component';
// import { ComponentsComponent } from './components/components.component';
// import { LoginComponent } from './examples/login/login.component';
// import { ComponentsModule } from './components/components.module';
import { RemovePurchaseComponent } from './remove-purchase/remove-purchase.component';

import { FilmService } from './GlobalServices/film-model.service';
import { MemberService } from './admin/models/member-model/member-model.service';
import { PurchaseService } from './GlobalServices/purchase.service';
import { ShowingService } from './GlobalServices/showing-model.service';
import { AuthService } from './auth/auth.service'
import { SeatsService } from './GlobalServices/seats.service';
import { appInitializer } from './GlobalServices/appInitializer';

@NgModule({
    declarations: [
        AppComponent,
        NavbarComponent,
        MainComponent,
        BookHeaderComponent,
        FilmModelComponent,
        MemberModelComponent,
        ShowingModelComponent,
        PurchaseModelComponent,
        AdminMainComponent,
        FilmFormComponent,
        MemberFormComponent,
        PurchaseFormComponent,
        ShowingFormComponent,
        AdminComponent,
        CarteleraComponent,
        ProyeccionCardComponent,
        ProjectionDetailsComponent,
        SetPurchaseComponent,
        UserComponent,
        UserPaymentComponent,
        SigninComponent,
        SignupComponent,
        RemovePurchaseComponent
        // ComponentsComponent,
        // LoginComponent
    ],
    imports: [
        BrowserAnimationsModule,
        NgbModule,
        FormsModule,
        RouterModule,
        AppRoutingModule,
        HttpClientModule
        // ComponentsModule
        // ExamplesModule
    ],
    providers: [FilmService, MemberService,PurchaseService, ShowingService, AuthService, SeatsService],
    bootstrap: [AppComponent]
})
export class AppModule { }
