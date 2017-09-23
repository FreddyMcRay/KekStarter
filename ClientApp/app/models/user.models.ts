import { UserProject } from './project.models';

export class AuthUser {
    id: number = 0;
    login: string = "";
    color: string = "light";
    language: string = "en";
    role: string = "Guest";
    token: string = "";
    onCheck: boolean = false;
}

export class UserProfile {
    id: number;
    firstName: string;
    secondName: string;
    urlPhoto: string = 'https://res.cloudinary.com/dbsjugefb/image/upload/w_250,h_250,c_thumb,r_max/v1505042128/anonim_user_vdzhx0.jpg';
    registrationDate: string;
    lastLogInDate: string;
    followedProjects: UserProject[];
    projects: UserProject[];
    achivments: UserAchivment[];
}

export class UserProfileMini {
    id: number;
    firstName: string;
    secondName: string;
    urlPhoto: string;
}

export class Achivment {
    id: number;
    name: string;
    urlImage: string;
    description: string;
}

export class UserAchivment {
    id: number;
    achivment: Achivment;
}