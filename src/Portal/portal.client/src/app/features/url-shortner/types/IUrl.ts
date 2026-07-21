export interface UrlMapping {
  id: string;
  url?: string;
}

export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data: T;
  errors: any[];
}
