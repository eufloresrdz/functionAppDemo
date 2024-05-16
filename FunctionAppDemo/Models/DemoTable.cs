using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppDemo.Models
{
    public class DemoTable : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        public string Nombre { get; set; }
        public int? Edad { get; set; }
        public string Genero { get; set; }
        public int? Id_Estado { get; set; }
        public string CURP { get; set; }

        public DemoTable(string partitionKey, string rowKey, string nombre, int edad, string genero, int id_Estado, string curp)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
            Timestamp = DateTime.UtcNow;
            Nombre = nombre;
            Edad = edad;
            Genero = genero;
            Id_Estado = id_Estado;
            CURP = curp;
        }
    }
}
