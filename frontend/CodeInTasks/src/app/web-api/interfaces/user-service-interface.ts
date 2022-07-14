import { Observable } from "rxjs";
import { BanManageModel, RoleManageModel, UserCreateModel, UserSignInModel, UserSignInResultModel, UserViewModel } from "../models/user";

export abstract class UserServiceInterface {
    public abstract get accessToken(): string | null;

    public abstract get currentUser(): UserViewModel | null;

    public abstract signInAsync(signInModel: UserSignInModel): Observable<UserSignInResultModel>;

    public abstract signOutAsync(): Observable<unknown>;

    public abstract registerAsync(createModel: UserCreateModel): Observable<unknown>;

    public abstract getUserInfoAsync(userId: string): Observable<UserViewModel>;

    public abstract setRoleAsync(roleManageModel: RoleManageModel): Observable<unknown>;

    public abstract setBanAsync(banManageModel: BanManageModel): Observable<unknown>;
}
