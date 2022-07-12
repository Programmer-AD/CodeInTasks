import { Observable } from "rxjs";
import BanManageModel from "../models/user/ban-manage-model";
import RoleManageModel from "../models/user/role-manage-model";
import UserCreateModel from "../models/user/user-create-model";
import UserSignInModel from "../models/user/user-sign-in-model";
import UserSignInResultModel from "../models/user/user-sign-in-result-model";
import UserViewModel from "../models/user/user-view-model";

export default abstract class UserServiceInterface {
    public abstract signInAsync(signInModel: UserSignInModel): Observable<UserSignInResultModel>;

    public abstract registerAsync(createModel: UserCreateModel): Observable<void>;

    public abstract getUserInfoAsync(userId: string): Observable<UserViewModel>;

    public abstract setRoleAsync(roleManageModel: RoleManageModel): Observable<void>;

    public abstract setBanAsync(banManageModel: BanManageModel): Observable<void>;
}
