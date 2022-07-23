import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EMPTY, map, Observable } from 'rxjs';
import { UserServiceInterface } from '../interfaces';
import { BanManageModel, RoleManageModel, UserCreateModel, UserSignInModel, UserSignInResultModel, UserViewModel } from '../models';

@Injectable()
export class UserService implements UserServiceInterface {
  private static readonly basePath = "/api/user";

  private accessTokenValue: string | null = null;
  private tokenExpires: Date | null = null;
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
    return this.httpClient.post<UserSignInResultModel>(`${UserService.basePath}/signIn`, signInModel)
      .pipe(map(this.saveSignInResult.bind(this)));
  }

  public signOutAsync(): Observable<unknown> {
    this.accessToken = null;
    this.tokenExpires = null;
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

  private saveSignInResult(signInResult: UserSignInResultModel) : UserSignInResultModel {
    this.accessToken = signInResult.token;
    this.currentUser = signInResult.user;
    this.tokenExpires = signInResult.expirationDate;

    return signInResult;
  }
}
