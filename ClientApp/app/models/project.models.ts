import { UserProfileMini } from './user.models';

export class UserProject {
    id: number;
    image: string;
    title: string;
    description: string;
    currentSum: string;
    progress: number;
    leftOver: string;
    requiredSum: number;
}

export class HomeParseObject {
    successfulProjects: UserProject[];
    newProjects: UserProject[];
    tags: string[];
}

export class UserProjectFull {
    id: number;
    image: string;
    title: string;
    sponsors: number;
    currentSum: number;
    requiredSum: number;
    description: string;
    content: string;
    leftOver: number;
    dateEnd: string;
    progress: string;
    status: boolean;
    rating: number;
    userRating: number;
    followed: boolean = false;
    user: UserProfileMini;
    tags: string[];
}

export class Comment {
    id: number;
    dataCreated: string;
    userProfile: UserProfileMini;
    projectId: number;
    content: string;
}