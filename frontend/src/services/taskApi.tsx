import axios from "axios";
import { ApiResponse, PageResultResponse } from "../models/response";
import { TodoCreateDto, TodoGetDto } from "../models/todo";

const API_URL = "https://localhost:7295/api/tasks";


export const getTasks = async (
  page: number,
  pageSize: number
): Promise<PageResultResponse<TodoGetDto>> => {
  try {
    const response = await axios.get<PageResultResponse<TodoGetDto>>(
      `${API_URL}?pageNumber=${page}&pageSize=${pageSize}`
    );
    return response.data;
  } catch (error: any) {
    console.error("Failed to fetch tasks:", error.response?.data || error.message);
    throw new Error(error.response?.data?.errorMessage || "Failed to fetch tasks");
  }
};

export const createTask = async (todo: TodoCreateDto) => {
  try {
    const response = await axios.post<TodoGetDto>(API_URL, todo);
    return response.data;
  } catch (error: any) {
    console.error("Failed to create task:", error.response?.data || error.message);

    // Додаємо деталі помилок, які повернув сервер
    throw {
      message: error.response?.data?.title || "Failed to create task",
      details: error.response?.data?.errors || null,
    };
  }
};


export const deleteTask = async (id: number): Promise<ApiResponse<boolean>> => {
  try {
    const response = await axios.delete<ApiResponse<boolean>>(`${API_URL}/${id}`);
    return response.data;
  } catch (error: any) {
    console.error("Failed to delete task:", error.response?.data || error.message);
    throw new Error(error.response?.data?.errorMessage || "Failed to delete task");
  }
};
