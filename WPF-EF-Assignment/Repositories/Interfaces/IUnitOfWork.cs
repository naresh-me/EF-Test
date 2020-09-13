using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_EF_Assignment.Data;

namespace WPF_EF_Assignment.Repositories
{
    public interface IUnitOfWork
    {
        GenericRepository<ReagentBatch> ReagentBatchRepository { get; }
        GenericRepository<ReagentLot> ReagentLotRepository { get; }
        void Save();
    }
}
