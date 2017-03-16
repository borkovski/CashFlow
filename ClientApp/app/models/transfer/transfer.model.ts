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

    constructor() {
        this.isRepeated = false;
    }
}