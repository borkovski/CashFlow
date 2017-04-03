import { Transfer } from '../transfer/transfer.model';

export class AccountHistory {
    id: number;
    name: string;
    accountStartBalance: number;
    accountCurrentBalance: number;
    incomingTransfers: Transfer[];
    outgoingTransfers: Transfer[];
}