﻿using System;
using System.Collections.Generic;
using USP.ESI.SUEP.Dao;
using USP.ESI.SUEP.Lib.Model;

namespace USP.ESI.SUEP.Lib.Controller
{
    public class UserController
    {
        public List<User> Get(long _par_intLoggedUser)
        {
            var _objLstDatabaseUsers = new UserDAO(new EntidadesContext()).Get(_par_intLoggedUser);
            var _objLstUsers = new List<User>();

            foreach (var _objDatabaseUser in _objLstDatabaseUsers)
                _objLstUsers.Add(DaoToModel(_objDatabaseUser));

            return _objLstUsers;
        }

        public List<string> GetAllPacientsName()
        {
            var _objLstDatabaseUsers = new UserDAO(new EntidadesContext()).GetAllPacients();
            var _objLstUsers = new List<string>();

            foreach (var _objDatabaseUser in _objLstDatabaseUsers)
                _objLstUsers.Add(_objDatabaseUser.Name);

            return _objLstUsers;
        }

        public bool Delete(User user)
        {
            try
            {
                return new UserDAO(new EntidadesContext()).Delete(ModelToDao(user));
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        private TbSuep_User ModelToDao(User _parObjUserModel)
        {
            try
            {
                return new TbSuep_User()
                {
                    Id = _parObjUserModel.Id,
                    Name = _parObjUserModel.Name,
                    CPF = _parObjUserModel.CPF,
                    Id_User_Type = _parObjUserModel.IdAccessProfile,
                    FlActive = _parObjUserModel.Active,
                    Pass = _parObjUserModel.Pass,
                    Login = _parObjUserModel.Login,
                    HourPrice = _parObjUserModel.HourPrice
                };
            }
            catch(Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        private User DaoToModel(TbSuep_User _parObjDatabaseUser)
        {
            try
            {
                return new User(
                    _parObjDatabaseUser.Login,
                    _parObjDatabaseUser.Pass,
                    _parObjDatabaseUser.TbSuep_User_Type.UserType)
                {
                    Id = _parObjDatabaseUser.Id,
                    Name = _parObjDatabaseUser.Name,
                    CPF = _parObjDatabaseUser.CPF,
                    Active = _parObjDatabaseUser.FlActive,
                    HourPrice = _parObjDatabaseUser.HourPrice
                };
            }
            catch(Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        public bool Save(User _parObjUserModel)
        {
            try
            {
                var _objDao = ModelToDao(_parObjUserModel);

                if (_parObjUserModel.Id > 0)
                    return new UserDAO(new EntidadesContext()).Update(_objDao);

                return new UserDAO(new EntidadesContext()).Add(_objDao);
            }
            catch(Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }
    }
}
