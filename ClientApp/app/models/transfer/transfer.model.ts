export class Transfer {
    id: number;
    title: string;
    amount: number;
    direction: number;
    date: string;
    isRepeated: boolean;
    repeatPeriod: number;
    finishDate: string;
    isContinuous: boolean;
    account: number;

    constructor() {
        this.isRepeated = false;
    }
}