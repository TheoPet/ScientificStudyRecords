using System;
using System.Threading.Tasks;

namespace ScientificStudyWeb.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IExperimentRepository experimentRepository { get; }
        IGroupRepository groupRepository { get; }
        IStudyRepository studyRepository { get; }
        ITaskRepository taskRepository { get; }
        ITestSubjectRepository testSubjectRepository { get; }

        int SaveChanges();
        Task SaveChangesAsync();
    }
}