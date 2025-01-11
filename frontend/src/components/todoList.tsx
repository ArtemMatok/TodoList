import React, { useEffect, useState } from "react";
import { createTask, deleteTask, getTasks } from "../services/taskApi";
import { TodoGetDto } from "../models/todo";

const App: React.FC = () => {
  const [tasks, setTasks] = useState<TodoGetDto[]>([]);
  const [newTask, setNewTask] = useState<string>("");
  const [page, setPage] = useState<number>(1);
  const [pageSize] = useState<number>(5);
  const [hasNextPage, setHasNextPage] = useState<boolean>(false);
  const [hasPreviousPage, setHasPreviousPage] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const fetchTasks = async () => {
    try {
      const result = await getTasks(page, pageSize);
      if (result) {
        setTasks(result.items);
        setHasNextPage(result.hasNextPage);
        setHasPreviousPage(result.hasPreviousPage);
        setError(null);
      }
    } catch (error: any) {
      setError("Failed to load tasks. Please try again.");
    }
  };

  const handleAddTask = async () => {
    if (!newTask.trim()) {
      setError("Task title cannot be empty.");
      return;
    }

    try {
      const result = await createTask({ title: newTask });

      if (result) {
        setNewTask("");
        setError(null);

        fetchTasks();
      }
    } catch (error: any) {
      if (error.response?.data?.errors) {
        const errorMessages = Object.values(error.response.data.errors)
          .flat()
          .join(", ");
        setError(`Failed to add task: ${errorMessages}`);
      } else {
        setError(error.message || "Failed to add task. Please try again.");
      }
    }
  };

  const handleDeleteTask = async (id: number) => {
    try {
      const response = await deleteTask(id);
      if (response) {
        setTasks((prevTasks) => prevTasks.filter((task) => task.todoId !== id));
        setError(null);
      }
    } catch (error: any) {
      if (error.response?.data?.errors) {
        const errorMessages = Object.values(error.response.data.errors)
          .flat()
          .join(", ");
        setError(`Failed to add task: ${errorMessages}`);
      } else {
        setError(error.message || "Failed to delete task. Please try again.");
      }
    }
  };

  useEffect(() => {
    fetchTasks();
  }, [page]);

  return (
    <div className="container">
      <h1>Task Manager</h1>

      {error && (
        <div className="error-message">
          <p>{error}</p>
        </div>
      )}

      <div className="form">
        <input
          type="text"
          placeholder="Add a new task..."
          value={newTask}
          onChange={(e) => setNewTask(e.target.value)}
        />
        <button onClick={handleAddTask}>Add Task</button>
      </div>

      <div className="task-list">
        {tasks.map((task) => (
          <div className="task-item" key={task.todoId}>
            <span>{task.title}</span>
            <button onClick={() => handleDeleteTask(task.todoId)}>
              Delete
            </button>
          </div>
        ))}
      </div>

      <div className="pagination">
        <button
          onClick={() => setPage((prev) => Math.max(prev - 1, 1))}
          disabled={!hasPreviousPage}
          style={{
            backgroundColor: hasPreviousPage ? "#007bff" : "#d3d3d3",
            color: hasPreviousPage ? "#fff" : "#7a7a7a",
            cursor: hasPreviousPage ? "pointer" : "not-allowed",
          }}
        >
          Previous
        </button>
        <span>Page {page}</span>
        <button
          onClick={() => setPage((prev) => prev + 1)}
          disabled={!hasNextPage}
          style={{
            backgroundColor: hasNextPage ? "#007bff" : "#d3d3d3",
            color: hasNextPage ? "#fff" : "#7a7a7a",
            cursor: hasNextPage ? "pointer" : "not-allowed",
          }}
        >
          Next
        </button>
      </div>
    </div>
  );
};

export default App;
