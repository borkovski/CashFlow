import { Transfer } from '../transfer/transfer.model';
import { AccountBalanceHistory } from '../account/accountBalanceHistory.model';

export class AccountHistory {
    id: number;
    name: string;
    accountStartBalance: number;
    accountCurrentBalance: number;
    incomingTransfers: Transfer[];
    outgoingTransfers: Transfer[];
    accountBalanceHistory: AccountBalanceHistory[];
}