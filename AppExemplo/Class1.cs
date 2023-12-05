using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace AppExemplo
{
   
    internal class Conexao
    {
        MySqlConnection conn;

        private void Conectar()
        {
            try 
            {
                string conexao = "Persist Security info = false; server = localhost; database = bdexemplo; user = root; pwd =;";
                conn = new MySqlConnection(conexao);
                conn.Open(); 
            }
            catch (Exception ex)
            {
                throw new Exception("Verifique sua conexão.");
            }
            
        }

        public void ExecutarComando(string sql)
        {
            Conectar();
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conn);
                comando.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw new Exception("Corrija a instrução SQL que deseja executar.");
            }
            finally
            {
                conn.Close();
            }
            
        }

        public DataTable ExecutarConsulta(string sql)
        {
            Conectar();

            try
            {
               MySqlDataAdapter dt = new MySqlDataAdapter(sql, conn);
               DataTable dados = new DataTable();
               dt.Fill(dados);
               return dados;
           
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close ();
            }
        }

    }


}
