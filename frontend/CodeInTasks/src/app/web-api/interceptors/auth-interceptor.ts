import { HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { UserService } from "../services";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(private userService: UserService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler) {
        const authToken = this.userService.accessToken;

        const authReq = authToken === null ? req : req.clone({
            headers: req.headers.set('Authorization', `Bearer ${authToken}`)
        });

        return next.handle(authReq);
    }
}
