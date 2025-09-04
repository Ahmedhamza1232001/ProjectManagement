-- ================================
-- Seed Roles
-- ================================
INSERT INTO Roles (Id, Name)
VALUES 
    (NEWID(), 'Admin'),
    (NEWID(), 'Manager'),
    (NEWID(), 'User');

-- ================================
-- Seed Users
-- ================================
DECLARE @User1 UNIQUEIDENTIFIER = NEWID();
DECLARE @User2 UNIQUEIDENTIFIER = NEWID();

INSERT INTO Users (Id, Username, Email, PasswordHash)
VALUES 
    (@User1, 'admin_user', 'admin@example.com', 'hashed_password_here'),
    (@User2, 'normal_user', 'user@example.com', 'hashed_password_here');

-- ================================
-- Assign Roles to Users
-- Assuming many-to-many join table: RoleUser (RoleId, UserId)
-- ================================
-- Admin role → admin_user
INSERT INTO RoleUser (RolesId, UsersId)
SELECT TOP 1 Id, @User1 FROM Roles WHERE Name = 'Admin';

-- User role → normal_user
INSERT INTO RoleUser (RolesId, UsersId)
SELECT TOP 1 Id, @User2 FROM Roles WHERE Name = 'User';

-- ================================
-- Seed Projects
-- ================================
DECLARE @Project1 UNIQUEIDENTIFIER = NEWID();

INSERT INTO Projects (Id, Name, Description)
VALUES (@Project1, 'Project Alpha', 'First test project');

-- Assign users to project (ProjectsUsers join table)
INSERT INTO ProjectUser (ProjectsId, UsersId)
VALUES (@Project1, @User1), (@Project1, @User2);

-- ================================
-- Seed Tasks
-- ================================
DECLARE @Task1 UNIQUEIDENTIFIER = NEWID();
DECLARE @Task2 UNIQUEIDENTIFIER = NEWID();

INSERT INTO Tasks (Id, Title, Description, ProjectId)
VALUES 
    (@Task1, 'Setup environment', 'Prepare development environment', @Project1),
    (@Task2, 'Implement authentication', 'Build login & register feature', @Project1);

-- ================================
-- Seed Task Attachments (optional)
-- ================================
INSERT INTO TaskAttachments (Id, TaskId, FilePath)
VALUES 
    (NEWID(), @Task1, '/attachments/setup-guide.pdf'),
    (NEWID(), @Task2, '/attachments/auth-diagram.png');