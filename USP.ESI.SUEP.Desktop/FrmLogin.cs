﻿using System;
using System.Windows.Forms;
using USP.ESI.SUEP.Desktop.SessionInfos;
using USP.ESI.SUEP.Lib.Controller;
using USP.ESI.SUEP.Lib.Model;
using USP.ESI.SUEP.Lib.Model.Constants;

namespace USP.ESI.SUEP.Desktop
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void BtAuth_Click(object sender, EventArgs e)
        {
            try
            {
                var _bolValid = ValidaLogin();

                if(_bolValid)
                {
                    var _objUser = new User(TxtLogin.Text, TxtPass.Text);
                    var _objLoggedUser = new LoginController().Auth(_objUser);

                    LoggedUser.USER = _objLoggedUser;

                    Hide();
                    var _strUserType = _objLoggedUser.AccessProfile.GetUserTypeAsString();
                    switch(_strUserType)
                    {
                        case UserTypeConstants.ADMIN:
                            new FrmLoggedAdminIndex().Show();
                            break;
                        case UserTypeConstants.DOCTO:
                            new FrmAgenda().Show();
                            break;
                        default:
                            throw new Exception("Perfil de acesso não mapeado para acesso no sistema");
                    }
                }
            }
            catch(ArgumentException _excArgumentException)
            {
                MessageBox.Show(_excArgumentException.Message);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool ValidaLogin()
        {
            if (TxtLogin.Text.Length < 3 || TxtPass.Text.Length < 3)
                throw new ArgumentException("Login ou senha inválidos");

            return true;
        }
    }
}
