using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class JobToRequestConfiguration : IEntityTypeConfiguration<JobToRequest>
    {
        public void Configure(EntityTypeBuilder<JobToRequest> builder)
        {
            builder.HasOne(b => b.Agency).WithMany().HasForeignKey(a => a.AgencyId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(b => b.AppUser).WithMany().HasForeignKey(a => a.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(b => b.Aria).WithMany().HasForeignKey(a => a.AriaId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(b => b.AttributeDetail).WithMany().HasForeignKey(a => a.AttributeDetailId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(b => b.ClientLocation).WithMany().HasForeignKey(a => a.ClientLocationId)
              .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(b => b.Grade).WithMany().HasForeignKey(a => a.GradeId)
             .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(b => b.JobType).WithMany().HasForeignKey(a => a.JobTypeId)
             .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(b => b.PaymentType).WithMany().HasForeignKey(a => a.PaymentTypeId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.ShiftState).WithMany().HasForeignKey(a => a.ShiftStateId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.TimeDetail).WithMany().HasForeignKey(a => a.TimeDetailId)
            .OnDelete(DeleteBehavior.Restrict);

        }
    }
}


