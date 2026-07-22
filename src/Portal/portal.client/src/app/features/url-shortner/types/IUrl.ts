export interface UrlMapping {
  id: string;
  slug?: string;
  destinationUrl:string;
  createdAt: Date;
}

export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data: T;
  errors: any[];
}
