using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ORM.orm
{
    public abstract class Orm
    {
        static readonly Dictionary<string, Dictionary<string, OrmField>> tables = new Dictionary<string, Dictionary<string, OrmField>>();
        static readonly Dictionary<string, string> primary_Keys = new Dictionary<string, string>();

        protected static void Int(string table_name, string property_name, Func<Orm, int> getter, Action<Orm, int> setter)
        {
            if (tables.ContainsKey(table_name) == false)
                    tables[table_name] = new Dictionary<string, OrmField>();

            var ormMapper = new OrmInt(getter, setter);

            //Register the property in the tables array
            tables[table_name].Add(property_name, ormMapper);
        }

        protected static void String(string table_name, string property_name, Func<Orm, string> getter, Action<Orm, string> setter)
        {
            if (tables.ContainsKey(table_name) == false)
                tables[table_name] = new Dictionary<string, OrmField>();

            var ormMapper = new OrmString(getter, setter);

            //Register the property in the tables array
            tables[table_name].Add(property_name, ormMapper);
        }

        protected static void PrimaryKey(string table_name, string column)
        {
            primary_Keys[table_name] = column;
        }

        public void Insert()
        {
            string tableName = TableName();
            List<string> cols = new List<string>();
            List<string> vals = new List<string>();
            if (tables.ContainsKey(tableName) == false)
            {
                Console.WriteLine("$There are no (tableName) in this ORM");
                return;
            }

            string pk_column = primary_Keys[tableName];

            //Run through all fields that was defined in the child class
            foreach (KeyValuePair<string, OrmField> kv in tables[tableName])
            {
                if (kv.Key == pk_column)
                {
                    continue;
                }
                var OrmField = kv.Value;
                cols.Add(kv.Key);
                vals.Add(OrmField.GetSQLValue(this));
            }
            //Join the columns by giving them together with a ,
            string colsString = string.Join(",", cols);
            string valsString = string.Join(",", vals);
            string sql = $"INSERT INTO {tableName} ({colsString}) VALUES ({valsString})";
            // Console.WriteLine(sql);
            Execute(sql);
        }
        
        public void Update()
        {
            string tableName = TableName();
            List<string> pair = new List<string>();
            if (tables.ContainsKey(tableName) == false)
            {
                Console.WriteLine("$There are no (tableName) in this ORM");
                return;
            }

            string pk_column = primary_Keys[tableName];
            string pk_value = tables[tableName][pk_column].GetSQLValue(this);

            //Run through all fields that was defined in the child class
            foreach (KeyValuePair<string, OrmField> kv in tables[tableName])
            {
                if (kv.Key == pk_column)
                {
                    continue;
                }
                var OrmField = kv.Value;
                pair.Add(kv.Key + " = " + OrmField.GetSQLValue(this));
            }

            //Join the columns by giving them together with a ,
            string pairString = string.Join(",", pair);

            string sql = $"UPDATE {tableName} SET {pairString} WHERE {pk_column} = {pk_value}";
            // Console.WriteLine(sql);
            Execute(sql);
        }
        
        public void Delete()
        {
            string tableName = TableName();
            if (tables.ContainsKey(tableName) == false)
            {
                Console.WriteLine("$There are no (tableName) in this ORM");
                return;
            }

            string pk_column = primary_Keys[tableName];
            string pk_value = tables[tableName][pk_column].GetSQLValue(this);
            
            string sql = $"DELETE FROM {tableName} WHERE {pk_column} = {pk_value}";
            // Console.WriteLine(sql);
            Execute(sql);
        }
        
        // public void Display()
        // {
        //     string tableName = TableName();
        //     if (tables.ContainsKey(tableName) == false)
        //     {
        //         Console.WriteLine("$There are no (tableName) in this ORM");
        //         return;
        //     }
        //     //
        //     // string pk_column = primary_Keys[tableName];
        //     // string pk_value = tables[tableName][pk_column].GetSQLValue(this);
        //     
        //     string sql = $"SELECT * FROM {tableName}";
        //     // Console.WriteLine(sql);
        //     Execute(sql);
        // }

        protected abstract string TableName();

        private static SqlConnection openConnection()
        {
            Database db = new Database();
            SqlConnection conn = Database.connection();
            conn.Open();

            return conn;
        }

        private static void Execute(string sql)
        {
            var saveConnection = openConnection();

            var cmd = saveConnection.CreateCommand();

            cmd.CommandText = sql;

            cmd.ExecuteNonQuery();
        }

        public void query(string sql)
        {
            SqlCommand command = openConnection().CreateCommand();
            command.CommandText = sql;
            SqlDataReader reader = command.ExecuteReader();

            string tableName = TableName();
            object[] values = new object[tables[tableName].Count];
            while (reader.Read())
            {
                reader.GetValues(values);
                int p = 0;
                foreach (KeyValuePair<string, OrmField> kv in tables[tableName])
                {
                    OrmField column = kv.Value;
                    column.SetValue(this, values[p]);
                    p++;
                }
            }
        }
    }
}