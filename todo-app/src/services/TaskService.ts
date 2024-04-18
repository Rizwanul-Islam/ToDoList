import { Task } from '../interfaces/Task';

export class TaskService {
  private baseUrl: string;

  constructor(baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  async fetchTasks(): Promise<Task[]> {
    try {
      const response = await fetch(`${this.baseUrl}/task/list`);
      if (!response.ok) {
        throw new Error('Failed to fetch tasks');
      }
      return await response.json();
    } catch (error) {
      console.error('Error fetching tasks:', error);
      return [];
    }
  }

  async createTask(newTask: Task): Promise<void> {
    try {
      const response = await fetch(`${this.baseUrl}/task/create`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(newTask),
      });
      if (!response.ok) {
        throw new Error('Failed to create task');
      }
    } catch (error) {
      console.error('Error creating task:', error);
    }
  }

  async updateTask(id: number, updatedTask: Task): Promise<void> {
    try {
      const response = await fetch(`${this.baseUrl}/task/update/${id}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(updatedTask),
      });
      if (!response.ok) {
        throw new Error('Failed to update task');
      }
    } catch (error) {
      console.error('Error updating task:', error);
    }
  }

  async deleteTask(id: number): Promise<void> {
    try {
      const response = await fetch(`${this.baseUrl}/task/delete/${id}`, {
        method: 'DELETE',
      });
      if (!response.ok) {
        throw new Error('Failed to delete task');
      }
    } catch (error) {
      console.error('Error deleting task:', error);
    }
  }
}
