using Microsoft.EntityFrameworkCore;
using Project1.Models;
using Project1.Models.Enums;

namespace Project1.Data
{
    public class AllocationDbContext : DbContext
    {
        public AllocationDbContext(DbContextOptions<AllocationDbContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<HoursPerCreditForActivity> HoursPerCreditForActivity { get; set; }
        public DbSet<MinTeachingHoursByRole> MinTeachingHoursByRole { get; set; }
        public DbSet<AllocationCell> AllocationCells { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Lecturer>().ToTable("Lecturer");
            modelBuilder.Entity<HoursPerCreditForActivity>().ToTable("HoursPerCreditForActivity");
            modelBuilder.Entity<MinTeachingHoursByRole>().ToTable("MinTeachingHoursByRole");
            modelBuilder.Entity<AllocationCell>().ToTable("AllocationCells");

            modelBuilder.Entity<HoursPerCreditForActivity>(entity =>
            {
                entity.Property(e => e.Activity).IsRequired();
                entity.Property(e => e.NoOfHoursPerCredit).IsRequired();
                entity.Property(e => e.NoOfHoursPerEvent).IsRequired();
            });

            #region Seeding

            SeedMinTeachingHoursByRole(modelBuilder);
            SeedHoursPerCreditForActivity(modelBuilder);
            SeedCourses(modelBuilder);
            SeedLecturers(modelBuilder);

            #endregion
        }

        private void SeedMinTeachingHoursByRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MinTeachingHoursByRole>().HasData(new MinTeachingHoursByRole
            {
                Role = RoleType.HeadOfDepartment,
                MinNoOfHours = 180
            }, new MinTeachingHoursByRole
            {
                Role = RoleType.SeniorProfessor,
                MinNoOfHours = 300
            }, new MinTeachingHoursByRole
            {
                Role = RoleType.AssociateProfessor,
                MinNoOfHours = 360
            }, new MinTeachingHoursByRole
            {
                Role = RoleType.SeniorLecturerGradeIAndII,
                MinNoOfHours = 380
            }, new MinTeachingHoursByRole
            {
                Role = RoleType.Lecturer,
                MinNoOfHours = 450
            }, new MinTeachingHoursByRole
            {
                Role = RoleType.InstructorGradeI,
                MinNoOfHours = 480
            }, new MinTeachingHoursByRole
            {
                Role = RoleType.InstructorGradeII,
                MinNoOfHours = 510
            });
        }

        private void SeedHoursPerCreditForActivity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HoursPerCreditForActivity>().HasData(new HoursPerCreditForActivity
            {
                Activity = ActivityType.Lectures,
                NoOfHoursPerCredit = 15
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.Tutorials,
                NoOfHoursPerCredit = 15
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.LabClasses,
                NoOfHoursPerCredit = 30
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.ClinicalTeaching,
                NoOfHoursPerCredit = 30
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.GroupDiscussions,
                NoOfHoursPerCredit = 15
            },
            new HoursPerCreditForActivity
            {
                Activity = ActivityType.FieldWork,
                NoOfHoursPerCredit = 45
            },
            new HoursPerCreditForActivity
            {
                Activity = ActivityType.ResearchGrantInternational,
                NoOfHoursPerEvent = 80
            },
            new HoursPerCreditForActivity
            {
                Activity = ActivityType.ResearchGrantNational,
                NoOfHoursPerEvent = 40
            },
            new HoursPerCreditForActivity
            {
                Activity = ActivityType.MemberOfREseachTeam,
                NoOfHoursPerEvent = 30
            },
            new HoursPerCreditForActivity
            {
                Activity = ActivityType.RefereedJournal,
                NoOfHoursPerEvent = 50
            },
            new HoursPerCreditForActivity
            {
                Activity = ActivityType.RefereedIndexedJournal,
                NoOfHoursPerEvent = 20
            },
            new HoursPerCreditForActivity
            {
                Activity = ActivityType.ExtendedAbstracts,
                NoOfHoursPerEvent = 10
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.Abstracts,
                NoOfHoursPerEvent = 5
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.EditorInternationalJournal,
                NoOfHoursPerEvent = 100
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.MemberOfEditorialBoard,
                NoOfHoursPerEvent = 20
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.EditingOFCOllectionOfBooks,
                NoOfHoursPerEvent = 30
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.ConferenceCoordinatorNational,
                NoOfHoursPerEvent = 60
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.ConferenceCoordinatorInternational,
                NoOfHoursPerEvent = 50
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.WorkshopCoordinator,
                NoOfHoursPerEvent = 20
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.SupervisionOfResearchFullTime,
                NoOfHoursPerEvent = 60
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.SupervisionOfResearchPartTime,
                NoOfHoursPerEvent = 30
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.SupervisionOfResearch,
                NoOfHoursPerEvent = 30
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.AuthorOfBooksInternational,
                NoOfHoursPerEvent = 100
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.AuthorOfBooksNational,
                NoOfHoursPerEvent = 30
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.ReviewerOfResearchArticles,
                NoOfHoursPerEvent = 10
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.PromoterOfInstitutionalCollaborations,
                NoOfHoursPerEvent = 20
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.MemberOfProjects,
                NoOfHoursPerEvent = 20
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.AuthorOfPolicyPapers,
                NoOfHoursPerEvent = 50
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.KeynoteSpeeche,
                NoOfHoursPerEvent = 10
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.HeadOfDepartment,
                NoOfHoursPerEvent = 400
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.DirectorOfUniversityCenter,
                NoOfHoursPerEvent = 100
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.DeputyDirectorOfUniversityCenter,
                NoOfHoursPerEvent = 50
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.UniversityDisciplinaryMember,
                NoOfHoursPerEvent = 30
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.UniversityCommitteeChair,
                NoOfHoursPerEvent = 30
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.UniversityCommitteeMember,
                NoOfHoursPerEvent = 20
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.SenateStandingSubcommitteesMember,
                NoOfHoursPerEvent = 30
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.SenateExamOffenceInquiryCommittee,
                NoOfHoursPerEvent = 15
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.DirectorITCGU,
                NoOfHoursPerEvent = 100
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.DirectorOfCenter,
                NoOfHoursPerEvent = 50
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.FacultyStandingSubcommitteesChair,
                NoOfHoursPerEvent = 30
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.FacultyStandingSubcommitteesMember,
                NoOfHoursPerEvent = 20
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.FacultyInquiryOrganizing,
                NoOfHoursPerEvent = 15
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.PostgraduateCoordinator,
                NoOfHoursPerEvent = 30
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.DeptStandingSubcommitteeCoordinator,
                NoOfHoursPerEvent = 30
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.DeptStandingSubcommitteeMember,
                NoOfHoursPerEvent = 20
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.DeptEventOrganizing,
                NoOfHoursPerEvent = 15
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.DeputyProctor,
                NoOfHoursPerEvent = 60
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.StudentCounselor,
                NoOfHoursPerEvent = 20
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.StudentAcademicAdvisor,
                NoOfHoursPerEvent = 20
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.Warden,
                NoOfHoursPerEvent = 30
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.ProfessionalCommitteeMember,
                NoOfHoursPerEvent = 50
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.AcademicWriting,
                NoOfHoursPerEvent = 0
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.AcademicNetworking,
                NoOfHoursPerEvent = 0
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.StudentRelations,
                NoOfHoursPerEvent = 0
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.IndustryRelations,
                NoOfHoursPerEvent = 0
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.KnowledgeUpdating,
                NoOfHoursPerEvent = 0
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.ParticipationInConferences,
                NoOfHoursPerEvent = 0
            }, new HoursPerCreditForActivity
            {
                Activity = ActivityType.NationalServices,
                NoOfHoursPerEvent = 0
            });
        }

