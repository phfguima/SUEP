﻿using System;
using System.Collections.Generic;
using USP.ESI.SUEP.Dao;
using USP.ESI.SUEP.Lib.Model;

namespace USP.ESI.SUEP.Lib
{
    public class AgendaController
    {
        public List<Agenda> GetAgendaFrom(User _parObjLoggedUser)
        {
            try
            {
                var _objLstDatabaseAgendas = new AgendaDAO(new EntidadesContext()).Get(_parObjLoggedUser.Id);
                var _objLstAgendas = new List<Agenda>();
                
                foreach (var _objDatabaseAgenda in _objLstDatabaseAgendas)
                    _objLstAgendas.Add(DaoToModel(_objDatabaseAgenda));

                return _objLstAgendas;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private Agenda DaoToModel(TbSuep_Agenda _parObjDatabaseAgenda)
        {
            try
            {
                var _objAgenda = new Agenda()
                {
                    Id = _parObjDatabaseAgenda.Id,
                    DtBegin = _parObjDatabaseAgenda.Dt_Begin,
                    DtEnd = _parObjDatabaseAgenda.Dt_End,
                    Price = _parObjDatabaseAgenda.Price,
                    Payed = _parObjDatabaseAgenda.Fl_Payed,
                };

                _objAgenda.Doctor = new User()
                {
                    Id = _parObjDatabaseAgenda.Id_User_Doctor
                };

                _objAgenda.Pacient = new User()
                {
                    Id = _parObjDatabaseAgenda.Id_User_Pacient,
                    Name = _parObjDatabaseAgenda.TbSuep_User1.Name
                };

                return _objAgenda;
            }
            catch(Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        public void Add(Agenda _parObjAgenda)
        {
            try
            {
                var _objUserPacient = new UserDAO(new EntidadesContext()).GetUserWithTheName(_parObjAgenda.Pacient.Name);
                _parObjAgenda.Pacient.Id = _objUserPacient.Id;

                new AgendaDAO(new EntidadesContext()).Add(new TbSuep_Agenda()
                {
                    Dt_Begin = _parObjAgenda.DtBegin,
                    Dt_End = _parObjAgenda.DtEnd,
                    Price = _parObjAgenda.Price,
                    Fl_Payed = _parObjAgenda.Payed,
                    Id_User_Doctor = _parObjAgenda.Doctor.Id,
                    Id_User_Pacient = _parObjAgenda.Pacient.Id
                });
            }
            catch(Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        public bool Remove(Agenda _parObjAgenda)
        {
            try
            {
                var _objDatabaseAgenda = new TbSuep_Agenda()
                {
                    Id = _parObjAgenda.Id
                };

                return new AgendaDAO(new EntidadesContext()).Remove(_objDatabaseAgenda);
            }
            catch(Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        public bool Edit(Agenda _parObjAgenda)
        {
            try
            {
                var _objUserPacient = new UserDAO(new EntidadesContext()).GetUserWithTheName(_parObjAgenda.Pacient.Name);
                _parObjAgenda.Pacient.Id = _objUserPacient.Id;

                var _objDatabaseAgenda = new TbSuep_Agenda()
                {
                    Id = _parObjAgenda.Id,
                    Dt_Begin = _parObjAgenda.DtBegin,
                    Dt_End = _parObjAgenda.DtEnd,
                    Price = _parObjAgenda.Price,
                    Fl_Payed = _parObjAgenda.Payed,
                    Id_User_Doctor = _parObjAgenda.Doctor.Id,
                    Id_User_Pacient = _parObjAgenda.Pacient.Id
                };

                return new AgendaDAO(new EntidadesContext()).Edit(_objDatabaseAgenda);
            }
            catch(Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }
    }
}
