using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Azure.Data.Tables;
using System.Collections.Generic;
using FunctionAppDemo.Models;
using Newtonsoft.Json;

namespace FunctionAppDemo
{   

    public static class Function1
    {
        /* Cadena de conexión para utilizar el emulador local (Azurite) */
        static string LOCAL_CONN_STRING = "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;\r\nQueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";
        /* Cadena de conexión para utilizar los recursos en la nube */
        static string AZURE_CONN_STRING = "DefaultEndpointsProtocol=https;AccountName=plenicaldemo;AccountKey=M0xoP6sxP5CMnBUs+1wKn6QBD6VdSDzdYMK4XP4Dmlkw86so0f+dFfhZd/C1GwZ18Sip5uXHrmzY+AStjZ/xRw==;EndpointSuffix=core.windows.net;TableEndpoint=https://plenicaldemo.table.core.windows.net/datosPersonales;";

        [FunctionName("GetAll")]
        public static async Task<IActionResult> GetAll(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "demo")] HttpRequest req,            
            ILogger log)
            
        {
            /* Nueva instancia de la lista donde se van a almacenar los registros */
            List<ValoresTable> valores = new List<ValoresTable>();
            /* Setear cadena de conexión */
            string connectionString = LOCAL_CONN_STRING;
            /* Setear nombre de la tabla a consultar */
            string tableName = "datosPersonales";

            /* Abrir conexión con la Azure Table */
            TableClient demos = new(connectionString, tableName);

            /* Leer todos los valores en la tabla */
            foreach (var demo in demos.Query<TableEntity>())
            {
                /* Crear una instancia de la clase ValoresTable por cada registro */
                ValoresTable valor = new ValoresTable(demo.PartitionKey, demo.RowKey, demo.Timestamp, demo);
                /* Agregar instancia a la lista */
                valores.Add(valor);
            }

            return new OkObjectResult(valores);
        }

        [FunctionName("CreateNew")]
        public static async Task<IActionResult> CreateNew(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "demo")] HttpRequest req,
            ILogger log)

        {
            /* Setear cadena de conexión */
            string connectionString = LOCAL_CONN_STRING;
            /* Setear nombre de la tabla a consultar */
            string tableName = "datosPersonales";

            /* Leer el Body del request HTTP */
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            /* Crear objeto de instancia DemoTable a partir de la información recuperada del Body */
            var input = JsonConvert.DeserializeObject<DemoTable>(requestBody);

            /* Abrir conexión con la Azure Table */
            TableClient demos = new(connectionString, tableName);

            /* Guardar registro en la tabla */
            demos.UpsertEntity(input);

            return new OkObjectResult(input);
        }
    }
}
