using Microsoft.Extensions.DependencyInjection;
using School.Infrastructure.Abstracties;
using School.Infrastructure.InfrastructureBases;
using School.Infrastructure.Repositories;

namespace School.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentRepositories, StudentRepositories>();
            services.AddTransient<IDepartmentRepositories, DepartmentRepositories>();
            services.AddTransient<IInstructorRepositories, InstructorRepositories>();
            services.AddTransient<ISubjectRepositories, SubjectRepositories>();
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));

            return services;
        }
    }
}
