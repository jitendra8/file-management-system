# File Management System

A secure full-stack file upload and management system built with .NET 10 backend and React frontend.

## 🚀 Features

### Backend (.NET 10 Web API)
- **3-Layer Architecture**: Clean separation of concerns (Controllers → Business → Repository)
- **JWT Authentication**: Secure user authentication with Bearer tokens
- **BCrypt Password Hashing**: Industry-standard password security
- **File Management**: Upload, download, list, and delete files
- **Owner-Only Access**: Users can only access their own files
- **SQLite Database**: Lightweight, file-based database with Entity Framework Core
- **UUID-Based File Names**: Prevents file name conflicts
- **File Validation**: Type and size restrictions
- **CORS Support**: Configured for frontend integration
- **Global Exception Handling**: Centralized error management

### Frontend (React + TypeScript + Vite)
- **Modern UI**: Built with Tailwind CSS
- **User Authentication**: Registration and login with JWT
- **File Upload**: Drag-and-drop support with progress indicators
- **File Management**: View, download, and delete files
- **Protected Routes**: Authentication-based access control
- **Responsive Design**: Works on all screen sizes
- **Error Handling**: User-friendly error messages
- **Type-Safe**: Full TypeScript implementation

## 📋 Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Node.js](https://nodejs.org/) (v18 or higher)
- npm or yarn

## 🛠️ Technology Stack

### Backend
- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- JWT Authentication
- BCrypt.Net-Next

### Frontend
- React 18
- TypeScript
- Vite
- React Router DOM
- Axios
- Tailwind CSS

## 📁 Project Structure

```
file-management-system/
├── backend/
│   ├── FileManagement.API/           # API Layer
│   │   ├── Controllers/
│   │   ├── Middleware/
│   │   ├── Program.cs
│   │   └── appsettings.json
│   ├── FileManagement.Business/      # Business Logic Layer
│   │   ├── Services/
│   │   ├── Interfaces/
│   │   └── DTOs/
│   └── FileManagement.Repository/    # Data Access Layer
│       ├── Data/
│       ├── Entities/
│       ├── Repositories/
│       └── Interfaces/
└── frontend/
    └── src/
        ├── components/
        ├── contexts/
        ├── hooks/
        ├── pages/
        ├── services/
        └── types/
```

## 🚀 Getting Started

### Backend Setup

1. **Navigate to the backend directory:**
   ```bash
   cd backend
   ```

2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

3. **Build the solution:**
   ```bash
   dotnet build
   ```

4. **Run the API:**
   ```bash
   cd FileManagement.API
   dotnet run
   ```

   The API will be available at `http://localhost:5249`

### Frontend Setup

1. **Navigate to the frontend directory:**
   ```bash
   cd frontend
   ```

2. **Install dependencies:**
   ```bash
   npm install
   ```

3. **Configure environment variables:**
   
   Create a `.env` file (or copy from `.env.example`):
   ```
   VITE_API_URL=http://localhost:5249/api
   ```

4. **Start the development server:**
   ```bash
   npm run dev
   ```

   The frontend will be available at `http://localhost:5173`

## 📚 API Documentation

### Authentication Endpoints

#### Register User
```http
POST /api/auth/register
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "YourPassword123",
  "confirmPassword": "YourPassword123"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "email": "user@example.com",
  "userId": 1
}
```

#### Login
```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "YourPassword123"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "email": "user@example.com",
  "userId": 1
}
```

### File Management Endpoints

All file endpoints require authentication. Include the JWT token in the Authorization header:
```
Authorization: Bearer {your-token}
```

#### Upload File
```http
POST /api/files/upload
Content-Type: multipart/form-data
Authorization: Bearer {token}

file: [binary file data]
```

**Response:**
```json
{
  "id": 1,
  "originalFileName": "document.pdf",
  "sizeInBytes": 1024000,
  "contentType": "application/pdf",
  "uploadedAt": "2025-12-19T13:00:00Z"
}
```

#### List Files
```http
GET /api/files
Authorization: Bearer {token}
```

**Response:**
```json
[
  {
    "id": 1,
    "originalFileName": "document.pdf",
    "sizeInBytes": 1024000,
    "contentType": "application/pdf",
    "uploadedAt": "2025-12-19T13:00:00Z"
  }
]
```

#### Download File
```http
GET /api/files/{id}/download
Authorization: Bearer {token}
```

Returns the file as a binary stream.

#### Delete File
```http
DELETE /api/files/{id}
Authorization: Bearer {token}
```

**Response:** `204 No Content`

## ⚙️ Configuration

### Backend Configuration (appsettings.json)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=filemanagement.db"
  },
  "Jwt": {
    "Secret": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
    "Issuer": "FileManagementAPI",
    "Audience": "FileManagementClient"
  },
  "Cors": {
    "AllowedOrigins": "http://localhost:5173,http://localhost:3000"
  },
  "FileStorage": {
    "UploadPath": "uploads"
  },
  "FileUpload": {
    "MaxFileSizeMB": 10,
    "AllowedExtensions": [
      ".jpg", ".jpeg", ".png", ".gif", ".webp",
      ".pdf", ".doc", ".docx", ".txt",
      ".zip", ".csv", ".xlsx"
    ]
  }
}
```

### Frontend Configuration (.env)

```
VITE_API_URL=http://localhost:5249/api
```

## 🔒 Security Features

- **JWT Bearer Token Authentication**: Secure token-based authentication
- **BCrypt Password Hashing**: Passwords are hashed using BCrypt
- **Owner-Only File Access**: Users can only access their own files
- **File Type Validation**: Whitelist of allowed file extensions
- **File Size Limits**: Configurable maximum file size (default 10MB)
- **CORS Protection**: Configured allowed origins
- **Exception Handling**: Sensitive errors are not exposed to clients

## 🧪 Testing the Application

1. **Start the backend** (port 5249)
2. **Start the frontend** (port 5173)
3. **Register a new user** at `http://localhost:5173/register`
4. **Login** with your credentials
5. **Upload files** using drag-and-drop or file browser
6. **View, download, and delete** your files from the dashboard

## 📝 Development Notes

### Database Migrations

The application automatically applies migrations on startup. To create a new migration:

```bash
cd backend/FileManagement.API
dotnet ef migrations add MigrationName --project ../FileManagement.Repository/FileManagement.Repository.csproj
```

### Building for Production

**Backend:**
```bash
cd backend
dotnet publish -c Release
```

**Frontend:**
```bash
cd frontend
npm run build
```

The production build will be in the `dist` folder.

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## 📄 License

This project is licensed under the MIT License.

## 👥 Authors

- Your Name - Initial work

## 🙏 Acknowledgments

- ASP.NET Core Team
- React Team
- Tailwind CSS Team
