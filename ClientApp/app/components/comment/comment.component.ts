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
    yourComment: Comment;
    content: string;
    @Input() projectId: number;
    AuthUser: AuthUser;

    public checkrolefordeletecomment(i: number): boolean {
        return (this.AuthUser.role == 'Admin' || this.AuthUser.id == this.comments[i].userProfile.id) ? true : false;
    }

    constructor(private service: RestService, private userService: UserService) {
      
        this.AuthUser = this.userService.getCurrentUser();
    }

    //ngOnInit() {
    //    this.getCommentsFromServer();   
    //}

    //getCommentsFromServer() {
    //    this.service.getCommentsByProject(this.projectId.toString()).subscribe(result => {
    //        console.log("GetCommentresult= " + result.json());
    //        let arrComments = result.json();
    //        if (arrComments != null)
    //            this.comments = this.comments.concat(arrComments);
    //    });
    //}

    //sendcommentonserver() {
    //    this.service.sendCommentsOnServer({ projectid: this.projectId, userid: this.AuthUser.id, content: this.content })
    //        .subscribe(result => {
    //            this.yourComment = result.json();
    //            this.comments.push(this.yourComment)
    //        });
    //}



    //deletecomment(i: number): void {
    //    let comment = this.comments[i];
    //    this.service.removeCommentInProject(comment);
    //    this.comments.splice(i, 1);
    //}
}