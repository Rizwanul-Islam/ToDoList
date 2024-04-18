import React, { useEffect, useState } from 'react';
import '../TodoList.css';
import { Task } from '../interfaces/Task';
import { TaskService } from '../services/TaskService';
import { TextField, Button, Grid, Paper, InputLabel, Typography, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@mui/material';
import AddCircleOutlineOutlinedIcon from '@mui/icons-material/AddCircleOutlineOutlined';
import DeleteIcon from '@mui/icons-material/Delete';
import DoneIcon from '@mui/icons-material/Done';

const taskService = new TaskService('http://localhost:5246');

const TodoList: React.FC = () => {
  const [tasks, setTasks] = useState<Task[]>([]);
  const [newTask, setNewTask] = useState({
    taskName: '',
    startDate: '',
    endDate: ''
  });
  const [error, setError] = useState('');

  useEffect(() => {
    taskService.fetchTasks()
      .then(data => {
        setTasks(data);
      });
  }, []);

  const createTask = () => {
    // Validation logic here
    if (newTask.taskName.length <= 10) {
      setError('Task name must be longer than 10 characters.');
      return;
    }

    const taskToCreate: Task = {
      taskName: newTask.taskName,
      startDate: newTask.startDate,
      endDate: newTask.endDate
    };

    taskService.createTask(taskToCreate)
      .then(() => {
        setTasks([...tasks, taskToCreate]);
        setNewTask({
          taskName: '',
          startDate: '',
          endDate: ''
        });
        setError('');
      });
  };

  const deleteTask = (id?: number) => {
    if (id === undefined) {
      console.error('Invalid ID');
      return;
    }
    taskService.deleteTask(id)
      .then(() => {
        setTasks(tasks.filter(task => task.id !== id));
      });
  };

  const handleTaskDone = (id?: number) => {
    if (id === undefined) {
      console.error('Invalid ID');
      return;
    }
    const taskToUpdate = tasks.find(task => task.id === id);
    if (!taskToUpdate) {
      console.error('Task not found');
      return;
    }
    const updatedTask: Task = { ...taskToUpdate, isDone: true };
    taskService.updateTask(id, updatedTask)
      .then(() => {
        const updatedTasks = tasks.map(task => {
          if (task.id === id) {
            return { ...task, isDone: true };
          }
          return task;
        });
        setTasks(updatedTasks);
      })
      .catch(error => {
        console.error('Error marking task as done:', error);
      });
  };

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;
    setNewTask(prevState => ({
      ...prevState,
      [name]: value
    }));
  };

  // Function to format date as "24 April 2024"
  const formatDate = (dateString: string) => {
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', { day: '2-digit', month: 'long', year: 'numeric' });
  };

  // Function to check if a task is overdue
  const isOverdue = (endDate: string) => {
    return new Date(endDate) < new Date();
  };

  return (
    <div className="container">
      <Typography variant="h4" gutterBottom>
        Todo app
      </Typography>
      <Grid container justifyContent="center">
        <Grid item xs={6}>
          <Paper elevation={3} className="form-container">
            <form onSubmit={createTask}>
              <Grid container direction="column" spacing={2} sx={{ padding: '20px' }}>
                <Grid item>
                <InputLabel htmlFor="taskName">Task Name</InputLabel>
                  <TextField
                    fullWidth
                    id="taskName"
                    name="taskName"
                    variant="outlined"
                    value={newTask.taskName}
                    onChange={handleInputChange}
                  />
                </Grid>
                <Grid item container spacing={2}>
                  <Grid item xs={6}>
                  <InputLabel htmlFor="startDate">Start Date</InputLabel>
                    <TextField
                      fullWidth
                      id="startDate"
                      type="date"
                      name="startDate"
                      variant="outlined"
                      value={newTask.startDate}
                      onChange={handleInputChange}
                    />
                  </Grid>
                  <Grid item xs={6}>
                  <InputLabel htmlFor="endDate">End Date</InputLabel>
                    <TextField
                      fullWidth
                      id="endDate"
                      type="date"
                      name="endDate"
                      variant="outlined"
                      value={newTask.endDate}
                      onChange={handleInputChange}
                    />
                  </Grid>
                </Grid>
                <Button
                  type="submit"
                  variant="contained"
                  color="primary"
                  startIcon={<AddCircleOutlineOutlinedIcon />}
                  style={{ marginTop: '16px' }}
                >
                  Create Task
                </Button>
                {error && <Typography variant="body2" color="error" style={{ marginTop: '8px' }}>{error}</Typography>}
              </Grid>
            </form>
          </Paper>
        </Grid>
      </Grid>

      <TableContainer component={Paper} style={{ marginTop: '16px' }}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Task Name</TableCell>
              <TableCell>Start Date</TableCell>
              <TableCell>End Date</TableCell>
              <TableCell>Actions</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
  {tasks.map(task => (
    <TableRow key={task.id} className={isOverdue(task.endDate) ? 'overdue' : ''}>
      <TableCell>{task.taskName}</TableCell>
      <TableCell>{formatDate(task.startDate)}</TableCell>
      <TableCell>{formatDate(task.endDate)}</TableCell>
      <TableCell>
        <Button onClick={() => deleteTask(task.id)} startIcon={<DeleteIcon />} size="small">Delete</Button>
        {!task.isDone && <Button onClick={() => handleTaskDone(task.id)} startIcon={<DoneIcon />} size="small">Done</Button>}
      </TableCell>
    </TableRow>
  ))}
</TableBody>
        </Table>
      </TableContainer>
    </div>
  );
};

export default TodoList;
