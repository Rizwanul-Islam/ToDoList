import React, { useEffect, useState } from 'react';
import './TodoList.css';

interface Task {
  taskName: string;
  startDate: string;
  endDate: string;
  id: number;
  isDone : boolean
}

const TodoList: React.FC = () => {
  const [tasks, setTasks] = useState<Task[]>([]);
  const [newTaskName, setNewTaskName] = useState('');
  const [newTaskStartDate, setNewTaskStartDate] = useState('');
  const [newTaskEndDate, setNewTaskEndDate] = useState('');
  const [error, setError] = useState('');

  useEffect(() => {
    fetch('http://localhost:5246/tl')
      .then(response => {
        if (!response.ok) {
          throw new Error('Failed to fetch tasks');
        }
        return response.json();
      })
      .then(data => {
        setTasks(data);
      })
      .catch(error => {
        console.error('Error fetching tasks:', error);
      });
  }, []);

  const createTask = () => {
    if (newTaskName.length <= 10) {
      setError('Task name must be longer than 10 characters.');
      return;
    }

    const newTask: Task = {
      taskName: newTaskName,
      startDate: newTaskStartDate,
      endDate: newTaskEndDate,
      id: 0, // Dummy ID for now
      isDone: false 
    };

    fetch('http://localhost:5246/t', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(newTask),
    })
      .then(response => {
        if (!response.ok) {
          throw new Error('Failed to create task');
        }
        return response.json();
      })
      .then(() => {
        setTasks([...tasks, newTask]);
        setNewTaskName('');
        setNewTaskStartDate('');
        setNewTaskEndDate('');
        setError('');
      })
      .catch(error => {
        console.error('Error creating task:', error);
      });
  };

  const deleteTask = (id: number) => {
    fetch(`http://localhost:5246/t/${id}`, {
      method: 'POST',
    })
      .then(response => {
        if (!response.ok) {
          throw new Error('Failed to delete task');
        }
        setTasks(tasks.filter(task => task.id !== id));
      })
      .catch(error => {
        console.error('Error deleting task:', error);
      });
  };

  const handleTaskNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setNewTaskName(event.target.value);
  };

  const handleStartDateChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setNewTaskStartDate(event.target.value);
  };

  const handleEndDateChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setNewTaskEndDate(event.target.value);
  };

  // Function to format date as "24 April 2024"
  const formatDate = (dateString: string) => {
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', { day: '2-digit', month: 'long', year: 'numeric' });
  };

  return (
    <div>
      <h1>Todo App</h1>
      <div>
        <h2>Create New Task</h2>
        <div>
          <label>Task Name:</label>
          <input type="text" value={newTaskName} onChange={handleTaskNameChange} />
        </div>
        <div>
          <label>Start Date:</label>
          <input type="date" value={newTaskStartDate} onChange={handleStartDateChange} />
        </div>
        <div>
          <label>End Date:</label>
          <input type="date" value={newTaskEndDate} onChange={handleEndDateChange} />
        </div>
        <button onClick={createTask}>Create Task</button>
        {error && <div style={{ color: 'red' }}>{error}</div>}
      </div>
      <h1>Todo List</h1>
      <table>
        <thead>
          <tr>
            <th>Task Name</th>
            <th>Start Date</th>
            <th>End Date</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {tasks.map(task => (
            <tr key={task.id} className={new Date(task.endDate) < new Date() ? 'overdue' : ''}>
              <td>{task.taskName}</td>
              <td>{formatDate(task.startDate)}</td>
              <td>{formatDate(task.endDate)}</td>
              <td>
                <button onClick={() => deleteTask(task.id)}>Delete</button>
                {!task.isDone && <button onClick={() => console.log('Task done')}>Done</button>}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      
    </div>
  );
};

export default TodoList;
