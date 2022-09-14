import { Provider } from "@angular/core"
import { SolutionServiceInterface, TaskServiceInterface, UserServiceInterface } from "../interfaces";
import { SolutionService, TaskService, UserService } from ".";

export const serviceProviders: Provider[] = [
    { provide: SolutionServiceInterface, useClass: SolutionService },
    { provide: TaskServiceInterface, useClass: TaskService },
    { provide: UserServiceInterface, useClass: UserService },
];
