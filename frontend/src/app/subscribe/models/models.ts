export interface Subscriber {
    name: string;
    email: string;
    phoneNumber: string | null;
}

export interface Statistics {
    subscribedLast7D: number[];
    percentageSubscribersWithPhoneNumber: number;
}


export interface BarData {
    date: string;
    value: number;
}

export interface AnswerData {
    subscriberId: string;
    occQuestion: string;
    ageQuestion: number | null;
}

export interface EmailModel {
    email: string;
    hasAnswered: boolean;
}