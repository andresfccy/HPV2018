﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.Reporte
{
    public class EncuestaSatisfaccionObservaciones
    {
       
        public EncuestaSatisfaccionObservaciones()
        {
            Atributos = new List<String>();
            NomAtributos = new List<String>();
        }

        public List<String> Atributos;
        public List<String> NomAtributos;
    }
}
