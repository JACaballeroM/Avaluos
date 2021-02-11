using System;
using System.IO;
using System.IO.Compression;

/// <summary>
/// Clase que agrupa utilidades compartidas para el manejo de avalúos.
/// </summary>
public class Utilidades
{
    /// <summary>
    /// Constructor que previene la creación de una instancia por defecto.
    /// </summary>
    private Utilidades() { }

    /// <summary>
    /// Funcion que codificara el xml en base64.
    /// </summary>
    /// <param name="data">Cadena que contiene el xml que queremos convertir a base64.</param>
    /// <returns>
    /// Cadena de texto con el contenido de la encriptacion en base64.
    /// </returns>
    public static string base64Encode(string data)
    {
        try
        {
            byte[] encData_byte = new byte[data.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }
        catch (Exception e)
        {
            throw new Exception("Error en base64Encode" + e.Message);
        }
    }

    /// <summary>
    /// Funcion que decodfica en base64.
    /// </summary>
    /// <param name="data">Cadena con los datos que queremos descifrar.</param>
    /// <returns>
    /// Cadena que contendra el xml desifrado.
    /// </returns>
    public static string base64Decode(string data)
    {
        try
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();

            byte[] todecode_byte = Convert.FromBase64String(data);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
        catch (Exception e)
        {
            throw new Exception("Error en base64Decode" + e.Message);
        }
    }

    /// <summary>
    /// Metodo auxiliar para comprimir un array de bytes.
    /// </summary>
    /// <param name="info">Array de bytes que se quiere comprimir.</param>
    /// <returns>
    /// Array de bytes comprimido.
    /// </returns>
    public static byte[] Comprimir(byte[] info)
    {
        MemoryStream ms = new MemoryStream();
        GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true);
        zip.Write(info, 0, info.Length);
        zip.Close();
        return ms.ToArray();
    }

    /// <summary>
    /// Metodo auxiliar para decomprimir un array de bytes.
    /// </summary>
    /// <param name="info">Array de bytes comprimido.</param>
    /// <returns>
    /// Array de bytes descomprimido.
    /// </returns>
    public static byte[] Decomprimir(byte[] info)
    {
        MemoryStream ms = new MemoryStream();
        ms.Write(info, 0, info.Length);
        ms.Position = 0;
        GZipStream zip = new GZipStream(ms, CompressionMode.Decompress, true);
        MemoryStream result = new MemoryStream();
        byte[] buffer = new byte[64];
        int bytesRead = -1;
        bytesRead = zip.Read(buffer, 0, buffer.Length);
        while (bytesRead > 0)
        {
            result.Write(buffer, 0, bytesRead);
            bytesRead = zip.Read(buffer, 0, buffer.Length);
        }
        zip.Close();
        return result.ToArray();
    }

    
}