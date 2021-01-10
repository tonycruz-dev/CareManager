export interface ICandidatePhoto {
    id: number;
    url: string;
    description: string;
    isMain: boolean;
    publicId: string;
    candidateId: number;
}

export class CandidatePhoto {
    id: number;
    url: string;
    description: string;
    isMain: boolean;
    publicId: string;
    candidateId: number;
}
