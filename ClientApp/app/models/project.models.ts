import { UserProfileMini } from './user.models';
import { FinansalGoal } from './draft.models';

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

export class ProjectParseObject {
    project: UserProjectFull;
    tags: string[];
    finansalGoal: FinansalGoal[];
    creater: UserProfileMini;
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
    goals: FinansalGoal[] = [];
}

export class Comment {
    id: number;
    dataCreated: string;
    userProfile: UserProfileMini;
    projectId: number;
    content: string;
}