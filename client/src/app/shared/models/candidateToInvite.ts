export interface ICandidateToInvite {
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
    photoUrl: string;
    appUserId: string;
    grade: string;
    gradeId: number;
    isSelected: boolean;
}
export interface ICandidateResponded {
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
  photoUrl: string;
  appUserId: string;
  grade: string;
  gradeId: number;
  accept: boolean;
  reject: boolean;
  isSelected: boolean;
}
