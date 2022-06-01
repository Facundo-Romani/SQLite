using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using CRUDSqlite.Models;

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
    }
}
