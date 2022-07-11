import { Observable } from "rxjs";
import BanManageModel from "../models/user/ban-manage-model";
import RoleManageModel from "../models/user/role-manage-model";
import UserCreateModel from "../models/user/user-create-model";
import UserSignInModel from "../models/user/user-sign-in-model";
import UserSignInResultModel from "../models/user/user-sign-in-result-model";
import UserViewModel from "../models/user/user-view-model";

export default interface UserServiceInterface {
    signInAsync(signInModel: UserSignInModel): Observable<UserSignInResultModel>;

    registerAsync(createModel: UserCreateModel): Observable<void>;

    getUserInfoAsync(userId: string): Observable<UserViewModel>;

    setRoleAsync(roleManageModel: RoleManageModel): Observable<void>;

    setBanAsync(banManageModel: BanManageModel): Observable<void>;
}
