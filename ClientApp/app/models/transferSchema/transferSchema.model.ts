export class TransferSchema {
    id: number;
    accountFromId: number;
    accountToId: number;
    title: string;
    amount: number;
    transferStartDate: string;
    transferPeriod: string;
    transferEndDate: string;
    lastTransferDate: string;
}