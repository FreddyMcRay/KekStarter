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
    yourComment: Comment = new Comment;
   // @Input() userProfile: UserProfileMini;
   // @Input() projectId: number;
    take: string = "10";
    skip: string;
    AuthUser: AuthUser;

    //public checkRole(): boolean {
    //    this.AuthUser = this.userService.getCurrentUser();
    //    return (this.AuthUser.role == 'Admin' || (this.AuthUser.id == this.userProfile.id && this.AuthUser.role != 'Guest')) ? true : false;
    //}

    //public checkRoleForDeleteComment(i: number): boolean {
    //    this.AuthUser = this.userService.getCurrentUser();
    //    return (this.AuthUser.role == 'Admin' || this.AuthUser.id == this.comments[i].userProfile.id) ? true : false;
    //}

    constructor(private service: RestService, private userService: UserService) {

    }

    //ngOnInit() {
    //    this.getCommentsFromServer();   
    //}

    //getCommentsFromServer() {
    //    this.service.getCommentsByProject(this.take, this.comments.length.toString(), this.projectId.toString()).subscribe(result => {
    //        console.log("GetCommentresult= " + result.json());
    //        let arrComments = result.json();
    //        if (arrComments != null)
    //            this.comments = this.comments.concat(arrComments);
    //    });
    //}

    //addComment() {
    //    let comment = this.createComment();
    //    this.comments.push(comment);
    //    this.sendCommentOnServer(comment);
    //}

    //createComment(): Comment {
    //    let comment = new Comment();
    //    comment.dataCreated = Date.now();
    //    comment.userProfile = this.userProfile;
    //    comment.content = this.yourComment.content;
    //    comment.project = new UserProject();
    //    comment.project.id = this.projectId;
    //    this.yourComment.content = "";
    //    return comment;
    //}

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