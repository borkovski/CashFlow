import { ColumnDefinition } from '../grid/columnDefinition.model';
import { DataFilter } from '../grid/dataFilter.model';

export class GridDefinition {
    columnDefinitions: ColumnDefinition[];
    data: any[];
    dataFilter: DataFilter;

    constructor() {
        this.dataFilter = new DataFilter();
    }
}