import { Component } from '@angular/core';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent {
    projec: UserProject = {
        id: 1, urlImage: 'http://res.cloudinary.com/profunding/image/upload/v1504950919/default-bg.jpg',
        title: 'Sasay project', description: 'This is sasay project. So, you need to sasay', currentSum: 200, requiredSum: 1000, leftOver: '40', progress: 20
    };
    projects: UserProject[] = [this.projec, this.projec, this.projec, this.projec];
}

class UserProject {
    id: number;
    urlImage: string;
    title: string;
    description: string;
    currentSum: number;
    requiredSum: number;
    leftOver: string;
    progress: number;// = (this.requiredSum / this.currentSum) * 100;
}
