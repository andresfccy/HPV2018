﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.AsistenciaWS
{
    public class OE_DarAsistenciaEncabezado
    {
        public Int64 IdFacilitador { get; set; }
        public Int64 IdGrupo { get; set; }
        public Int64 IdTaller { get; set; }
        public Int64 IdPeriodo { get; set; }
    }
}