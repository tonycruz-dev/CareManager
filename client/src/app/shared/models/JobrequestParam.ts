export class JobRequestParams {
    sort = 'dateDesc';
    pageNumber = 1;
    pageSize = 15;
    search: string;
    dateFrom: string;
    dateTo: string;
    dateRange: Date[];
    agencyId?: string;
}
