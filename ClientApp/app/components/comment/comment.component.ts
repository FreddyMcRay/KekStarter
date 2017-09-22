import { RestService } from '../../RestService/rest.service';
import { Component, OnInit } from '@angular/core';
import { Input } from '@angular/core';
import { Comment } from '../../models/project.models';
import { UserProfileMini, AuthUser } from '../../models/user.models';
import { UserService } from '../../UserService/user.service';
import { UserProject } from '../../models/project.models';

@Component({
    selector: 'comments',
    templateUrl: 'comment.component.html',
    styleUrls: ['comment.component.css'],
    providers: [RestService, UserService],
})

export class CommentComponent {
    comments: Comment[] = [];
    yourComment: Comment = new Comment();
    @Input() userProfile: UserProfileMini;
    @Input() projectId: number;
    take: string = "10";
    skip: string;
    AuthUser: AuthUser;

    

    public checkRole(): boolean {
        this.AuthUser = this.userService.getCurrentUser();
        return (this.AuthUser.role == 'Admin' || (this.AuthUser.id == this.userProfile.id && this.AuthUser.role != 'Guest')) ? true : false;
    }

    public checkRoleForDeleteComment(i: number): boolean {
        this.AuthUser = this.userService.getCurrentUser();
        return (this.AuthUser.role == 'Admin' || this.AuthUser.id == this.comments[i].userProfile.id) ? true : false;
    }

    constructor(private service: RestService, private userService: UserService) {
        this.comments = [{
            id: 1, dataCreated: '21.09.2017', userProfile: { id: 1, firstName: 'Andrey', secondName: 'Repkovcki', urlPhoto: 'http://res.cloudinary.com/dbsjugefb/image/upload/v1505769861/syqcdp8w55xgzvpyx0pm.jpg' },
            projectId: 2002, content: 'lknwlkfklmn lkmneklrv w klmnlkevlkn klwnerkb k oik'
        }]

    }

    ngOnInit() {
        this.getCommentsFromServer();   
    }

    getCommentsFromServer() {
        this.service.getCommentsByProject(this.take, this.comments.length.toString(), this.projectId.toString()).subscribe(result => {
            console.log("GetCommentresult= " + result.json());
            let arrComments = result.json();
            if (arrComments != null)
                this.comments = this.comments.concat(arrComments);
        });
    }

    addcomment() {
        let comment = this.createcomment();
        console.log("Add comment");
        this.comments.push(comment);
        this.sendCommentOnServer(comment);
    }

    createcomment(): Comment {
        let comment = new Comment();
        comment.dataCreated = Date.now().toString();
        comment.userProfile = this.userProfile;
        comment.content = this.yourComment.content;
        comment.projectId = this.projectId;
        this.yourComment.content = "";
        return comment;
    }

    sendCommentOnServer(comment: Comment) {
        console.log(comment);
        this.service.sendCommentsOnServer(comment);
    }



    deleteComment(i: number): void {
        let comment = this.comments[i];
        this.service.removeCommentInProject(comment);
        this.comments.splice(i, 1);
    }
}