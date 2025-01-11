# Todo List Application

## Step 2: Backend Setup

### Navigate to the backend folder:
```bash
cd Backend/TodoListAPI
```

### Update the connection string in `appsettings.json` to point to your SQL Server instance. Example:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TodoListDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

### Apply database migrations and update the database schema:
```bash
dotnet ef database update
```

### Start the backend server:
```bash
dotnet run
```
The backend server will run on [https://localhost:7295](https://localhost:7295).

---

## Step 3: Frontend Setup

### Navigate to the frontend folder:
```bash
cd ../../frontend
```

### Install dependencies:
```bash
npm install
```

### Start the development server:
```bash
npm start
```

---

## API Endpoints

- **GET /tasks**: Retrieve all tasks with pagination.
- **POST /tasks**: Add a new task. Requires a title in the request body.
- **DELETE /tasks/{id}**: Delete a task by its ID.

---

## Unit Tests

Unit tests are implemented for the backend. To run the tests:

### Navigate to the backend folder:
```bash
cd Backend/TodoListAPI.Tests
```

### Run the tests:
```bash
dotnet test
```

---

## Git Workflow

The repository follows a structured Git workflow:

- **main branch**: Contains production-ready code.
- **develop branch**: Contains the latest development updates.
- All features and fixes are committed with descriptive messages. Example:
  ```bash
  git commit -m "Added pagination to tasks API"
  ```

---

## Additional Notes

- The application supports pagination, showing 5 tasks per page.
- Validation ensures that task titles are at least 2 characters long.
- User-friendly error messages are displayed for any API errors.

---

