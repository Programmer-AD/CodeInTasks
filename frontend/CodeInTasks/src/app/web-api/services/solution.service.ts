import { Injectable } from '@angular/core';
import SolutionServiceInterface from '../interfaces/solution-service-interface';

@Injectable({
  providedIn: 'root'
})
export default class SolutionService implements SolutionServiceInterface {

  constructor() { }
}
