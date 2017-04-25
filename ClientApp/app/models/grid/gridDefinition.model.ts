import { ColumnDefinition } from '../grid/columnDefinition.model';
import { DataFilter } from '../grid/dataFilter.model';

export class GridDefinition {
    columnDefinitions: ColumnDefinition[];
    data: any[];
    totalCount: number;
    dataFilter: DataFilter;
    pageSizes: number[] = [10, 20, 50];

    constructor() {
        this.dataFilter = new DataFilter();
    }

    public getCurrentPageNumber(): number {
        if (!this.dataFilter.take || this.dataFilter.take == 0) {
            return 0;
        }
        return Math.ceil(this.dataFilter.skip / this.dataFilter.take) + 1;
    }

    public setCurrentPageNumber(page: number) {
        this.dataFilter.skip = Math.ceil(this.dataFilter.take * --page);
    }

    public getTotalPages(): number {
        if (!this.dataFilter.take || this.dataFilter.take == 0) {
            return 0;
        }
        return Math.ceil(this.totalCount / this.dataFilter.take);
    }

    public getPages(): number[] {
        let totalPages = this.getTotalPages();
        let currentPage = this.getCurrentPageNumber();

        let startPage: number, endPage: number;
        if (totalPages <= 10) {
            // less than 10 total pages so show all
            startPage = 1;
            endPage = totalPages;
        } else {
            // more than 10 total pages so calculate start and end pages
            if (currentPage <= 6) {
                startPage = 1;
                endPage = 10;
            } else if (currentPage + 4 >= totalPages) {
                startPage = totalPages - 9;
                endPage = totalPages;
            } else {
                startPage = currentPage - 5;
                endPage = currentPage + 4;
            }
        }

        let pages: number[] = [];
        for (let i = startPage; i <= endPage; i++) {
            pages.push(i);
        }
        return pages;
    }
}