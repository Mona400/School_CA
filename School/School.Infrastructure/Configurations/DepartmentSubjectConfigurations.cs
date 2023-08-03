using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Data.Entities;

namespace School.Infrastructure.Configurations
{
    public class DepartmentSubjectConfigurations : IEntityTypeConfiguration<DepartmentSubject>
    {
        public void Configure(EntityTypeBuilder<DepartmentSubject> builder)
        {
            builder.HasKey(x => new { x.SubID, x.DID });
            builder.HasOne(ds => ds.Department)
                .WithMany(d => d.DepartmentSubjects)
                .HasForeignKey(ds => ds.DID);

            builder.HasOne(ds => ds.Subject)
                .WithMany(d => d.DepartmentSubjects)
                .HasForeignKey(ds => ds.SubID);
        }
    }
}
