using System;
using System.Threading.Tasks;

namespace ScientificStudyWeb.Data.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        void SaveChanges();
         Task SaveChangesAsync();
    }
}