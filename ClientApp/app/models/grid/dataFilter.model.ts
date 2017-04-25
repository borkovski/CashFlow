export class DataFilter {
    public sortPropertyName: string;
    public isDescending: boolean;
    public skip: number;
    public take: number;
    public filterProperties: KeyValuePair[];

    constructor() {
        this.filterProperties = new Array<KeyValuePair>();
    }
}

export class KeyValuePair {
    public Key: string;
    public Value: string;
}

export class PagedList<T> {
    public items: T[];
    public totalCount: number;
}