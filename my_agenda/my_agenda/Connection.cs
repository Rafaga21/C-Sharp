using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace my_agenda
{
    abstract class Connection
    {
        private SQLiteConnection myConn;
        private SQLiteDataReader dataReader;
        private SQLiteCommand myCmd;
        private DataTable myDataTable;
        private String db = Application.StartupPath + "\\myagenda.db";

        public Connection()
        {
            if (!File.Exists(db))
            {
                startConnection();
                executeQuery("CREATE TABLE directorio(id INTEGER PRIMARY KEY AUTOINCREMENT, nombre TEXT, telefono TEXT)");
                close();
            }
        }

        private void dtColumns()
        {
            myDataTable = new DataTable();
            myDataTable.Columns.Add("ID");
            myDataTable.Columns.Add("NOMBRE");
            myDataTable.Columns.Add("TELEFONO");
        }

        private void startConnection()
        {
            myConn = new SQLiteConnection("Data Source = " + db);
            myConn.Open();
            dtColumns();
        }

        private Boolean executeQuery(String sql)
        {
            Boolean answer = false;
            try
            {
                startConnection();
                myCmd = new SQLiteCommand(sql, myConn);
                answer = myCmd.ExecuteNonQuery() > 0;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                close();
            }
            return answer;
        }

        protected Boolean update(String sql)
        {
            return executeQuery(sql);
        }

        protected Boolean insert(String sql)
        {
            return executeQuery(sql);
        }

        protected Boolean delete(String sql)
        {
            return executeQuery(sql);
        }

        protected DataTable query(String sql)
        {
            try
            {
                startConnection();
                myCmd = new SQLiteCommand(sql, myConn);
                dataReader = myCmd.ExecuteReader();
                while (dataReader.Read())
                {
                    myDataTable.Rows.Add(
                        new Object[] {
                            dataReader["id"], 
                            dataReader["nombre"], 
                            dataReader["telefono"]
                        }
                    );
                }
                dataReader.Close();
            }catch(SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                close();
            }

            return myDataTable;
        }

        private void close()
        {
            if(myConn != null && myConn.State == ConnectionState.Open)
            {
                myConn.Close();
            }
        }
    }
}
