import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable()
export class BaseUrlInterceptor implements HttpInterceptor {

  intercept(req: HttpRequest<any>, next: HttpHandler) {

    console.log('Interceptor running:', req.url); 

    if (!req.url.startsWith('http')) {

      const apiReq = req.clone({
        url: environment.apiBaseUrl + req.url
      });

      console.log('Final URL:', apiReq.url); 

      return next.handle(apiReq);
    }

    return next.handle(req);
  }
}