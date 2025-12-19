export interface User {
  userId: number;
  email: string;
  token: string;
}

export interface RegisterRequest {
  email: string;
  password: string;
  confirmPassword: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface AuthResponse {
  token: string;
  email: string;
  userId: number;
}

export interface FileInfo {
  id: number;
  originalFileName: string;
  sizeInBytes: number;
  contentType: string;
  uploadedAt: string;
}

export interface FileUploadResponse {
  id: number;
  originalFileName: string;
  sizeInBytes: number;
  contentType: string;
  uploadedAt: string;
}
