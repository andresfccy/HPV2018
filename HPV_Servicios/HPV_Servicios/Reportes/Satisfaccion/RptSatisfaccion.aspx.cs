﻿using HPV_Datos.Reporte;
using HPV_Entidades.Reporte;
using HPV_Entidades.ReporteWS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HPV_Servicios.Reportes.Satisfaccion
{
    public partial class RptSatisfaccion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                String idPeriodo = Request.QueryString["IdPeriodo"];

                if (idPeriodo == null)
                    return;

                String idUsuario = Request.QueryString["IdUsuario"];
                String idCoordinador = Request.QueryString["IdCoordinador"];
                String idFacilitador = Request.QueryString["IdFacilitador"];
                String idDepartamento = Request.QueryString["IdDepartamento"];
                String idGrupo = Request.QueryString["IdGrupo"];
                String idMunicipio = Request.QueryString["IdMunicipio"];
                String FechaCorte = Request.QueryString["FechaCorte"];

                String pathPlantilla = System.Configuration.ConfigurationManager.AppSettings["pathPlantilla"];
                String pathTmp = System.Configuration.ConfigurationManager.AppSettings["pathTemp"];

                if (!Directory.Exists(pathTmp))
                    Directory.CreateDirectory(pathTmp);

                String rutaPlantilla = String.Format("{0}/encuestaSatisfaccion.xlsx", pathPlantilla);
                String rutaRpt = String.Format("{0}/encuestaSatisfaccion.xlsx", pathTmp);

                ManejadorExcel rpt = new ManejadorExcel();

                rpt.Abrir(rutaPlantilla);

                OE_RptSatisfaccion oe = new OE_RptSatisfaccion();
                oe.ParametroRpt.IdPeriodo = Int64.Parse(idPeriodo);
                oe.ParametroRpt.IdUsuario = Int64.Parse(idUsuario == null ? "0" : idUsuario);
                oe.ParametroRpt.IdFacilitador = Int64.Parse(idFacilitador);
                oe.ParametroRpt.IdCoordinador = Int64.Parse(idCoordinador);
                oe.ParametroRpt.IdGrupo = Int64.Parse(idGrupo);
                oe.ParametroRpt.IdDepartamento = Int64.Parse(idDepartamento);
                oe.ParametroRpt.IdMunicipio = Int64.Parse(idMunicipio);
                oe.ParametroRpt.FechaCorte = FechaCorte;

                OE_RptSatisfaccionObservaciones oeObs = new OE_RptSatisfaccionObservaciones();
                oeObs.ParametroRpt = oe.ParametroRpt;

                OS_RptSatisfaccion os = new FachadaReporte().RptEncuestaSatisfaccion(oe);
                OS_RptSatisfaccionObservaciones osObs = new FachadaReporte().RptEncuestaSatisfaccionObservaciones(oeObs);

                if (os.Respuesta.Codigo == 0)
                {
                    Int32 fila = 10;
                    bool primeraFila = true;
                    foreach (HPV_Entidades.Reporte.EncuestaSatisfaccion reg in os.ListaSatisfaccion)
                    {
                        Int32 colum = 2;
                        var i = 0;
                        if (primeraFila)
                        {
                            foreach (var att in reg.NomAtributos)
                            {
                                rpt.Escribir("detalle", fila, colum++, reg.NomAtributos[i]);
                                i++;
                            }
                            fila++;
                            i = 0;
                            colum = 2;
                            foreach (var att in reg.Atributos)
                            {
                                rpt.Escribir("detalle", fila, colum++, reg.Atributos[i]);
                                i++;
                            }
                            primeraFila = false;
                        }
                        else
                        {
                            foreach (var att in reg.Atributos)
                            {
                                rpt.Escribir("detalle", fila, colum++, reg.Atributos[i]);
                                i++;
                            }
                        }
                        fila++;
                    }
                }

                if (osObs.Respuesta.Codigo == 0)
                {
                    Int32 fila = 10;
                    bool primeraFila = true;
                    foreach (HPV_Entidades.Reporte.EncuestaSatisfaccionObservaciones reg in osObs.ListaSatisfaccionObservaciones)
                    {
                        Int32 colum = 2;
                        var i = 0;
                        if (primeraFila)
                        {
                            foreach (var att in reg.NomAtributos)
                            {
                                rpt.Escribir("Observaciones", fila, colum++, reg.NomAtributos[i]);
                                i++;
                            }
                            fila++;
                            i = 0;
                            colum = 2;
                            foreach (var att in reg.Atributos)
                            {
                                rpt.Escribir("Observaciones", fila, colum++, reg.Atributos[i]);
                                i++;
                            }
                            primeraFila = false;
                        }
                        else
                        {
                            foreach (var att in reg.Atributos)
                            {
                                rpt.Escribir("Observaciones", fila, colum++, reg.Atributos[i]);
                                i++;
                            }
                        }
                        fila++;
                    }
                }

                rpt.SalvarComo(rutaRpt);
                rpt.Cerrar();

                byte[] binaryRpt = File.ReadAllBytes(rutaRpt);


                Response.Clear();

                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                Response.AddHeader("Content-Disposition", "attachment;filename=Satisfaccion-p" + idPeriodo + "-" + FechaCorte + ".xlsx");

                Response.Charset = "";
                Response.BinaryWrite(binaryRpt);
                Response.Flush();

                Response.End();


            }
            catch (Exception err)
            {


            }
        }
    }
}