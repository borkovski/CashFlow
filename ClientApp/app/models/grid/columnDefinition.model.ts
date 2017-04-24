export class ColumnDefinition {
    dataKey: string;
    dataHeader: string;
    filterType: string;
    sortingEnabled: boolean;

    constructor(dataKey: string, dataHeader: string, filterType: string, sortingEnabled: boolean) {
        this.dataKey = dataKey;
        this.dataHeader = dataHeader;
        this.filterType = filterType;
        this.sortingEnabled = sortingEnabled;
    }
}