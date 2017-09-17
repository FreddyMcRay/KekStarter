import { UserProfileMini } from './user.models';

export class UserProject {
    id: number;
    image: string;
    title: string;
    description: string;
    currentSum: string;
    progress: string;
    leftOver: string;
}

export class HomeParseObject {
    successProjects: UserProject[];
    newProjects: UserProject[];
    tags: string[];
}

export class Comment {
    id: number;
    dataCreated: number;
    userProfile: UserProfileMini;
    project: UserProject;
    content: string;
}