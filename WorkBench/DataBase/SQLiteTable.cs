using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.SQLite
{
    public class SQLiteTable
    {
        public string TableName = "";
        public SQLiteColumnList Columns;

        public SQLiteTable()
        { }

        public SQLiteTable(string name)
        {
            TableName = name;
        }
        public void SetCols(SQLiteColumnList cols) {
            Columns = cols;
        }
    }
}