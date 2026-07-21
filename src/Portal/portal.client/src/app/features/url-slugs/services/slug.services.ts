import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { UrlSlug,AddSlug, ApiResponse } from '../types/ISlug';


@Injectable({ providedIn: 'root' })
export class SlugMappingsService {

  private baseSlug = `${environment.apiBaseUrl}/Urls`;

  constructor(private http: HttpClient) {}

  getSlugMappings(): Observable<UrlSlug[]> {
  return this.http.get<UrlSlug[]>(
    `${this.baseSlug}/AllSlug`
  );
}
  getSlugMappingById(id: string): Observable<ApiResponse<UrlSlug>> {
    return this.http.get<ApiResponse<UrlSlug>>(`${this.baseSlug}/${id}`);
  }

  createSlugMapping(SlugMapping: AddSlug): Observable<ApiResponse<UrlSlug>> {
    return this.http.post<ApiResponse<UrlSlug>>(`${this.baseSlug}/Create`, SlugMapping);
  }

  updateSlugMapping(SlugMapping: AddSlug): Observable<ApiResponse<UrlSlug>> {
    return this.http.put<ApiResponse<UrlSlug>>(`${this.baseSlug}/${SlugMapping.id}`, SlugMapping);
  }

  deleteSlugMapping(id: string): Observable<ApiResponse<any>> {
    return this.http.delete<ApiResponse<any>>(`${this.baseSlug}/${id}`);
  }

}
