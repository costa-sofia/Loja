using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace AppExemplo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparDados();
        }

        private void LimparDados()
        {
            txtID.Clear();
            txtDescricao.Clear();
            txtQuantidade.Text = "0";
            dtpValidade.Value = DateTime.Now;
        }
        
        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        Conexao objBD = new Conexao();
        private void PreencherDados()
        {
            string query = "SELECT * FROM tblProduto order by id;";
            dtgProdutos.DataSource = objBD.ExecutarConsulta(query);
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                string desc = txtDescricao.Text;
                int quant = int.Parse(txtQuantidade.Text);
                DateTime dt = dtpValidade.Value;
                Double precoProduto = double.Parse(txtPreco.Text);

                string inserir = $"INSERT INTO tblProduto VALUES (null,'{desc}','{quant}','{dt.ToString("yyyy/MM/dd")}','{precoProduto.ToString().Replace(',','.')}');";


                objBD.ExecutarComando(inserir);

                MessageBox.Show("Dados inseridos com sucesso");
                LimparDados();
                PreencherDados();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void butAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtID.Text);
                string desc = txtDescricao.Text;
                int quant = int.Parse(txtQuantidade.Text);
                DateTime dt = dtpValidade.Value;
                Double precoProduto = double.Parse(txtPreco.Text);

                string alterar = $"UPDATE tblProduto SET descricao = '{desc}', quantidade = {quant}, dtValidade = '{dt.ToString("yyyy/MM/dd")}', preco = {precoProduto.ToString().Replace(',', '.')} WHERE id = {id};";

                objBD.ExecutarComando(alterar);

                MessageBox.Show("Dados alterados com sucesso");

                PreencherDados();
                LimparDados();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dtgProdutos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtID.Text = dtgProdutos.Rows[e.RowIndex].Cells["id"].Value.ToString();
                txtDescricao.Text = dtgProdutos.Rows[e.RowIndex].Cells["descricao"].Value.ToString();
                txtQuantidade.Text = dtgProdutos.Rows[e.RowIndex].Cells["quantidade"].Value.ToString();
                txtPreco.Text = dtgProdutos.Rows[e.RowIndex].Cells["preco"].Value.ToString();
                dtpValidade.Value = DateTime.Parse(dtgProdutos.Rows[e.RowIndex].Cells["dtValidade"].Value.ToString());
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                MessageBox.Show("DEU MERDA MENÓ");
            }
         
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PreencherDados();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtID.Text != "")
                {
                    int idProduto = int.Parse(txtID.Text);

                    string excluir = $"DELETE FROM tblProduto WHERE ID = '{idProduto}';";

                    objBD.ExecutarComando(excluir);
                    LimparDados();
                    PreencherDados();
                }
                else
                {
                    MessageBox.Show("Selecione o registro que deseja excluir");
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
