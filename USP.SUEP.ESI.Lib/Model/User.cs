﻿using System;
using USP.ESI.SUEP.Lib.Model.Constants;

namespace USP.ESI.SUEP.Lib.Model
{
    public class User
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public bool Active { get; set; }
        public IUserType AccessProfile { get; set; }
        public int IdAccessProfile { get; set; }
        public decimal? HourPrice { get; set; }

        public User()
        {
        }

        public User(string login, string pass)
        {
            Login = login;
            Pass = pass;
        }

        public User(string login, string pass, string user_type) : this(login, pass)
        {
            switch(user_type)
            {
                case UserTypeConstants.ADMIN:
                    AccessProfile = new Admin();
                    break;
                case UserTypeConstants.DOCTO:
                    AccessProfile = new Doctor();
                    break;
                case UserTypeConstants.PACIE:
                    AccessProfile = new Pacient();
                    break;
                default:
                    throw new ArgumentException("Perfil de acesso desconhecido");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(User))
                return false;

            return ((User)obj).Login == Login;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
