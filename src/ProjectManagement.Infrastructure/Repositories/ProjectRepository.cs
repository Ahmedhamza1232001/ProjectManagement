using ProjectManagement.Application.Abstractions.Repositories;
using ProjectManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ProjectManagementDbContext _db;

        public ProjectRepository(ProjectManagementDbContext db) => _db = db;

        public async Task AddAsync(Project project)
        {
            await _db.Projects.AddAsync(project);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Project project)
        {
            _db.Projects.Update(project);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Project project)
        {
            _db.Projects.Remove(project);
            await _db.SaveChangesAsync();
        }

        public async Task<Project?> GetByIdAsync(Guid id)
        {
            return await _db.Projects.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Project>> GetAllAsync()
        {
            return await _db.Projects.ToListAsync();
        }

        public async Task<List<Project>> SearchAsync(string keyword)
        {
            return await _db.Projects
                .Where(p => p.Name.Contains(keyword) || (p.Description ?? string.Empty).Contains(keyword))
                .ToListAsync();
        }


    }
}