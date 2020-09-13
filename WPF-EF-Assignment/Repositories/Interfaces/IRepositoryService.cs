using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_EF_Assignment.Data;

namespace WPF_EF_Assignment.Repositories
{
    public interface IReagentBatchService 
    {
        List<ReagentBatch> GetAll();
        ReagentBatch GetById(string Id);
        bool Update(ReagentBatch LotBatch);
        bool Add(ReagentBatch LotBatch);
        bool Delete(ReagentBatch LotBatch);
    }
    public interface IReagentLotService 
    {
        List<ReagentLot> GetAll();
        ReagentLot GetById(string Id);
        bool Update(ReagentLot Lot);
        bool Add(ReagentLot Lot);
        bool Delete(ReagentLot Lot);
    }
}
