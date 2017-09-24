import { RestService } from '../../RestService/rest.service';
import { Component, OnInit } from '@angular/core';
import { Input } from '@angular/core';
import { Comment } from '../../models/project.models';
import { UserProfileMini, AuthUser } from '../../models/user.models';
import { UserService } from '../../UserService/user.service';
import { UserProject } from '../../models/project.models';
import { Language } from 'angular-l10n';

@Component({
    selector: 'comments',
    templateUrl: 'comment.component.html',
    styleUrls: ['comment.component.css'],
    providers: [RestService, UserService],
})

export class CommentComponent {
    @Language() lang;
    comments: Comment[] = [];
    yourComment: Comment;
    content: string;
    @Input() projectId: number;
    AuthUser: AuthUser;

    public checkroleForDeleteComment(i: number): boolean {
        return (this.AuthUser.role == 'Admin' || this.AuthUser.id == this.comments[i].userProfile.id) ? true : false;
    }

    constructor(private service: RestService, private userService: UserService) {
      
        this.AuthUser = this.userService.getCurrentUser();
    }

    ngOnInit() {
        this.getCommentsFromServer();          
    }

    getCommentsFromServer() {
        console.log('getCommentsFromServer')
        this.service.getCommentsByProject(this.projectId.toString()).subscribe(result => {
            let arrComments = result.json();
            if (arrComments != null)
                this.comments = this.comments.concat(arrComments);
        });
    }

    sendCommentOnServer() {
        this.service.sendCommentsOnServer({ projectid: this.projectId, userid: this.AuthUser.id, content: this.content })
            .subscribe(result => {
                this.yourComment = result.json();
                this.comments.push(this.yourComment);
                this.content = "";
            });
    }

    deleteComment(i: number): void {
        let comment = this.comments[i];
        this.service.removeCommentInProject({ projectid: this.projectId, userid: this.AuthUser.id, createUser: comment.userProfile.id, id: comment.id });

        this.comments.splice(i, 1);
    }
}