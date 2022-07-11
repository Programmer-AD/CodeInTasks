import RoleEnum from "../../enums/role-enum";

export default class UserViewModel {
    public userId: string = null!;
    public name: string = null!;
    public roles: RoleEnum[] = [];
    public isBanned: boolean = null!;
}
