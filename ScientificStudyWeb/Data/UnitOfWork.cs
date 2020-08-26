using System.Threading.Tasks;
using ScientificStudyWeb.Data.Interfaces;

namespace ScientificStudyWeb.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ScientificStudiesRecordDbContext _context;

        public IExperimentRepository experimentRepository { get; }

        public IGroupRepository groupRepository { get; }

        public IStudyRepository studyRepository { get; }

        public ITaskRepository taskRepository { get; }

        public ITestSubjectRepository testSubjectRepository { get; }

        public IUserRepository userRepository { get; }

        public UnitOfWork(ScientificStudiesRecordDbContext context)
        {
            _context = context;

            experimentRepository = new ExperimentRepository(_context);
            groupRepository = new GroupRepository(_context);
            studyRepository = new StudyRepository(_context);
            taskRepository = new TaskRepository(_context);
            testSubjectRepository = new TestSubjectRepository(_context);
            userRepository = new UserRepository(_context);

        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}