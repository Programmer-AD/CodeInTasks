import { RoleEnum } from "../../enums";

export class UserViewModel {
    public userId: string = null!;
    public name: string = null!;
    public roles: RoleEnum[] = [];
    public isBanned: boolean = null!;
}
