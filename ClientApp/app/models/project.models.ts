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
    successfulProjects: UserProject[];
    newProjects: UserProject[];
    tags: string[];
}