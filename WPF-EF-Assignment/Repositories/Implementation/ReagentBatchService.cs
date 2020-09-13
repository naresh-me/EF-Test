using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_EF_Assignment.Data;

namespace WPF_EF_Assignment.Repositories
{
    public class ReagentBatchService : IReagentBatchService
    {
        IUnitOfWork _unitOfWork;
        ILogger _logger;
     
        /// <summary>
        /// Reagent Batch Service constructor
        /// </summary>
        /// <param name="UnitOfWork"></param>
        public ReagentBatchService(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
            _logger = LogManager.GetCurrentClassLogger();
        }
       
        /// <summary>
        /// Insert a batch lot record from the system
        /// </summary>
        /// <param name="BatchLot"></param>
        /// <returns></returns>
        public bool Add(ReagentBatch BatchLot)
        {
            try
            {
                _unitOfWork.ReagentBatchRepository.Insert(BatchLot);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }
        /// <summary>
        /// Remove a batch lot record from the system
        /// </summary>
        /// <param name="BatchLot"></param>
        /// <returns></returns>
        public bool Delete(ReagentBatch BatchLot)
        {
            try
            {
                _unitOfWork.ReagentBatchRepository.Delete(BatchLot);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }
        /// <summary>
        /// Pull all the batch lot records from the system.
        /// </summary>
        /// <returns></returns>
        public List<ReagentBatch> GetAll()
        {
            try
            {
                var allRecords = _unitOfWork.ReagentBatchRepository.GetAll().ToList();
                return allRecords;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return null;
            }
        }
        /// <summary>
        /// Pull a batch lot record by it's key from the system
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ReagentBatch GetById(string Id)
        {
            try
            {
                var singleRecordById = _unitOfWork.ReagentBatchRepository.GetByID(Id);
                return singleRecordById;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return null;
            }
        }
        /// <summary>
        /// Update Batch Lot record
        /// </summary>
        /// <param name="BatchLot"></param>
        /// <returns></returns>
        public bool Update(ReagentBatch BatchLot)
        {
            try
            {
                _unitOfWork.ReagentBatchRepository.Update(BatchLot);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }
    }
}
