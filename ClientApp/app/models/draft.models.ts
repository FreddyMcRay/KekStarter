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
    payment: Payment;
    userId: number;
    tags: string[] = []
}

export class Payment {
    cardNumber: string;
    expirationDate: string;
    cvCode: string;
    owner: string;
}