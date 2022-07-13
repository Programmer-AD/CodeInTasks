import { Observable } from "rxjs";
import BanManageModel from "../models/user/ban-manage-model";
import RoleManageModel from "../models/user/role-manage-model";
import UserCreateModel from "../models/user/user-create-model";
import UserSignInModel from "../models/user/user-sign-in-model";
import UserSignInResultModel from "../models/user/user-sign-in-result-model";
import UserViewModel from "../models/user/user-view-model";

export default abstract class UserServiceInterface {
    public abstract get accessToken(): string | null;

    public abstract get currentUser(): UserViewModel | null;

    public abstract signInAsync(signInModel: UserSignInModel): Observable<UserSignInResultModel>;

    public abstract signOutAsync(): Observable<unknown>;

    public abstract registerAsync(createModel: UserCreateModel): Observable<unknown>;

    public abstract getUserInfoAsync(userId: string): Observable<UserViewModel>;

    public abstract setRoleAsync(roleManageModel: RoleManageModel): Observable<unknown>;

    public abstract setBanAsync(banManageModel: BanManageModel): Observable<unknown>;
}
