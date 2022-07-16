using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Firma.Models;
using SQLite;

namespace Firma.Controllers
{
    public class BaseDatosSQLite
    {
        readonly SQLiteAsyncConnection db;

        //Constructor de la clase DataBaseSQLite
        public BaseDatosSQLite(String pathdb)
        {
            //Crear una conexion a la base de datos
            db = new SQLiteAsyncConnection(pathdb);

            //Creacion de la tabla personas dentro de SQLite
            db.CreateTableAsync<ModelFirmas>().Wait();
        }

        // Operaciones CRUD con SQLite
        //READ List Way
        public Task<List<ModelFirmas>> ObtenerListaFirmas()
        {
            return db.Table<ModelFirmas>().ToListAsync();
        }

        //READ one by one
        public Task<ModelFirmas> ObtenerFirma(int pcodigo)
        {
            return db.Table<ModelFirmas>()
                .Where(i => i.codigo == pcodigo)
                .FirstOrDefaultAsync();
        }

        //CREATE firma
        public Task<int> GrabarFirma(ModelFirmas firma)
        {
            if (firma.codigo != 0)
            {
                return db.UpdateAsync(firma);
            }
            else
            {
                return db.InsertAsync(firma);
            }
        }

        //Delete

        public Task<int> EliminarFirma(ModelFirmas firma)
        {
            return db.DeleteAsync(firma);

        }
    }
}
