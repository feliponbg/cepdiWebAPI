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


        public DataTable LeerUsuarios()
        {
            string archivo = objConfiguracion.GetValue<string>("BaseDeDatosSimulacion:TablaUsuarios");
            var contentRoot = objConfiguracion.GetValue<string>(WebHostDefaults.ContentRootKey);
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            if (!File.Exists(contentRoot + archivo))
                throw new FileNotFoundException($"La tabla no existe: {contentRoot + archivo}");

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

        public DataTable LeerMedicamentos()
        {
            string archivo = objConfiguracion.GetValue<string>("BaseDeDatosSimulacion:TablaMedicamentos");
            var contentRoot = objConfiguracion.GetValue<string>(WebHostDefaults.ContentRootKey);
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            if (!File.Exists(contentRoot + archivo))
                throw new FileNotFoundException($"La tabla no existe: {contentRoot + archivo}");

            FileStream streamer = new FileStream(contentRoot + archivo, FileMode.Open);
            IExcelDataReader reader = null;
            reader = ExcelReaderFactory.CreateCsvReader(streamer);
            DataSet results = reader.AsDataSet();
            results.Tables[0].Rows[0].Delete();
            results.AcceptChanges();

            DataTable dt = new DataTable("Medicamentos");
            DataColumn column;
            DataRow newRow;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "IIDMEDICAMENTO";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "NOMBRE";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "CONCENTRACION";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "IIDFORMAFARMACEUTICA";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Single");
            column.ColumnName = "PRECIO";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "STOCK";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "PRESENTACION";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Boolean");
            column.ColumnName = "BHABILITADO";
            dt.Columns.Add(column);
            

            foreach (DataRow row in results.Tables[0].Rows)
            {
                newRow = dt.NewRow();
                int i = 0;
                foreach (var item in row.ItemArray)
                {
                    if (i == 0) newRow["IIDMEDICAMENTO"] = Convert.ToInt32(item.ToString());
                    if (i == 1) newRow["NOMBRE"] = item.ToString();
                    if (i == 2) newRow["CONCENTRACION"] = item.ToString();
                    if (i == 3) newRow["IIDFORMAFARMACEUTICA"] = Convert.ToInt32(item.ToString());
                    if (i == 4) newRow["PRECIO"] = Convert.ToSingle(item.ToString());
                    if (i == 5) newRow["STOCK"] = Convert.ToInt32(item.ToString());
                    if (i == 6) newRow["PRESENTACION"] = item.ToString();
                    if (i == 7) newRow["BHABILITADO"] = item.ToString() == "1" ? true : false;

                    //Console.WriteLine(item.ToString());
                    i++;
                }
                dt.Rows.Add(newRow);
            }

            return dt;
        }

    }
}
