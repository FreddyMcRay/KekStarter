import { Component, OnInit, Input } from '@angular/core';
import { RestService } from '../../RestService/rest.service';
import { MessageService } from '../../MessageService/message.service';
import { ProjectNew } from '../../models/project.models';
import { AuthUser } from '../../models/user.models';
import { UserService } from '../../UserService/user.service';
import 'rxjs/Rx';

@Component({
    selector: 'news',
    templateUrl: './news.component.html',
    styleUrls: ['./newws.component.css']
})
export class NewsComponent implements OnInit {
    news: ProjectNew[] = [];
    oneNew: ProjectNew;
    content: string;
    @Input() projectId: number;
    AuthUser: AuthUser;

    constructor(private service: RestService, private userService: UserService) {
        this.AuthUser = this.userService.getCurrentUser();
    }

    ngOnInit() {
        this.getNewsFromServer();
    }

    getNewsFromServer() {
        console.log('getNewsFromServer')
        this.service.getNewsByProject(this.projectId.toString()).subscribe(result => {
            let arrNews = result.json();
            if (arrNews != null)
                this.news = this.news.concat(arrNews);
        });
    }

    sendNewOnServer() {
        this.service.sendCommentsOnServer({ projectid: this.projectId, userid: this.AuthUser.id, content: this.content })
            .subscribe(result => {
                this.oneNew = result.json();
                this.news.push(this.oneNew);
                this.content = "";
            });
    }
}
