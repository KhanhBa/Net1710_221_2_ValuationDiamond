﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using ValuationDiamond.Data.Models;
using ValuationDiamond.Data.Repository;

namespace ValuationDiamond.Data
{
    public class UnitOfWork
    {
        private Net1710_221_2_ValuationDiamondContext _unitOfWorkContext;
        private ValuationDiamondRepository _valuate;
        private CustomerRepository _customer;
        private ServiceRepository _service;
        private OrderRepository _order;
        private ValuationCertificateRepository _certificateRepository;
        private OrderDetailRepository _orderDetailRepository;

        public UnitOfWork()
        {
            _unitOfWorkContext ??= new Net1710_221_2_ValuationDiamondContext();
        }

        public ValuationDiamondRepository valuationDiamondRepository
        {
            get
            {
                return _valuate ??= new Repository.ValuationDiamondRepository(_unitOfWorkContext);
            }
        }
        public CustomerRepository CustomerRepository
        {
            get
            {
                return _customer ??= new Repository.CustomerRepository(_unitOfWorkContext);
            }
        }
        public ServiceRepository ServiceRepository
        {
            get
            {
                return _service ??= new Repository.ServiceRepository();
            }
        }
        public OrderRepository OrderRepository
        {
            get
            {
                return _order ??= new Repository.OrderRepository(_unitOfWorkContext);
            }
        }
        public ValuationCertificateRepository CertificateRepository
        {
            get
            {
                return _certificateRepository ??= new ValuationCertificateRepository(_unitOfWorkContext);
            }
        }
        public OrderDetailRepository OrderDetailRepository
        {
            get
            {
                return _orderDetailRepository ??= new OrderDetailRepository(_unitOfWorkContext);
            }
        }
        ////TO-DO CODE HERE/////////////////

        #region Set transaction isolation levels

        /*
        Read Uncommitted: The lowest level of isolation, allows transactions to read uncommitted data from other transactions. This can lead to dirty reads and other issues.

        Read Committed: Transactions can only read data that has been committed by other transactions. This level avoids dirty reads but can still experience other isolation problems.

        Repeatable Read: Transactions can only read data that was committed before their execution, and all reads are repeatable. This prevents dirty reads and non-repeatable reads, but may still experience phantom reads.

        Serializable: The highest level of isolation, ensuring that transactions are completely isolated from one another. This can lead to increased lock contention, potentially hurting performance.

        Snapshot: This isolation level uses row versioning to avoid locks, providing consistency without impeding concurrency. 
         */

        public int SaveChangesWithTransaction()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _unitOfWorkContext.Database.BeginTransaction())
            {
                try
                {
                    result = _unitOfWorkContext.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        public async Task<int> SaveChangesWithTransactionAsync()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _unitOfWorkContext.Database.BeginTransaction())
            {
                try
                {
                    result = await _unitOfWorkContext.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        #endregion
    }
}