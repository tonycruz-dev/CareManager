
export interface IAgencyLoops {
    timeDetails: TimeDetail[];
    jobTypes: JobType[];
    grades: Grade[];
    clientLocations: ClientLocation[];
    attributeDetails: AttributeDetail[];
    shiftStates: ShiftState[];
    paymentTypes: PaymentType[];
    arias: Aria[];
  }

export interface Aria {
    id: number;
    name: string;
    borough: string;
  }

export interface PaymentType {
    id: number;
    name: string;
  }

export interface ShiftState {
    id: number;
    shiftDetails: string;
  }

export interface AttributeDetail {
    id: number;
    attributeName: string;
}
export interface ClientLocation {
    id: number;
    companyName: string;
    managerName: string;
    contactName: string;
    address1: string;
    address2: string;
    address3: string;
    address4: string;
    address5: string;
    contactNumber: string;
    agencyId: number;
}

export interface Grade {
    id: number;
    gradeName: string;
  }
export interface JobType {
    id: number;
    jobName: string;
}
export interface TimeDetail {
    id: number;
    hour: string;
}
