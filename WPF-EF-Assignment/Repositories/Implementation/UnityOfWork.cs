using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_EF_Assignment.Data;

namespace WPF_EF_Assignment.Repositories
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        #region Private member variables...
        AppDbContext _context;
        Logger _logger;
        GenericRepository<ReagentBatch> _ReagentBatchRepository;
        GenericRepository<ReagentLot> _ReagentLotRepository;
        #endregion
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            _logger = LogManager.GetCurrentClassLogger();
        }

        #region Public Repository Creation properties...
        /// <summary>
        /// Get/Set property for Reagent batch repository
        /// </summary>
        public GenericRepository<ReagentBatch> ReagentBatchRepository
        {
            get
            {
                if (this._ReagentBatchRepository == null)
                {
                    this._ReagentBatchRepository = new GenericRepository<ReagentBatch>(_context);
                }
                return this._ReagentBatchRepository;
            }
        }

        /// <summary>
        /// Get/Set property for Reagent Lot repository
        /// </summary>
        public GenericRepository<ReagentLot> ReagentLotRepository
        {
            get
            {
                if (this._ReagentLotRepository == null)
                {
                    this._ReagentLotRepository = new GenericRepository<ReagentLot>(_context);
                }
                return this._ReagentLotRepository;
            }
        }
        #endregion

        #region Public member methods...
        /// <summary>
        /// Save method.
        /// </summary>
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw ex;
            }
        }
        #endregion

        #region Implementing IDiosposable...

        #region private dispose variable declaration...
        private bool disposed = false;
        #endregion

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //_logger.Debug("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
