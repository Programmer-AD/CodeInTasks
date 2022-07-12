import { Injectable } from '@angular/core';
import TaskServiceInterface from '../interfaces/task-service-interface';

@Injectable({
  providedIn: 'root'
})
export default class TaskService implements TaskServiceInterface {

  constructor() { }
}
