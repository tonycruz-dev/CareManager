export interface HRDashboard {
    totalJobRequest: number;
    totalCandidates: number;
    totalJobConfirmed: number;
    totalInvitesCandidates: number;
    agencies: IAgency[];
    candidates: ICandidate[];
  }

export interface ICandidate {
    id: number;
    firstName: string;
    lastName: string;
    address1: string;
    address2: string;
    address3: string;
    address4: string;
    address5: string;
    contactNumber: string;
    email: string;
    accoutNumber: string;
    accoutName: string;
    sortCode: string;
    photoUrl: string;
    appUserId: string;
    grade: string;
    gradeId: number;
  }

export interface IAgency {
    id: number;
    name: string;
    contactName: string;
    address1: string;
    address2: string;
    address3: string;
    address4: string;
    address5: string;
    contactNumber: string;
    email: string;
    accoutNumber: string;
    accoutName: string;
    sortCode: string;
    logoUrl: string;
    appUserId: string;
    appUser?: any;
  }
