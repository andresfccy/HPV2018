using HPV_Datos.Reporte;
using HPV_Entidades.Reporte;
using HPV_Entidades.ReporteWS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;

namespace HPV_Servicios.Reportes.Trazabilidad
{
    public partial class RptTrazabilidad : System.Web.UI.Page
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

                String rutaPlantilla = String.Format("{0}/trazabilidad.xlsx", pathPlantilla) ;
                String rutaRpt = String.Format("{0}/trazabilidad.xlsx", pathTmp);

                ManejadorExcel rpt = new ManejadorExcel();

                rpt.Abrir(rutaPlantilla);

                OE_RptTrazabilidad  oe = new OE_RptTrazabilidad();
                oe.ParametroRpt.IdPeriodo = Int64.Parse(idPeriodo);
                oe.ParametroRpt.IdUsuario = Int64.Parse(idUsuario == null ? "0" : idUsuario);
                oe.ParametroRpt.IdFacilitador = Int64.Parse(idFacilitador);
                oe.ParametroRpt.IdCoordinador = Int64.Parse(idCoordinador);
                oe.ParametroRpt.IdGrupo = Int64.Parse(idGrupo);
                oe.ParametroRpt.IdDepartamento = Int64.Parse(idDepartamento);
                oe.ParametroRpt.IdMunicipio = Int64.Parse(idMunicipio);
                oe.ParametroRpt.FechaCorte = FechaCorte;

                OS_RptTrazabilidad os = new FachadaReporte().RptTrazabilidad(oe);

                if (os.Respuesta.Codigo == 0)
                {
                    Int32 fila = 13;
                    bool primeraFila = true;
                    foreach (HPV_Entidades.Reporte.Trazabilidad reg in os.ListaTrazabilidad)
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


                rpt.SalvarComo(rutaRpt);
                rpt.Cerrar();
				
				Thread.Sleep(2000);

                byte[] binaryRpt = File.ReadAllBytes(rutaRpt);


                Response.Clear();
                
                Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment;filename=Trazabilidadxpart-p" + idPeriodo + "-" + FechaCorte + ".xlsx");
                
                Response.Charset = "";
                Response.BinaryWrite(binaryRpt);
				Response.Flush(); 
				Response.End();
                


            }
            catch (Exception err)
            {
                Response.Clear();
                Response.Write("Genero error " + err.Message);

            }

        }
    }
}