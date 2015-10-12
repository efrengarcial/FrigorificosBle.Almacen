using FrigorificosBle.Almacen.Core.Service;
using FrigorificosBle.Almacen.SPA.Models;
using log4net;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;

namespace FrigorificosBle.Almacen.SPA.Controllers
{
    [RoutePrefix("api/SSRSReporting")]
    public class SSRSReportingController : BaseApiController
    {

        private readonly IReportesService _reportesService;
        private readonly ILog _logger;


        public SSRSReportingController(IReportesService reportesService, ILog logger)
        {
            _reportesService = reportesService;
            _logger = logger;
        } 

        [HttpPost]
        [Route("saveReport")]
        public HttpResponseMessage SaveReport([FromBody]ReportDto reportDto)
        {
            byte[] bytes;
            using (var reportViewer = new ReportViewer())
            {
                var roles = this.Roles();
                reportViewer.ProcessingMode = ProcessingMode.Local;
                Assembly assembly = Assembly.GetAssembly(typeof(FrigorificosBle.Almacen.Reports.Class));
                Stream stream = assembly.GetManifestResourceStream("FrigorificosBle.Almacen.Reports." + 
                    reportDto.ResourcePath+ ".rdlc");
                reportViewer.LocalReport.LoadReportDefinition(stream);

                if (reportDto.Parameters != null)
                {
                    foreach (Parameter parameter in reportDto.Parameters)
                    {
                        // reportViewer.LocalReport.SetParameters(new ReportParameter(parameter.Name, parameter.Data));
                    }
                }

                if (reportDto.DataSources != null)
                {
                    /*var dt = new DataTable();

                    foreach (DataSource dataSource in reportDto.DataSources)
                    {                
                        dt.Columns.Add(dataSource.Name, typeof(string));
                    }

                    DataRow dr = dt.NewRow();

                    for (int index = 0; index < reportDto.DataSources.Count; index++)
                    {
                        dr[index] = reportDto.DataSources[index].Data;
                    }

                    dt.Rows.Add(dr);*/

                    var dataSource =  _reportesService.ConsultarInventarioFinal(DateTime.Now.AddDays(-100), DateTime.Now);


                    reportViewer.LocalReport.DataSources.Add(new ReportDataSource(reportDto.ReportDataSourceName, dataSource));
                }

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;

                bytes = reportViewer.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);
            }

            /*var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = string.Format("SampleReport.pdf"),
                Inline = true,
            };*/

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            Stream memoryStream = new MemoryStream(bytes);
            result.Content = new StreamContent(memoryStream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            return result;
        }
    }
}
