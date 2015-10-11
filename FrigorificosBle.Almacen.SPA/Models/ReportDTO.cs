using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrigorificosBle.Almacen.SPA.Models
{

    public class ReportDto
    {
        public string ResourcePath { get; set; }
        public List<DataSource> DataSources { get; set; }
        public List<Parameter> Parameters { get; set; }
        public string ReportDataSourceName { get; set; }
    }

    public class DataSource
    {
        public string Name { get; set; }
        public string Data { get; set; }
    }

    public class Parameter
    {
        public string Name { get; set; }
        public string Data { get; set; }
    }

}