// Task.ts
import { BaseType } from './BaseType';

export interface Task extends BaseType {
    taskName: string;
    startDate: string;
    endDate: string;
}
  