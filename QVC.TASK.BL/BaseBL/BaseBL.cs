﻿using QVC.TASK.Common;
using QVC.TASK.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.BL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field

        private IBaseDL<T> _baseDL;

        #endregion

        #region Constractor

        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        
        #endregion

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách đối tượng bản ghi</returns>
        /// Created by: DungVV (28/03/2023)
        public List<T> GetAll(string domain)
        {
            return _baseDL.GetAll(domain);
        }

        /// <summary>
        /// Lấy danh sách bản ghi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="domaindb"></param>
        /// <returns></returns>
        public List<T> GetAllById(string id, string domaindb)
        {
            return _baseDL.GetAllById(id, domaindb);
        }

        public T GetById(string id, string domaindb)
        {
            return _baseDL.GetById(id, domaindb);
        }

        /// <summary>
        /// >Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="dataInsert"></param>
        /// <returns></returns>
        public int Insert(DataInsert<T> dataInsert)
        {
            return _baseDL.Insert(dataInsert);
        }

        public int Update(DataInsert<T> data)
        {
            return _baseDL.Update(data);
        }

        public int Delete(Guid id, string db)
        {
            return _baseDL.Delete(id,db);
        }

    }
}
