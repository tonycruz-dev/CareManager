using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Arias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Borough = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Occupation = table.Column<string>(nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    NickName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttributeDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttributeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShiftStates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShiftDetails = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hour = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Agencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ContactName = table.Column<string>(nullable: true),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    Address3 = table.Column<string>(nullable: true),
                    Address4 = table.Column<string>(nullable: true),
                    Address5 = table.Column<string>(nullable: true),
                    ContactNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    AccoutNumber = table.Column<string>(nullable: true),
                    AccoutName = table.Column<string>(nullable: true),
                    SortCode = table.Column<string>(nullable: true),
                    LogoUrl = table.Column<string>(nullable: true),
                    AppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agencies_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    Address3 = table.Column<string>(nullable: true),
                    Address4 = table.Column<string>(nullable: true),
                    Address5 = table.Column<string>(nullable: true),
                    ContactNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    AccoutNumber = table.Column<string>(nullable: true),
                    AccoutName = table.Column<string>(nullable: true),
                    SortCode = table.Column<string>(nullable: true),
                    PhotoUrl = table.Column<string>(nullable: true),
                    AppUserId = table.Column<string>(nullable: true),
                    GradeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidates_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Candidates_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientLocations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(nullable: true),
                    ManagerName = table.Column<string>(nullable: true),
                    ContactName = table.Column<string>(nullable: true),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    Address3 = table.Column<string>(nullable: true),
                    Address4 = table.Column<string>(nullable: true),
                    Address5 = table.Column<string>(nullable: true),
                    ContactNumber = table.Column<string>(nullable: true),
                    AgencyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientLocations_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobToRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberCandidate = table.Column<int>(nullable: false),
                    JobDateStart = table.Column<DateTime>(nullable: false),
                    JobDateEnd = table.Column<DateTime>(nullable: false),
                    StartTime = table.Column<string>(nullable: true),
                    EndTime = table.Column<string>(nullable: true),
                    NumberApplied = table.Column<int>(nullable: false),
                    TimeDetailId = table.Column<int>(nullable: false),
                    JobTypeId = table.Column<int>(nullable: false),
                    AgencyId = table.Column<int>(nullable: false),
                    GradeId = table.Column<int>(nullable: false),
                    ClientLocationId = table.Column<int>(nullable: false),
                    AttributeDetailId = table.Column<int>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true),
                    ShiftStateId = table.Column<int>(nullable: false),
                    PaymentTypeId = table.Column<int>(nullable: false),
                    AriaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobToRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobToRequests_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobToRequests_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobToRequests_Arias_AriaId",
                        column: x => x.AriaId,
                        principalTable: "Arias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobToRequests_AttributeDetails_AttributeDetailId",
                        column: x => x.AttributeDetailId,
                        principalTable: "AttributeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobToRequests_ClientLocations_ClientLocationId",
                        column: x => x.ClientLocationId,
                        principalTable: "ClientLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobToRequests_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobToRequests_JobTypes_JobTypeId",
                        column: x => x.JobTypeId,
                        principalTable: "JobTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobToRequests_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobToRequests_ShiftStates_ShiftStateId",
                        column: x => x.ShiftStateId,
                        principalTable: "ShiftStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobToRequests_TimeDetails_TimeDetailId",
                        column: x => x.TimeDetailId,
                        principalTable: "TimeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvitedCandidates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsSelected = table.Column<bool>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    Accept = table.Column<bool>(nullable: true),
                    Reject = table.Column<bool>(nullable: true),
                    PublishDate = table.Column<DateTime>(nullable: false),
                    PublishTime = table.Column<string>(nullable: true),
                    ResponseDate = table.Column<DateTime>(nullable: true),
                    RespondTime = table.Column<string>(nullable: true),
                    JobDate = table.Column<DateTime>(nullable: false),
                    StartTime = table.Column<string>(nullable: true),
                    EndTime = table.Column<string>(nullable: true),
                    JobAddress = table.Column<string>(nullable: true),
                    TimeDetailId = table.Column<int>(nullable: false),
                    PaymentTypeId = table.Column<int>(nullable: false),
                    CandidateId = table.Column<int>(nullable: false),
                    AgencyId = table.Column<int>(nullable: false),
                    JobToRequestId = table.Column<int>(nullable: false),
                    GradeId = table.Column<int>(nullable: false),
                    ClientLocationId = table.Column<int>(nullable: false),
                    ShiftStateId = table.Column<int>(nullable: false),
                    AppUserPostedId = table.Column<string>(nullable: true),
                    AppUserCandidateId = table.Column<string>(nullable: true),
                    AriaId = table.Column<int>(nullable: false),
                    AttributeDetailId = table.Column<int>(nullable: false),
                    JobTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvitedCandidates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvitedCandidates_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvitedCandidates_AspNetUsers_AppUserCandidateId",
                        column: x => x.AppUserCandidateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvitedCandidates_AspNetUsers_AppUserPostedId",
                        column: x => x.AppUserPostedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvitedCandidates_Arias_AriaId",
                        column: x => x.AriaId,
                        principalTable: "Arias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvitedCandidates_AttributeDetails_AttributeDetailId",
                        column: x => x.AttributeDetailId,
                        principalTable: "AttributeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvitedCandidates_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvitedCandidates_ClientLocations_ClientLocationId",
                        column: x => x.ClientLocationId,
                        principalTable: "ClientLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvitedCandidates_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvitedCandidates_JobToRequests_JobToRequestId",
                        column: x => x.JobToRequestId,
                        principalTable: "JobToRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvitedCandidates_JobTypes_JobTypeId",
                        column: x => x.JobTypeId,
                        principalTable: "JobTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvitedCandidates_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvitedCandidates_ShiftStates_ShiftStateId",
                        column: x => x.ShiftStateId,
                        principalTable: "ShiftStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvitedCandidates_TimeDetails_TimeDetailId",
                        column: x => x.TimeDetailId,
                        principalTable: "TimeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobConfirmeds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradeDetails = table.Column<string>(nullable: true),
                    PhotoUrl = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    JobDate = table.Column<DateTime>(nullable: false),
                    StartTime = table.Column<string>(nullable: true),
                    EndTime = table.Column<string>(nullable: true),
                    JobAddress = table.Column<string>(nullable: true),
                    PaymentDescription = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Rating = table.Column<int>(nullable: false),
                    FinishShift = table.Column<bool>(nullable: true),
                    LostShift = table.Column<bool>(nullable: true),
                    TimeDetailId = table.Column<int>(nullable: false),
                    PaymentTypeId = table.Column<int>(nullable: false),
                    CandidateId = table.Column<int>(nullable: false),
                    AgencyId = table.Column<int>(nullable: false),
                    JobToRequestId = table.Column<int>(nullable: false),
                    GradeId = table.Column<int>(nullable: false),
                    ClientLocationId = table.Column<int>(nullable: false),
                    ShiftStateId = table.Column<int>(nullable: false),
                    AppUserPostedId = table.Column<string>(nullable: true),
                    AppUserCandidateId = table.Column<string>(nullable: true),
                    AriaId = table.Column<int>(nullable: false),
                    AttributeDetailId = table.Column<int>(nullable: false),
                    JobTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobConfirmeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobConfirmeds_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobConfirmeds_AspNetUsers_AppUserCandidateId",
                        column: x => x.AppUserCandidateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobConfirmeds_AspNetUsers_AppUserPostedId",
                        column: x => x.AppUserPostedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobConfirmeds_Arias_AriaId",
                        column: x => x.AriaId,
                        principalTable: "Arias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobConfirmeds_AttributeDetails_AttributeDetailId",
                        column: x => x.AttributeDetailId,
                        principalTable: "AttributeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobConfirmeds_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobConfirmeds_ClientLocations_ClientLocationId",
                        column: x => x.ClientLocationId,
                        principalTable: "ClientLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobConfirmeds_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobConfirmeds_JobToRequests_JobToRequestId",
                        column: x => x.JobToRequestId,
                        principalTable: "JobToRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobConfirmeds_JobTypes_JobTypeId",
                        column: x => x.JobTypeId,
                        principalTable: "JobTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobConfirmeds_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobConfirmeds_ShiftStates_ShiftStateId",
                        column: x => x.ShiftStateId,
                        principalTable: "ShiftStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobConfirmeds_TimeDetails_TimeDetailId",
                        column: x => x.TimeDetailId,
                        principalTable: "TimeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agencies_AppUserId",
                table: "Agencies",
                column: "AppUserId",
                unique: true,
                filter: "[AppUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_AppUserId",
                table: "Candidates",
                column: "AppUserId",
                unique: true,
                filter: "[AppUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_GradeId",
                table: "Candidates",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientLocations_AgencyId",
                table: "ClientLocations",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_InvitedCandidates_AgencyId",
                table: "InvitedCandidates",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_InvitedCandidates_AppUserCandidateId",
                table: "InvitedCandidates",
                column: "AppUserCandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_InvitedCandidates_AppUserPostedId",
                table: "InvitedCandidates",
                column: "AppUserPostedId");

            migrationBuilder.CreateIndex(
                name: "IX_InvitedCandidates_AriaId",
                table: "InvitedCandidates",
                column: "AriaId");

            migrationBuilder.CreateIndex(
                name: "IX_InvitedCandidates_AttributeDetailId",
                table: "InvitedCandidates",
                column: "AttributeDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_InvitedCandidates_CandidateId",
                table: "InvitedCandidates",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_InvitedCandidates_ClientLocationId",
                table: "InvitedCandidates",
                column: "ClientLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_InvitedCandidates_GradeId",
                table: "InvitedCandidates",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_InvitedCandidates_JobToRequestId",
                table: "InvitedCandidates",
                column: "JobToRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_InvitedCandidates_JobTypeId",
                table: "InvitedCandidates",
                column: "JobTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InvitedCandidates_PaymentTypeId",
                table: "InvitedCandidates",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InvitedCandidates_ShiftStateId",
                table: "InvitedCandidates",
                column: "ShiftStateId");

            migrationBuilder.CreateIndex(
                name: "IX_InvitedCandidates_TimeDetailId",
                table: "InvitedCandidates",
                column: "TimeDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_JobConfirmeds_AgencyId",
                table: "JobConfirmeds",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_JobConfirmeds_AppUserCandidateId",
                table: "JobConfirmeds",
                column: "AppUserCandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_JobConfirmeds_AppUserPostedId",
                table: "JobConfirmeds",
                column: "AppUserPostedId");

            migrationBuilder.CreateIndex(
                name: "IX_JobConfirmeds_AriaId",
                table: "JobConfirmeds",
                column: "AriaId");

            migrationBuilder.CreateIndex(
                name: "IX_JobConfirmeds_AttributeDetailId",
                table: "JobConfirmeds",
                column: "AttributeDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_JobConfirmeds_CandidateId",
                table: "JobConfirmeds",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_JobConfirmeds_ClientLocationId",
                table: "JobConfirmeds",
                column: "ClientLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobConfirmeds_GradeId",
                table: "JobConfirmeds",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobConfirmeds_JobToRequestId",
                table: "JobConfirmeds",
                column: "JobToRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_JobConfirmeds_JobTypeId",
                table: "JobConfirmeds",
                column: "JobTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobConfirmeds_PaymentTypeId",
                table: "JobConfirmeds",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobConfirmeds_ShiftStateId",
                table: "JobConfirmeds",
                column: "ShiftStateId");

            migrationBuilder.CreateIndex(
                name: "IX_JobConfirmeds_TimeDetailId",
                table: "JobConfirmeds",
                column: "TimeDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_JobToRequests_AgencyId",
                table: "JobToRequests",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_JobToRequests_AppUserId",
                table: "JobToRequests",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobToRequests_AriaId",
                table: "JobToRequests",
                column: "AriaId");

            migrationBuilder.CreateIndex(
                name: "IX_JobToRequests_AttributeDetailId",
                table: "JobToRequests",
                column: "AttributeDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_JobToRequests_ClientLocationId",
                table: "JobToRequests",
                column: "ClientLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobToRequests_GradeId",
                table: "JobToRequests",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobToRequests_JobTypeId",
                table: "JobToRequests",
                column: "JobTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobToRequests_PaymentTypeId",
                table: "JobToRequests",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobToRequests_ShiftStateId",
                table: "JobToRequests",
                column: "ShiftStateId");

            migrationBuilder.CreateIndex(
                name: "IX_JobToRequests_TimeDetailId",
                table: "JobToRequests",
                column: "TimeDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "InvitedCandidates");

            migrationBuilder.DropTable(
                name: "JobConfirmeds");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "JobToRequests");

            migrationBuilder.DropTable(
                name: "Arias");

            migrationBuilder.DropTable(
                name: "AttributeDetails");

            migrationBuilder.DropTable(
                name: "ClientLocations");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "JobTypes");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "ShiftStates");

            migrationBuilder.DropTable(
                name: "TimeDetails");

            migrationBuilder.DropTable(
                name: "Agencies");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
