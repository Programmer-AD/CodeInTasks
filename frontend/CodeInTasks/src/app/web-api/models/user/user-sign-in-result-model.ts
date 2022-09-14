import { UserViewModel } from "..";

export class UserSignInResultModel {
    public token: string = null!;
    public expirationDate: Date = null!;
    public user: UserViewModel = null!;
}