        private void SeedCourses(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Name = "CO221",
                    Credits = 3,
                    LectureHrs = 30,
                    PracticalHrs = 16,
                    AssignementHrs = 14,
                    CourseId = Guid.NewGuid()
                },
                new Course
                {
                    Name = "CO222",
                    Credits = 3,
                    LectureHrs = 24,
                    PracticalHrs = 14,
                    AssignementHrs = 12,
                    TutorialHrs = 8,
                    CourseId = Guid.NewGuid()
                }, new Course
                {
                    Name = "CO223",
                    Credits = 3,
                    LectureHrs = 30,
                    PracticalHrs = 22,
                    AssignementHrs = 4,
                    TutorialHrs = 2,
                    CourseId = Guid.NewGuid()
                },
                new Course
                {
                    Name = "EM201",
                    Credits = 3,
                    LectureHrs = 36,
                    TutorialHrs = 9,
                    CourseId = Guid.NewGuid()
                },
                new Course
                {
                    Name = "EM313",
                    Credits = 3,
                    LectureHrs = 36,
                    TutorialHrs = 9,
                    CourseId = Guid.NewGuid()
                },
                new Course
                {
                    Name = "EE282",
                    Credits = 3,
                    LectureHrs = 45,
                    CourseId = Guid.NewGuid()
                });
        }

        private void SeedLecturers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lecturer>().HasData(
                new Lecturer
                {
                    Name = "Roshan G Ragel",
                    Role = RoleType.HeadOfDepartment,
                    LecturerId = Guid.NewGuid()
                },
                new Lecturer
                {
                    Name = "abc1",
                    Role = RoleType.SeniorProfessor,
                    LecturerId = Guid.NewGuid()
                },
                new Lecturer
                {
                    Name = "abc2",
                    Role = RoleType.AssociateProfessor,
                    LecturerId = Guid.NewGuid()
                },
                new Lecturer
                {
                    Name = "abc3",
                    Role = RoleType.SeniorLecturerGradeIAndII,
                    LecturerId = Guid.NewGuid()
                },
                new Lecturer
                {
                    Name = "abc4",
                    Role = RoleType.Lecturer,
                    LecturerId = Guid.NewGuid()
                },
                new Lecturer
                {
                    Name = "abc5",
                    Role = RoleType.InstructorGradeI,
                    LecturerId = Guid.NewGuid()
                },
                new Lecturer
                {
                    Name = "abc6",
                    Role = RoleType.InstructorGradeII,
                    LecturerId = Guid.NewGuid()
                },
                new Lecturer
                {
                    Name = "abc7",
                    Role = RoleType.Lecturer,
                    LecturerId = Guid.NewGuid()
                });
        }
    }
}