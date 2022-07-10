import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BackendSettings } from '../models/consts/backendSettings';
import { EndpointsUrl } from '../models/consts/endpointsUrl';
import { BehaviorSubject, Observable } from 'rxjs';
import { BaseBackendResponse } from '../models/response/BaseBackendResponse';
import { BackendResponse } from '../models/response/backendResponse';
import { GetImportantInfoDto } from '../models/dto/getImportantInfoDto';
import { AddImportantInfoDto } from '../models/dto/addImportantInfoDto';

@Injectable({
  providedIn: 'root'
})
export class ImportantInfoService {

  constructor(private http: HttpClient, 
    private backendSettings: BackendSettings, 
    private endpoints: EndpointsUrl) {
  }

  getInfosResponse$ = new BehaviorSubject<BackendResponse<GetImportantInfoDto[]>>({});
  addNewInfoResponse$ = new BehaviorSubject<BaseBackendResponse>({});

  private getInfosResponse() : Observable<BackendResponse<GetImportantInfoDto[]>> {
    var url = this.backendSettings.baseAddress + this.endpoints.getImportantInfoEndpoint;
    return this.http.post<BackendResponse<GetImportantInfoDto[]>>(url, { });
  }

  private addNewInfoResponse(newInfo: AddImportantInfoDto) : Observable<BaseBackendResponse> {
    var url = this.backendSettings.baseAddress + this.endpoints.addImportantInfoEndpoint;
    return this.http.post<BaseBackendResponse>(url, { ImportantInfoDetails: newInfo });
  }

  getInfos(o) {
    this.getInfosResponse().subscribe(x => {
      this.getInfosResponse$.next(x);
    });
  }

  addNewInfo(newInfo: AddImportantInfoDto) {
    this.addNewInfoResponse(newInfo).subscribe(x => {
      this.addNewInfoResponse$.next(x);
    });
  }
}
