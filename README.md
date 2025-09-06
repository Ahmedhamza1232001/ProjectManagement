# ProjectManagement

ProjectManagement is a .NET-based project management application that helps teams organize projects, tasks, roles, and users. The application is containerized using Docker, making it easy to run locally or in any environment supporting Docker.

---

## Table of Contents

- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Running the Project](#running-the-project)
- [Stopping the Project](#stopping-the-project)
- [Project Structure](#project-structure)
- [API Endpoints](#api-endpoints)
- [Notes](#notes)

---

## Prerequisites

Before running the project, ensure the following tools are installed:

- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)
- [.NET SDK](https://dotnet.microsoft.com/en-us/download) (optional if building locally)

---

## Getting Started

1. Clone the repository:

```bash
git clone [<repository-url>](https://github.com/Ahmedhamza1232001/ProjectManagement)
cd ProjectManagement
cd docker
docker compose up --build -d
```
### The API will be available at:
http://localhost:5000
