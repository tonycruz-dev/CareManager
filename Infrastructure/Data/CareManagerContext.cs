using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data
{
    public class CareManagerContext : IdentityDbContext<AppUser>
    {
        public CareManagerContext(DbContextOptions<CareManagerContext> options) : base(options)
        { 
        }

        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Aria> Arias { get; set; }
        public DbSet<AttributeDetail> AttributeDetails { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<ClientLocation> ClientLocations { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<InvitedCandidate> InvitedCandidates { get; set; }
        public DbSet<JobConfirmed> JobConfirmeds { get; set; }
        public DbSet<JobType> JobTypes { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }

        public DbSet<ShiftState> ShiftStates { get; set; }
        public DbSet<TimeDetail> TimeDetails { get; set; }

        public DbSet<JobToRequest> JobToRequests { get; set; }
        public DbSet<AgencyPhoto> AgencyPhotos { get; set; }
        public DbSet<AgencyDocument> AgencyDocuments { get; set; }
        public DbSet<CandidatePhoto> CandidatePhotos { get; set; }
        public DbSet<CandidateDocument> CandidateDocuments { get; set; }
         public DbSet<HRStaff> HRStaffs { get; set; }
         public DbSet<StaffPhoto> StaffPhotos { get; set; }

        //

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

    }
}
