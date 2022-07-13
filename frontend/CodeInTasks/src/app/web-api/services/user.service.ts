import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EMPTY, Observable } from 'rxjs';
import UserServiceInterface from '../interfaces/user-service-interface';
import BanManageModel from '../models/user/ban-manage-model';
import RoleManageModel from '../models/user/role-manage-model';
import UserCreateModel from '../models/user/user-create-model';
import UserSignInModel from '../models/user/user-sign-in-model';
import UserSignInResultModel from '../models/user/user-sign-in-result-model';
import UserViewModel from '../models/user/user-view-model';

@Injectable()
export default class UserService implements UserServiceInterface {
  private static readonly basePath = "/api/user";

  private accessTokenValue: string | null = null;
  private currentUserValue: UserViewModel | null = null;

  constructor(private httpClient: HttpClient) { }

  public get accessToken(): string | null {
    return this.accessTokenValue;
  }

  private set accessToken(value: string | null) {
    this.accessTokenValue = value;
  }

  public get currentUser(): UserViewModel | null {
    return this.currentUserValue;
  }

  private set currentUser(value: UserViewModel | null) {
    this.currentUserValue = value;
  }

  public signInAsync(signInModel: UserSignInModel): Observable<UserSignInResultModel> {
    return this.httpClient.post<UserSignInResultModel>(`${UserService.basePath}/signIn`, signInModel);
  }

  public signOutAsync(): Observable<unknown> {
    this.accessToken = null;
    this.currentUser = null;

    return EMPTY;
  }

  public registerAsync(createModel: UserCreateModel): Observable<unknown> {
    return this.httpClient.post(`${UserService.basePath}/register`, createModel);
  }

  public getUserInfoAsync(userId: string): Observable<UserViewModel> {
    return this.httpClient.get<UserViewModel>(`${UserService.basePath}/${userId}`);
  }

  public setRoleAsync(roleManageModel: RoleManageModel): Observable<unknown> {
    return this.httpClient.put(`${UserService.basePath}/role`, roleManageModel);
  }

  public setBanAsync(banManageModel: BanManageModel): Observable<unknown> {
    return this.httpClient.put(`${UserService.basePath}/ban`, banManageModel);
  }
}
