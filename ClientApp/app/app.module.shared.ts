import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { Ng2CloudinaryModule } from 'ng2-cloudinary';

import { AppComponent } from './components/app/app.component';
import { HomeComponent } from './components/home/home.component';
import { ProfileComponent } from "./components/profile/profile.component";
import { LoginComponent } from "./components/login/login.component";
import { RegistrationComponent } from "./components/registration/registration.component";
import { LinkedProjectsComponent } from './components/linkedProjects/linkedProjects.component';

import { RestService } from "./RestService/rest.service";
import { RoleService } from "./RoleService/role.service";

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        ProfileComponent,
        LoginComponent,
        RegistrationComponent,
        LinkedProjectsComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        Ng2CloudinaryModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'profile', component: ProfileComponent },
            { path: 'projects', component: LinkedProjectsComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [ RestService, RoleService]
})
export class AppModuleShared {
}
