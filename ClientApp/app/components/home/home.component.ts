import { Component } from '@angular/core';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent {
    projec: UserProject = {
        id: 1, urlImage: 'http://res.cloudinary.com/profunding/image/upload/v1504950919/default-bg.jpg',
        title: 'Sasay project', description: 'This is sasay project. So, you need to sasay', currentSum: '200', leftOver: '40'
    };
    projects: UserProject[] = [this.projec, this.projec, this.projec, this.projec];
}

class UserProject {
    id: number;
    urlImage: string;
    title: string;
    description: string;
    currentSum: string;
    leftOver: string;
}
