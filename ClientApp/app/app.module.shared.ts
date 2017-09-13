import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { Ng2CloudinaryModule } from 'ng2-cloudinary';
import { FileUploadModule } from 'ng2-file-upload';

import { AppComponent } from './components/app/app.component';
import { HomeComponent } from './components/home/home.component';
import { ProfileComponent } from "./components/profile/profile.component";
import { LoginComponent } from "./components/login/login.component";
import { RegistrationComponent } from "./components/registration/registration.component";
import { AlertComponent } from './components/alert/alert.component';

import { RestService } from "./RestService/rest.service";
import { RoleService } from "./RoleService/role.service";
import { AlertService } from "./AlertService/alert.service";

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        ProfileComponent,
        LoginComponent,
        RegistrationComponent,
        AlertComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        Ng2CloudinaryModule,
        FileUploadModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'profile/:id', component: ProfileComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [RestService, RoleService, AlertService]
})
export class AppModuleShared {
}
