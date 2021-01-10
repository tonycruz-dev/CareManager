import { IJobrequest } from './JobRequest';

export interface IJobRequestPagination {
    pageIndex: number;
    pageSize: number;
    count: number;
    currentPage: number;
    totalPages: number;
    totalCount: number;
    booked: number;
    canceled: number;
    finish: number;
    inProgress: number;
    pending: number;
    data: IJobrequest[];
}

export class JobRequestPagination implements IJobRequestPagination {
    pageIndex: number;
    pageSize: number;
    count: number;
    currentPage: number;
    totalPages: number;
    totalCount: number;
    booked: number;
    canceled: number;
    finish: number;
    inProgress: number;
    pending: number;
    data: IJobrequest[] = [];
}
