export interface IJobToRequest {
    id: number;
    numberCandidate: number;
    jobDateStart: string;
    jobDateEnd: string;
    startTime: string;
    endTime: string;
    numberApplied: number;
    timeDetailId: number;
    endTimeDetailId: number;
    jobTypeId: number;
    agencyId: number;
    gradeId: number;
    clientLocationId: number;
    attributeDetailId: number;
    appUserId: string;
    shiftStateId: number;
    paymentTypeId: number;
    ariaId: number;
    dateRange: Date[];
  }

export class JobToRequest implements IJobToRequest {
    id: number;
    numberCandidate: number;
    jobDateStart: string;
    jobDateEnd: string;
    startTime: string;
    endTime: string;
    numberApplied: number;
    timeDetailId: number;
    endTimeDetailId: number;
    jobTypeId: number;
    agencyId: number;
    gradeId: number;
    clientLocationId: number;
    attributeDetailId: number;
    appUserId: string;
    shiftStateId: number;
    paymentTypeId: number;
    ariaId: number;
    dateRange: Date[];
}
