import { Injectable } from '@angular/core';
import { BackendSettings } from '../models/consts/backendSettings';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {

  constructor(private backendSettings: BackendSettings, private http: HttpClient) { }
}
