﻿using HPV_Entidades.HistoriasDeVida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.HistoriasDeVidaWS
{
    public class OE_ActualizarHistoriaVida
    {
        public Int64 IdUsuario { get; set; } // Id del usuario logeado
        public HistoriaDeVida HistoriaDeVida { get; set; }
    }
}