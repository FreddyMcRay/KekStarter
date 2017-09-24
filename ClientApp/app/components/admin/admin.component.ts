import { Component, OnInit } from "@angular/core";
import { UserService } from "../../UserService/user.service";
import { User } from "../../models/user.models";
import { ConfirmationService, SelectItem } from "primeng/primeng";
import { AdminService } from "../../AdminService/admin.service";

@Component({
    selector: 'admin-component',
    templateUrl: './admin.component.html',
    styleUrls: ['./admin.component.css'],
})

export class AdminComponent implements OnInit {

    private users: User[];
    private selectedUsers: User[] = [];
    private roles: SelectItem[];
    private display = false;
    private currentConfirmUser: User = new User();


    constructor(private userService: UserService,
        private confirmationService: ConfirmationService,
        private adminService: AdminService) {
    }

    ngOnInit(): void {
        this.getAllUsers();
        this.roles = [];
        this.roles.push({ label: "All", value: null })
        this.roles.push({ label: 'Admin', value: 'Admin' });
        this.roles.push({ label: 'AuthUser', value: 'AuthUser' });
        this.roles.push({ label: 'User', value: 'User' });
    }

    getAllUsers() {
        //this.adminService.getAllUsers().subscribe(result => {
        //    this.users = result.json();
        //})
    }

    deleteUsers() {
        this.confirmationService.confirm({
            message: 'Do you want to delete ' + this.selectedUsers.length + ' users?',
            header: 'Delete users',
            icon: 'fa fa-trash',
            accept: () => {
                this.adminService.deleteUsers(this.selectedUsers)
            },
            reject: () => {
            }
        })
    }

    blockUsers() {
        this.adminService.blockUsers(this.selectedUsers)
    }

    unblockUsers() {
        this.adminService.unblockUsers(this.selectedUsers)
    }

    showConfirmWindow(user: User) {
        console.log(user);
        this.currentConfirmUser = user;
        this.display = true;
    }
}