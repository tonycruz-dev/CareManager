export interface ICandidateBooked {
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
  jobToRequestId: number;
  jobConfirmedId: number;
  raiting: number;
  comment: string;
  finishShift: boolean;
  lostShift: boolean;
  isSelected: boolean;
}
