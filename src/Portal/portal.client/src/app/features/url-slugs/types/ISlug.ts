export interface UrlSlug {
    id: string;
  slug: string;
  status?: number;
}

export interface AddSlug {
    id: string;
  name: string;
}

export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data: T;
  errors: any[];
}
