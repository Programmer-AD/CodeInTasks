import { Provider } from "@angular/core"
import SolutionServiceInterface from "./interfaces/solution-service-interface";
import TaskServiceInterface from "./interfaces/task-service-interface";
import UserServiceInterface from "./interfaces/user-service-interface";
import SolutionService from "./services/solution.service";
import TaskService from "./services/task.service";
import UserService from "./services/user.service";

const serviceProviders : Provider[] = [
    {provide: SolutionServiceInterface, useClass: SolutionService},
    {provide: TaskServiceInterface, useClass: TaskService},
    {provide: UserServiceInterface, useClass: UserService},
];

export default serviceProviders;
