import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { Ng2CloudinaryModule } from 'ng2-cloudinary';
import { FileUploadModule } from 'ng2-file-upload';
import { CalendarModule } from 'primeng/primeng';
import { InputTextModule } from 'primeng/primeng';
import { InputTextareaModule } from 'primeng/primeng';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Ng2FileDropModule } from 'ng2-file-drop';
import { EditorModule } from 'primeng/primeng';
import { TagInputModule } from 'ng2-tag-input';
import { RatingModule } from "ng2-rating";
import { Angular2FontawesomeModule } from 'angular2-fontawesome/angular2-fontawesome';
import { GrowlModule } from 'primeng/primeng';

import { DraftComponent } from './components/draft/draft.component';
import { GeneralInfoComponent } from './components/draft/general-info-component/general-info.component';
import { AppComponent } from './components/app/app.component';
import { HomeComponent } from './components/home/home.component';
import { ProfileComponent } from "./components/profile/profile.component";
import { LoginComponent } from "./components/login/login.component";
import { RegistrationComponent } from "./components/registration/registration.component";
import { PreviewComponent } from './components/preview/preview.component';
import { DragAndDropComponent } from './components/drag-and-drop/drag-and-drop.component';
import { FinansalGoalComponent } from './components/financial-goal/finansal-goal.component';
import { DescriptionComponent } from './components/draft/description-component/description.component';
import { ProjectsBlockComponent } from './components/projectsBlock/projectsBlock.component';
import { DisplayProjectComponent } from './components/displayProject/displayProject.component';
import { CommentComponent } from './components/comment/comment.component';
import { PayWayComponent } from './components/draft/payway/payway.component';

import { RestService } from "./RestService/rest.service";
import { RoleService } from "./RoleService/role.service";
import { ProjectService } from "./ProjectService/project.service";
import { UserService } from "./UserService/user.service";


@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        ProfileComponent,
        LoginComponent,
        RegistrationComponent,
        PreviewComponent,
        DraftComponent,
        GeneralInfoComponent,
        DragAndDropComponent,
        FinansalGoalComponent,
        DescriptionComponent,
        ProjectsBlockComponent,
        DisplayProjectComponent,
        CommentComponent,
        PayWayComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        BrowserAnimationsModule,
        FormsModule,
        Ng2CloudinaryModule,
        FileUploadModule,
        ReactiveFormsModule,
        CalendarModule,
        InputTextModule,
        InputTextareaModule,
        Ng2FileDropModule,
        EditorModule,
        TagInputModule,
        RatingModule,
        GrowlModule,
        Angular2FontawesomeModule,
        RouterModule.forRoot([
            { path: 'project/:id', component: DisplayProjectComponent },
            { path: 'draft', component: DraftComponent },
            { path: 'projects', component: ProjectsBlockComponent },
            { path: 'projects/:property/:type/:value', component: ProjectsBlockComponent, data: { title: 'Projects' } },
            { path: 'projects/:type/:value', component: ProjectsBlockComponent },
            { path: 'projects/:property', component: ProjectsBlockComponent, data: { title: 'Projects' } },
            { path: 'projects/:property/:type', component: ProjectsBlockComponent, data: { title: 'Projects' } },
            { path: 'profile/:id', component: ProfileComponent },
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent, data: { title: 'Home' } },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [RestService, RoleService, ProjectService, UserService]
})
export class AppModuleShared {
}
