using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_EF_Assignment.Data;

namespace WPF_EF_Assignment.Repositories
{
    public class ReagentLotService : IReagentLotService
    {
        IUnitOfWork _unitOfWork;
        ILogger _logger;
      
        /// <summary>
        /// Reagent Lot Service Constructor
        /// </summary>
        /// <param name="UnitOfWork"></param>
        public ReagentLotService(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
            _logger = LogManager.GetCurrentClassLogger();
        }
       
        /// <summary>
        /// Insert a lot record into system
        /// </summary>
        /// <param name="Lot"></param>
        /// <returns></returns>
        public bool Add(ReagentLot Lot)
        {
            try
            {
                _unitOfWork.ReagentLotRepository.Insert(Lot);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }
        /// <summary>
        /// Remove a lot record form the system
        /// </summary>
        /// <param name="Lot"></param>
        /// <returns></returns>
        public bool Delete(ReagentLot Lot)
        {
            try
            {
                _unitOfWork.ReagentLotRepository.Delete(Lot);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }
        /// <summary>
        /// Pull lot records from the system
        /// </summary>
        /// <returns></returns>
        public List<ReagentLot> GetAll()
        {
            try
            {
                var allRecords = _unitOfWork.ReagentLotRepository.GetAll().ToList();
                return allRecords;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return null;
            }
        }
        /// <summary>
        /// Pull the Lot record by it's key from the system
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ReagentLot GetById(string Id)
        {
            try
            {
                var singleRecordById = _unitOfWork.ReagentLotRepository.GetByID(Id);
                return singleRecordById;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return null;
            }
        }
        /// <summary>
        /// Update the lot record
        /// </summary>
        /// <param name="Lot"></param>
        /// <returns></returns>
        public bool Update(ReagentLot Lot)
        {
            try
            {
                _unitOfWork.ReagentLotRepository.Update(Lot);
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
