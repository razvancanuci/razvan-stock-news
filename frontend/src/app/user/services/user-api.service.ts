import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CodeModel, TokenModel, User, UserId, UserRole } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class UserApiService {

  constructor(private _httpClient: HttpClient) { }

  post(user: User) {
    return this._httpClient.post(`${environment.userApi}/api/User`, user);
  }

  postLogin(user: User) {
    return this._httpClient.post(`${environment.userApi}/api/User/login`, user).pipe(
      map<any, TokenModel>((response) => response.result
      ));
  }

  getUser() {
    return this._httpClient.get(`${environment.userApi}/api/User/me`).pipe(
      map<any, UserRole>((response) => response.result
      ));
  }

  getAllUsers() {
    return this._httpClient.get(`${environment.userApi}/api/User`).pipe(
      map<any, UserId[]>((response) => response.result)
    );
  }

  delete(id: string) {
    return this._httpClient.delete(`${environment.userApi}/api/User/${id}`);
  }
  post2FA(code: CodeModel) {
    return this._httpClient.post(`${environment.userApi}/api/User/2FA`, code).pipe(
      map<any, TokenModel>((response) => response.result
      ));;
  }
}
