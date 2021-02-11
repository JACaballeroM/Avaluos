using System;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using SecurityCore;

namespace SIGAPred.Data
{
    internal class TranHelper
    {
        #region Properties

        #endregion Properties

        #region EjecutaConsultaDS

        /// <summary>
        /// Ejecuta un SP
        /// </summary>
        /// <param name="cmd">Comando tipo Store Procedure</param>
        /// <returns>DataSet resultado de la consulta</returns>
        internal DataSet EjecutaConsultaSP(OracleCommand cmd)
        {
            OracleConnection oracleConnection = new OracleConnection(getStringConnection());
            oracleConnection.Open();
            cmd.Connection = oracleConnection;
            OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(cmd);
            DataSet dsReturn = new DataSet();

            oracleDataAdapter.Fill(dsReturn);

            oracleDataAdapter.Dispose();
            if (oracleConnection.State != ConnectionState.Closed)
                oracleConnection.Close();

            oracleConnection.Dispose();

            return dsReturn;
        }

        /// <summary>
        /// ejecuta un sp
        /// </summary>
        /// <param name="cmd">Comando tipo Store Procedure</param>
        /// <returns>numero de registros afectados</returns>
        internal int EjecutaNonQuerySP(OracleCommand cmd)
        {
            int afectedRows = 0;
            try
            {
                OracleConnection oracleConnection = new OracleConnection(getStringConnection());

                oracleConnection.Open();
                cmd.Connection = oracleConnection;
                afectedRows = cmd.ExecuteNonQuery();

                if (oracleConnection.State != ConnectionState.Closed)
                    oracleConnection.Close();

                oracleConnection.Dispose();

            }
            catch (Exception ex)
            {
                string e = ex.ToString();
            }
            return afectedRows;
        }

        /// <summary>
        /// ejecuta un sp que titne un clob como parametro de salida, entre otros
        /// </summary>
        /// <param name="cmd">comando</param>
        /// <param name="sParName">parameter name</param>
        /// <param name="sClob">out, clob content</param>
        /// <returns>non query result</returns>
        internal int EjecutaNonQuerySP(OracleCommand cmd, string sParName, out string sClob)
        {
            OracleConnection oracleConnection = new OracleConnection(getStringConnection());
            int afectedRows = 0;

            cmd.Parameters.Add(new OracleParameter(sParName, OracleDbType.Clob)).Direction = ParameterDirection.Output;

            oracleConnection.Open();
            cmd.Connection = oracleConnection;
            afectedRows = cmd.ExecuteNonQuery();

            //sClob = ((Oracle.DataAccess.Types.OracleClob)(cmd.Parameters[sParName].Value)).Value.ToString();

            OracleClob myLob = (OracleClob)cmd.Parameters[sParName].Value;
            sClob = System.Convert.ToString(myLob.Value);

            if (oracleConnection.State != ConnectionState.Closed)
                oracleConnection.Close();

            oracleConnection.Dispose();

            return afectedRows;
        }

        /// <summary>
        /// ejecuta un script
        /// </summary>
        /// <param name="myCommandText">script a ejecutar</param>
        /// <returns>numero de registro afectados</returns>
        internal int EjecutaNonQuery(string myCommandText)
        {
            OracleConnection oracleConnection = new OracleConnection(getStringConnection());
            int afectedRows;
            using (OracleCommand cmd = new OracleCommand(myCommandText))
            {
                cmd.Connection = oracleConnection;
                afectedRows = cmd.ExecuteNonQuery();
            }

            if (oracleConnection.State != ConnectionState.Closed)
                oracleConnection.Close();

            oracleConnection.Dispose();

            return afectedRows;
        }

        //#region EjecutaConsultaDS
        /// <summary>
        /// Ejecuta una consulta a la base de datos, regresa un DataSet de la consulta
        /// </summary>
        /// <param name="myCommandText">script de Consulta</param>
        /// <returns>DataSet resultado de la consulta</returns>
        internal DataSet EjecutaConsultaDS(string myCommandText)
        {
            OracleConnection oracleConnection = new OracleConnection(getStringConnection());
            OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(myCommandText, oracleConnection);
            DataSet dsReturn = new DataSet();

            oracleDataAdapter.Fill(dsReturn);

            oracleDataAdapter.Dispose();
            if (oracleConnection.State != ConnectionState.Closed)
                oracleConnection.Close();

            oracleConnection.Dispose();

            return dsReturn;
        }

        #endregion EjecutaConsultaDS

        public string getStringConnection()
        {

            return SecurityCoreManager.getStringConnection("FEXAVA");
            
        }
    }
}