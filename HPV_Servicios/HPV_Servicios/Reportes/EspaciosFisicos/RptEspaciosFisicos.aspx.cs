using HPV_Datos.Reporte;
using HPV_Entidades.ReporteWS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HPV_Servicios.Reportes.EspaciosFisicos
{
    public partial class RptEspaciosFisicos : System.Web.UI.Page
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

                String rutaPlantilla = String.Format("{0}/espaciosFisicos.xlsx", pathPlantilla);
                String rutaRpt = String.Format("{0}/espaciosFisicos.xlsx", pathTmp);

                ManejadorExcel rpt = new ManejadorExcel();

                rpt.Abrir(rutaPlantilla);

                OE_RptEspaciosFisicos oe = new OE_RptEspaciosFisicos();
                oe.ParametroRpt.IdPeriodo = Int64.Parse(idPeriodo);
                oe.ParametroRpt.IdUsuario = Int64.Parse(idUsuario == null ? "0" : idUsuario);
                oe.ParametroRpt.IdFacilitador = Int64.Parse(idFacilitador == null ? "0" : idFacilitador);
                oe.ParametroRpt.IdCoordinador = Int64.Parse(idCoordinador == null ? "0" : idCoordinador);
                oe.ParametroRpt.IdGrupo = Int64.Parse(idGrupo == null ? "0" : idGrupo);
                oe.ParametroRpt.IdDepartamento = Int64.Parse(idDepartamento == null ? "0" : idDepartamento);
                oe.ParametroRpt.IdMunicipio = Int64.Parse(idMunicipio == null ? "0" : idMunicipio);
                oe.ParametroRpt.FechaCorte = FechaCorte;

                OS_RptEspaciosFisicos os = new FachadaReporte().RptEspaciosFisicos(oe);

                if (os.Respuesta.Codigo == 0)
                {
                    Int32 fila = 10;
                    bool primeraFila = true;
                    foreach (HPV_Entidades.Reporte.EspaciosFisicos reg in os.ListaEspaciosFisicos)
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

                byte[] binaryRpt = File.ReadAllBytes(rutaRpt);


                Response.Clear();

                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=EspaciosFisicos-p" + idPeriodo + "-" + FechaCorte + ".xlsx");

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