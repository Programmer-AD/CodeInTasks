import { Provider } from "@angular/core"
import { SolutionServiceInterface, TaskServiceInterface, UserServiceInterface } from "./interfaces";
import { SolutionService, TaskService, UserService } from "./services";

const serviceProviders : Provider[] = [
    {provide: SolutionServiceInterface, useClass: SolutionService},
    {provide: TaskServiceInterface, useClass: TaskService},
    {provide: UserServiceInterface, useClass: UserService},
];

export default serviceProviders;
