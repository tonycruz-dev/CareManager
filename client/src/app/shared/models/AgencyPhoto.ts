export interface IAgencyPhoto {
  id: number;
  url: string;
  description: string;
  isMain: boolean;
  publicId: string;
  agencyId: number;
}

export class AgencyPhoto {
  id: number;
  url: string;
  description: string;
  isMain: boolean;
  publicId: string;
  agencyId: number;
}
