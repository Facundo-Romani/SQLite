using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using CRUDSqlite.Models;
using System.Threading.Tasks;

namespace CRUDSqlite.Data
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;

        public SQLiteHelper (string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Alumno>().Wait();
        }
           

        // Insertar o Actualizar.
        public Task<int> SaveAlumnoAsync(Alumno alum)
        {
            if (alum.IdAlumno != 0)
            {
                return db.UpdateAsync(alum);
            }
            else
            {
                return db.InsertAsync(alum);
            }
        }
        
        // Borrar.
        public Task<int> DeleteAlumnoAsync(Alumno alumno) 
        {
            return db.DeleteAsync(alumno);
        }

        /// <summary>
        /// Recuperar todos los Alumnos.
        /// </summary>
        /// <returns></returns>
        public Task<List<Alumno>>  GetAlumnosAsync()
        {
            return db.Table<Alumno>().ToListAsync();
        }

        /// <summary>
        /// Recupera Alumnos por Id
        /// </summary>
        /// <param name="idAlumno">Id del alumno que se requiere</param>
        /// <returns></returns>
        public Task<Alumno> GetAlumnoByIdAsync (int idAlumno)
        {
            return db.Table<Alumno>().Where(a => a.IdAlumno == idAlumno).FirstOrDefaultAsync();
        }
    }
}
