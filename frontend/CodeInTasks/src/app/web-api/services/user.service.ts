import { Injectable } from '@angular/core';
import UserServiceInterface from '../interfaces/user-service-interface';

@Injectable({
  providedIn: 'root'
})
export default class UserService implements UserServiceInterface {

  constructor() { }
}
