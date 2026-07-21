import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { UrlMapping, ApiResponse } from '../types/IUrl';


@Injectable({ providedIn: 'root' })
export class UrlMappingsService {

  private baseUrl = `${environment.apiBaseUrl}/Urls`;

  constructor(private http: HttpClient) {}

  getUrlMappings(): Observable<ApiResponse<UrlMapping[]>> {
    return this.http.get<ApiResponse<UrlMapping[]>>(this.baseUrl);
  }

  getUrlMappingById(id: string): Observable<ApiResponse<UrlMapping>> {
    return this.http.get<ApiResponse<UrlMapping>>(`${this.baseUrl}/${id}`);
  }

  createUrlMapping(UrlMapping: UrlMapping): Observable<ApiResponse<UrlMapping>> {
    return this.http.post<ApiResponse<UrlMapping>>(`${this.baseUrl}/Create`, UrlMapping);
  }

  updateUrlMapping(UrlMapping: UrlMapping): Observable<ApiResponse<UrlMapping>> {
    return this.http.put<ApiResponse<UrlMapping>>(`${this.baseUrl}/${UrlMapping.id}`, UrlMapping);
  }

  deleteUrlMapping(id: string): Observable<ApiResponse<any>> {
    return this.http.delete<ApiResponse<any>>(`${this.baseUrl}/${id}`);
  }

}
