import { Injectable } from '@angular/core';
import { first } from 'rxjs/internal/operators/first';
import { CodeModel, User } from '../models/models';
import { UserApiService } from './user-api.service';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private _apiService: UserApiService) { }

  createUser(user: User) {
    return this._apiService.post(user).pipe(first());
  }

  loginUser(user: User) {
    return this._apiService.postLogin(user);
  }

  loginAdmin(code: CodeModel) {
    return this._apiService.post2FA(code);
  }

  getUser() {
    return this._apiService.getUser();
  }
  deleteUser(id: string) {
    return this._apiService.delete(id);
  }

  getAllUsers() {
    return this._apiService.getAllUsers();
  }
}
