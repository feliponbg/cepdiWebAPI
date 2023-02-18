using ExcelDataReader;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;

namespace cepdiWebAPI.Services.Utilerias
{
    public class Excel
    {
        private readonly IConfiguration objConfiguracion;

        public Excel(IConfiguration configuration) 
        {
            this.objConfiguracion = configuration;
        }


        public DataTable Leer(string archivo)
        {
            var contentRoot = objConfiguracion.GetValue<string>(WebHostDefaults.ContentRootKey);
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            FileStream streamer = new FileStream(contentRoot + archivo, FileMode.Open);
            IExcelDataReader reader = null;
            reader = ExcelReaderFactory.CreateCsvReader(streamer);
            DataSet results = reader.AsDataSet();
            results.Tables[0].Rows[0].Delete();
            results.AcceptChanges();

            DataTable dt = new DataTable("Usuarios");
            DataColumn column;
            DataRow newRow;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "idusuario";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "nombre";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.DateTime");
            column.ColumnName = "fechacreacion";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "usuario";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "password";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "idperfil";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Boolean");
            column.ColumnName = "estatus";
            dt.Columns.Add(column);

            foreach (DataRow row in results.Tables[0].Rows)
            {
                newRow = dt.NewRow();
                int i = 0;
                foreach (var item in row.ItemArray)
                {
                    if (i == 0) newRow["idusuario"] = Convert.ToInt32(item.ToString());
                    if (i == 1) newRow["nombre"] = item.ToString();
                    if (i == 2) newRow["fechacreacion"] =  Convert.ToDateTime(item.ToString());
                    if (i == 3) newRow["usuario"] = item.ToString();
                    if (i == 4) newRow["password"] = item.ToString();
                    if (i == 5) newRow["idperfil"] = Convert.ToInt32(item.ToString());
                    if (i == 6) newRow["estatus"] = Convert.ToInt32(item.ToString());

                    //Console.WriteLine(item.ToString());
                    i++;
                }
                dt.Rows.Add(newRow);
            }

            return dt;
        }

    }
}
