using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite; // need this to talk to sqlite DBs
using Dapper; // using this to do most of the heavy lifting

namespace MikaMusicStudio2.Model
{
    public static class Database
    {
        // convenient having consts for filenames and such, helps make less typos
        const string DB_FILE_NAME = "products.db";
        const string CREATE_DB_SCRIPT = "create_database.sql";

        static string MAPPED_DATA = string.Empty; // this will be set to the APP_DATA folder, see constructor
        static string CONNECTION_STRING = string.Empty; // obvious

        static Database()
        {
            // static objects can have static constructors, like this one - handy

            MAPPED_DATA = HttpContext.Current.Server.MapPath("~/App_Data/");
            CONNECTION_STRING = $"Data Source={MAPPED_DATA + DB_FILE_NAME}";

            // sqlite can run many queries at the same time - such as creating table structure and inserting records - see create_database.sql
            // as long as each query ends in a ;

            var createQuery = System.IO.File.ReadAllText(MAPPED_DATA + CREATE_DB_SCRIPT);
            GetConnection().OpenAndReturn().Execute(createQuery); // just run create script
        }

        // lambda operator lets you reduce these one liner methods to...actually one line
        public static SQLiteConnection GetConnection() => new SQLiteConnection(CONNECTION_STRING);
    }
}