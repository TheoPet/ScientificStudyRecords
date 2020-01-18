using System;
using System.Threading.Tasks;

namespace ScientificStudyWeb.Data
{
    public interface IUnitOfWork: IDisposable
    {
        void SaveChanges();
         Task SaveChangesAsync();
    }
}