using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FunctionAppDemo.Models
{
    public partial class ValoresTable
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public string Nombre { get; set; }
        public int? Edad { get; set; }
        public string Genero {  get; set; }
        public int? Id_Estado { get; set; }
        public string CURP { get; set; }
       

        public ValoresTable(string partitionKey, string rowKey, DateTimeOffset? timeStamp, TableEntity demo)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
            Timestamp = timeStamp;
            Nombre = demo.GetString("Nombre");
            Edad = demo.GetInt32("Edad");
            Genero = demo.GetString("Genero");    
            Id_Estado = demo.GetInt32("Id_Estado");
            CURP = demo.GetString("CURP");
        }
    }

    public partial class DataModel
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public int Id_Estado { get; set; }
        public string Genero { get; set; }
        public string CURP { get; set; }
    }
}

