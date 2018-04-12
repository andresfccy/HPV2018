﻿using HPV_Entidades.AdmUsuario;
using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AdmUsuarioWS
{
    public class OS_DarRoles
    {
        public Respuesta Respuesta { get; set; }
        public List<Rol> ListaRol { get; set; }

        public OS_DarRoles()
        {
            Respuesta = new Respuesta();
            ListaRol = new List<Rol>();
        }
    }
}
