import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import * as moment from 'moment';

import { DataFilter, PagedList } from '../../models/grid/dataFilter.model';

import 'rxjs/add/operator/toPromise';

@Injectable()
export abstract class BaseService<T> {
    protected url = '';

    constructor(protected http: Http) { }

    protected getList(dataFilter: DataFilter = null): Promise<T[]> {
        return this.http.get(this.url + this.parseDataFilter(dataFilter)).toPromise()
            .then(response => response.json() as T[])
            .catch(this.handleError);
    }

    protected getPagedList(dataFilter: DataFilter = null): Promise<PagedList<T>> {
        return this.http.get(this.url + this.parseDataFilter(dataFilter)).toPromise()
            .then(response => response.json() as PagedList<T>)
            .catch(this.handleError);
    }

    protected get(id: number): Promise<T> {
        return this.http.get(this.url + '/' + id).toPromise()
            .then(response => {
                return response.json() as T;
            })
            .catch(this.handleError);
    }

    protected post(item: T): Promise<number> {
        return this.http.post(this.url, item).toPromise()
            .then(response => response.json() as number)
            .catch(this.handleError);
    }

    protected delete(itemId: number): Promise<boolean> {
        return this.http.delete(this.url + '/' + itemId).toPromise()
            .then(response => response.ok)
            .catch(this.handleError);
    }

    protected handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }

    private parseDataFilter(dataFilter: DataFilter) {
        let filterString:string = "";
        if (dataFilter) {
            if (dataFilter.sortPropertyName) {
                filterString = this.addParameter(filterString, "sortPropertyName=" + dataFilter.sortPropertyName);
            }
            if (dataFilter.isDescending != null) {
                filterString = this.addParameter(filterString, "isDescending=" + dataFilter.isDescending);
            }
            if (dataFilter.skip) {
                filterString = this.addParameter(filterString, "skip=" + dataFilter.skip);
            }
            if (dataFilter.take) {
                filterString = this.addParameter(filterString, "take=" + dataFilter.take);
            }
        }
        return filterString;
    }

    private addParameter(queryString: string, parameter: string) {
        return queryString += (queryString.length > 0 ? "&" : "?") + parameter;
    }
}