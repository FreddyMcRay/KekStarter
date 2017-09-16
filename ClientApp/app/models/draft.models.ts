export class FinansalGoal {
    title: string;
    cost: number;

    constructor(title: string, cost: number) {
        this.title = title;
        this.cost = cost;
    }
}

export class Project {
    id: number;
    title: string;
    completionDate: Date;
    description: string;
    content = "";
    image = 'http://res.cloudinary.com/profunding/image/upload/v1504950919/default-bg.jpg';
    finansalGoals: FinansalGoal[] = [];
    totalCost = 0;
    userId: number;
    tags: string[] = []
}