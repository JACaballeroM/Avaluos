using System;
using System.Data;


/// <summary>
/// Clase estatica con las constantes del frontal
/// </summary>
public static class Constantes
{

    #region Botones-Controles
    /// <summary>
    /// Nombre del botón 'VER AVALUO' de la bandeja avalúos próximos
    /// </summary>
    public const string BOTON_VERAVALUO_AVALUOSPROXIMOS = "LinkVerAv";

    /// <summary>
    /// Nombre del checkbox vigencia de la bandeja de entrada
    /// </summary>
    public const string CHECKBOX_VIGENCIA = "checkboxVIG";
    #endregion

    #region FILTRO BUSQUEDA

    /// <summary>
    ///Filtro de búsqueda seleccionado
    /// </summary>
    public const string REQUEST_FILTRO = "filtro";

    /// <summary>
    /// Criterio de ordenación seleccionado
    /// </summary>
    public const string REQUEST_SORTEXP = "sortexp";

    /// <summary>
    /// Dirección de ordenación seleccionada
    /// </summary>
    public const string REQUEST_SORTDIR = "sortdir";
    #endregion

    #region COLUNMAS DESCRIPCIONES

    /// <summary>
    /// COLUMNA DESCRIPCIÓN CLASE VIDA
    /// </summary>
    public const string COL_DESC_CLASEVIDA = "DescClaseVida";

    /// <summary>
    /// COLUMNA DESCRIPCIÓN CONSERVACIÓN
    /// </summary>
    public const string COL_DESC_CONSERVACION = "DescEstadoConserv";

    /// <summary>
    /// COLUMNA DESCRIPCIÓN USO
    /// </summary>
    public const string COL_DESC_USO = "DescUso";

    /// <summary>
    /// COLUMNA DESCRIPCIÓN RANGO NIVEL
    /// </summary>
    public const string COL_DESC_RANGONIV = "DescRangoNiv";

    /// <summary>
    /// COLUMNA DESCRIPCIÓN CLASE CONSTANTE
    /// </summary>
    public const string COL_DESC_CLASECONST = "DescClaseConst";
    #endregion

    #region NOMBRE DE LAS COLUNMAS DE BD  Y/O DEL GRIDVIEW
    /// <summary>
    /// COLUMNA CÓDIGO ESTADO AVALÚO
    /// </summary>
    public const string COL_CODESTADOAVALUO = "CODESTADOAVALUO";
    /// <summary>
    /// COLUMNA IDENTIFICADOR AVALÚO
    /// </summary>
    public const string COL_IDAVALUO = "IDAVALUO";
    /// <summary>
    /// COLUMNA NÚMERO NOTARIO
    /// </summary>
    public const string COL_NUMNOTARIO = "NUMERO_NOTARIO";
    /// <summary>
    /// COLUMNA CUENTA CATASTRAL
    /// </summary>
    public const string COL_CUENTACATASTRAL = "CUENTACATASTRAL";
    /// <summary>
    /// COLUMNA VIGENTE
    /// </summary>
    public const string COL_VIGENTE = "VIGENTE";
    /// <summary>
    /// COLUMNA NÚMERO ÚNICO
    /// </summary>
    public const string COL_NUMUNICO = "NUMEROUNICO";
    /// <summary>
    /// COLUMNA TIPO
    /// </summary>
    public const string COL_TIPO = "TIPO";
    /// <summary>
    /// COLUMNA CÓDIGO TIPO TRÁMITE
    /// </summary>
    public const string COL_CODTIPOTRAMITE = "CODTIPOTRAMITE";
    #endregion

    #region TIPOS AVALUOS
    /// <summary>
    /// PARÁMETRO TIPO AVALÚO
    /// </summary>
    public const string PAR_TIPO_AVALUO = "TipoAvaluo";
    /// <summary>
    /// PARÁMETRO ABREVIATURA AVALÚO COMERCIAL 
    /// </summary>
    public const string PAR_AVALUO_COMERCIAL_SHORT = "COM";
    /// <summary>
    /// PARÁMETRO ABREVIATURA AVALÚO CATASTRAL 
    /// </summary>
    public const string PAR_AVALUO_CATASTRAL_SHORT = "CAT";
    /// <summary>
    /// PARÁMETRO XML AVALÚO COMERCIAL
    /// </summary>
    public const string PAR_XML_AV__COMERCIAL = "Comercial";
    /// <summary>
    /// PARÁMETRO XML AVALÚO CATASTRAL
    /// </summary>
    public const string PAR_XML_AV_CATASTRAL = "Catastral";
    /// <summary>
    /// PARÁMETRO VALOR AVALÚO COMERCIAL
    /// </summary>
    public const string PAR_VAL_AVALUO_COMERCIAL = "1";
    /// <summary>
    /// PARÁMETRO VALOR AVALÚO CATASTRAL
    /// </summary>
    public const string PAR_VAL_AVALUO_CATASTRAL = "2";
    #endregion

    #region ESTADOS AVALÚOS
    /// <summary>
    /// CÓDIGO ESTADO AVALÚO RECIBIDO
    /// </summary>
    public const int CODESTADO_RECIBIDO = 1;
    /// <summary>
    /// CÓDIGO ESTADO AVALÚO CANCELADO
    /// </summary>
    public const int CODESTADO_CANCELADO = 2;
    /// <summary>
    /// CÓDIGO ESTADO AVALÚO ENVIADO NOTARIO
    /// </summary>
    public const int CODESTADO_ENVIADONOTARIO = 6;
    #endregion

    #region FOTOS
    /// <summary>
    /// PARÁMETRO VALOR TAMAÑO FOTO NORMAL
    /// </summary>
    public const string PAR_VAL_TAMANIOFOTO_NORMAL = "N";
    /// <summary>
    /// PARÁMETRO VALOR TAMAÑO FOTO REDUCIDA
    /// </summary>
    public const string PAR_VAL_TAMANIOFOTO_REDUCIDA = "R";
    #endregion

    #region URLS

    #region URLS PÁGINAS
    /// <summary>
    /// URL PÁGINA 'BANDEJA ENTRADA'
    /// </summary>
    public const string URL_BANDEJAENTRADA = "~/BandejaEntrada.aspx";
    /// <summary>
    /// URL PÁGINA 'AVALÚO'
    /// </summary>
    public const string URL_AVALUO = "~/Avaluo.aspx";
    /// <summary>
    /// URL PÁGINA 'AVALÚOS PRÓXIMOS'
    /// </summary>
    public const string URL_AVALUOS_PROXIMOS = "~/AvaluosProximos.aspx";
    /// <summary>
    /// URL PÁGINA 'DESCARGA AVALÚOS'
    /// </summary>
    public const string URL_DESCARGA_AVALUOS = "~/DescargaAvaluo.aspx";
    /// <summary>
    /// URL PÁGINA 'SUBIR AVALÚO'
    /// </summary>
    public const string URL_SUBIR_AVALUO = "~/SubirAvaluo.aspx";
    /// <summary>
    /// URL PÁGINA 'DESCARGAR ACUSE AVALÚO'
    /// </summary>
    public const string URL_DESCARGA_ACUSE_AVALUO = "~/DescargaAcuseAvaluo.aspx";
    /// <summary>
    /// URL PÁGINA 'DESCARGAR AVALÚO'
    /// </summary>
    public const string URL_DESCARGAR_AVALUO = "~/DescargarAvaluo.aspx";
    /// <summary>
    /// URL PÁGINA 'DESCARGA AVALÚO'
    /// </summary>
    public const string URL_DESCARGA_AVALUO_DETALLE = "~/DescargaAvaluo.aspx";
    /// <summary>
    /// URL PÁGINA 'IMAGEN'
    /// </summary>
    public const string URL_IMAGEN = "~/imagen.aspx";
    /// <summary>
    /// URL PÁGINA 'INFORME AVALÚO'
    /// </summary>
    public const string URL_AVALUOS_INFORME = "InformeAvaluo.aspx";


    #endregion

    #region URLS IMÁGENES
    /// <summary>
    /// Url imagen botón cambiar estado
    /// </summary>
    public const string URL_IMG_CAMBIAR_ESTADO = "~/Images/back-forth.gif";

    /// <summary>
    ///  Url imagen botón ver justificante avalúo
    /// </summary>
    public const string URL_IMG_VER_DETALLE = "~/Images/zoom-in.gif";
   
    /// <summary>
    ///  Url imagen botón asignar notario
    /// </summary>
    public const string URL_IMG_USER = "~/Images/user.gif";

    /// <summary>
    /// Url imagen botón asignar avalúos próximos
    /// </summary>
    public const string URL_IMG_AVALUOS_PROXIMOS = "~/Images/camera.gif";
   
    /// <summary>
    /// Url imagen botón ver acuse de avalúo
    /// </summary>
    public const string URL_INFORME = "~/Images/two-docs.gif";

    /// <summary>
    /// Url imagen botón cambiar estado de avalúo deshabilitado
    /// </summary>
    public const string URL_IMG_CAMBIAR_ESTADO_P = "~/Images/back-forth_p.gif";
   
    /// <summary>
    /// Url imagen botón justificante avalúo deshabilitado
    /// </summary>
    public const string URL_IMG_VER_DETALLE_P = "~/Images/zoom-in_p.gif";

    /// <summary>
    /// Url imagen botón  asignar notario deshabilitado
    /// </summary>
    public const string URL_IMG_USER_P = "~/Images/user_p.gif";

    /// <summary>
    ///  Url imagen botón avalúos próximos deshabilitado
    /// </summary>
    public const string URL_IMG_AVALUOS_PROXIMOS_P = "~/Images/camera_p.gif";

    /// <summary>
    ///  Url imagen botón acuse de avalúo deshabilitado
    /// </summary>
    public const string URL_INFORME_P = "~/Images/two-docs_p.gif";


    #endregion

    #region URLS INFORMES

    /// <summary>
    /// url plantilla justificante avalúo comercial
    /// </summary>
    public const string URL_INFORMEAV_COM = @"ReportDesign\JustificanteAvaluoCom\JustificanteAva.rdlc";
    
    /// <summary>
    ///  url plantilla justificante avalúo catastral
    /// </summary>
    public const string URL_INFORMEAV_CAT = @"ReportDesign\JustificanteAvaluoCat\JustificanteAva.rdlc";
    #endregion

    #endregion

    #region VALORES VIGENCIA
    /// <summary>
    /// PARÁMETRO AVALÚOS VIGENTES
    /// </summary>
    public const string PAR_VIGENTE = "S"; //Avalúos vigentes
    /// <summary>
    /// PARÁMETRO AVALÚOS NO VIGENTES
    /// </summary>
    public const string PAR_NO_VIGENTE = "N";//Avalúos no vigentes
    /// <summary>
    /// PARÁMETRO AVALÚOS VIGENTES Y NO VIGENTES (TODOS)
    /// </summary>
    public const string PAR_VIGENCIA_TODOS = "T";//Todos los avalúos (vigentes + no vigentes )
    #endregion

    #region VIEWSTATE
    /// <summary>
    /// PARÁMETRO VIEWSTATE TIPO ESTADO
    /// </summary>
    public const string PAR_VIEWSTATE_TIPOESTADO = "tipoEstado";
    /// <summary>
    /// PARÁMETRO VIEWSTATE TIPO VIGENCIA
    /// </summary>
    public const string PAR_VIEWSTATE_TIPOVIGENCIA = "tipoVigencia";
    /// <summary>
    /// PARÁMETRO VIEWSTATE TIPO BÚSQUEDA
    /// </summary>
    public const string PAR_VIEWSTATE_TIPOBUSQUEDA = "tipoBusqueda";
    /// <summary>
    /// PARÁMETRO VIEWSTATE NÚMERO ÚNICO AVALÚO
    /// </summary>
    public const string PAR_VIEWSTATE_NUMUNICOAVALUO = "numUnicoAvaluo";
    /// <summary>
    /// PARÁMETRO VIEWSTATE CUENTA CATASTRAL
    /// </summary>
    public const string PAR_VIEWSTATE_CUENTACAT = "cuentacat";
    /// <summary>
    /// PARÁMETRO VIEWSTATE NÚMERO AVALÚO
    /// </summary>
    public const string PAR_VIEWSTATE_NUMAVALUO = "numAvaluo";
    /// <summary>
    /// PARÁMETRO VIEWSTATE FECHA INICIAL
    /// </summary>
    public const string PAR_VIEWSTATE_FECHAINI = "fechainic";
    /// <summary>
    /// PARÁMETRO VIEWSTATE FECHA FINAL
    /// </summary>
    public const string PAR_VIEWSTATE_FECHAFIN = "fechafin";
    /// <summary>
    /// PARÁMETRO VIEWSTATE ORIGEN
    /// </summary>
    public const string AR_VIEWSTATE_ORIGEN = "origen";
    /// <summary>
    /// PARÁMETRO VIEWSTATE DOCUMENTO XML
    /// </summary>
    public const string PAR_VIEWSTATE_DOCXML = "documentoXML";
    /// <summary>
    /// PARÁMETRO VIEWSTATE ERROR DATATABLE
    /// </summary>
    public const string PAR_VIEWSTATE_ERRORDT = "errorDT";
    /// <summary>
    /// PARÁMETRO VIEWSTATE TIPO MENSAJE
    /// </summary>
    public const string PAR_VIEWSTATE_TIPOMSJ = "tipoMensaje";
    /// <summary>
    /// PARÁMETRO VIEWSTATE NÚMERO ÚNICO REGISTRADO
    /// </summary>
    public const string PAR_VIEWSTATE_NUMUNICOREGISTRADO = "numUnicoReg";
    /// <summary>
    /// PARÁMETRO VIEWSTATE FILTRO BÚSQUEDA
    /// </summary>
    public const string PAR_VIEWSTATE_FILTROBUSQUEDA = "fBusqueda";
    /// <summary>
    /// PARÁMETRO VIEWSTATE DIRECCIÓN DE ORDENACIÓN
    /// </summary>
    public const string PAR_VIEWSTATE_SORTDIRECTION = "SORTDIRECTION";
    /// <summary>
    /// PARÁMETRO VIEWSTATE EXPRESIÓN DE ORDENACIÓN
    /// </summary>
    public const string PAR_VIEWSTATE_SORTEXPRESION = "SORTEXPRESION";
    /// <summary>
    /// PARÁMETRO VIEWSTATE SELECCIONADO
    /// </summary>
    public const string PAR_VIEWSTATE_SELECCIONADO = "Seleccionado";
    #endregion

    #region Perfiles de usuario
    /// <summary>
    /// PERFIL DE USUARIO FUNCIÓN PERITO
    /// </summary>
    public const string FUN_PERITO = "FunPerito";
    /// <summary>
    /// PERFIL DE USUARIO FUNCIÓN SOCIEDAD
    /// </summary>
    public const string FUN_SOCIEDAD = "FunSociedad";
    /// <summary>
    /// PERFIL DE USUARIO FUNCIÓN FINANZAS
    /// </summary>
    public const string FUN_FINANZAS = "Funfinanzas";
    /// <summary>
    /// PERFIL DE USUARIO FUNCIÓN DICTAMENES
    /// </summary>
    public const string FUN_DICTAMENES = "FunDictamenes";
    #endregion

    #region EXTENSIONES FICHEROS
    /// <summary>
    /// TIPO DE CODIFICACIÓN EN XML
    /// </summary>
    public const string XML_ENCODING = "utf-8";
    /// <summary>
    /// TIPO DE EXTENSIÓN DEL FICHERO XML
    /// </summary>
    public const string XML_FILE_EXTENSION = ".xml";
    /// <summary>
    /// TIPO DE EXTENSIÓN EXCEL
    /// </summary>
    public const string EXCEL_FILE_EXTENSION = "Excel";
    #endregion

    #region RUTAS XML AVALUO
    /// <summary>
    /// IDENTIFICADOR DE LOS ELEMENTOS DEL XML
    /// </summary>
    public const string XML_IDENTIFICADOR_ELEMENTOS = "id";
    /// <summary>
    /// NODO RAÍZ DEL XML
    /// </summary>
    public const string NODO_RAIZ = "Avaluo";
    /// <summary>
    /// RUTA EN EL XML DE LA CUENTA CATASTRAL
    /// </summary>
    public const string RUTA_CUENTACAT = "Avaluo//Antecedentes/InmuebleQueSeValua/CuentaCatastral/";
    /// <summary>
    /// RUTA EN EL XML DEL PROPIETARIO
    /// </summary>
    public const string RUTA_PROP = "Avaluo//Antecedentes/Propietario/";
    /// <summary>
    /// RUTA EN EL XML DEL INMUBLE QUE SE VALÚA
    /// </summary>
    public const string RUTA_INM = "Avaluo//Antecedentes/InmuebleQueSeValua/";
    /// <summary>
    /// RUTA EN EL XML DE ESCRITURA
    /// </summary>
    public const string RUTA_ESC = "Avaluo//Terreno/MedidasYColindancias/FuenteDeInformacionLegal/Escritura/";
    /// <summary>
    /// RUTA EN EL XML DE SETENCIA
    /// </summary>
    public const string RUTA_SEN = "Avaluo//Terreno/MedidasYColindancias/FuenteDeInformacionLegal/Sentencia/";
    /// <summary>
    /// RUTA EN EL XML DEL CONTRATO PRIVADO
    /// </summary>
    public const string RUTA_CON = "Avaluo//Terreno/MedidasYColindancias/FuenteDeInformacionLegal/ContratoPrivado/";
    /// <summary>
    /// RUTA EN EL XML DEL ALINEAMIENTO Y NÚMERO OFICIAL
    /// </summary>
    public const string RUTA_ALI = "Avaluo//Terreno/MedidasYColindancias/FuenteDeInformacionLegal/AlineamientoYNumeroOficial/";
    /// <summary>
    /// RUTA EN EL XML DE IDENTIFICACIÓN
    /// </summary>
    public const string RUTA_IDENTIF = "Avaluo//Identificacion";
    #endregion

    #region MENSAJES
    /// <summary>
    /// MENSAJE DE ERROR EN LA APLICACIÓN
    /// </summary>
    public const string MSJ_ERROR_APLICACION = "Error en la aplicación";
    /// <summary>
    /// MENSAJE DE DÍGITO VERIFICADOR NO CORRECTO
    /// </summary>
    public const string MSJ_DIGITO_VERIFICADOR_ERRONEO = "El dígito verificador no es correcto";
    /// <summary>
    /// MENSAJE DE CUENTA CATASTRAL NO VÁLIDA
    /// </summary>
    public const string MSJ_CUENTACAT_INVALIDA = "La cuenta catastral no es válida.";
    /// <summary>
    /// MENSAJE DE USUARIO NO ENCONTRADO
    /// </summary>
    public const string MSJ_USUARIONOEXISTE_EXECP = "No se ha encontrado usuario en RCON.";
    /// <summary>
    /// MENSAJE DE EXCEPCIÓN TOKEN
    /// </summary>
    public const string MSJ_TOKEN_EXCEPTION = "Error: <LI>";
    /// <summary>
    /// MENSAJE INFORMATIVO DE REGISTRO CORRECTO DEL AVALÚO Y DE QUE LOS AVALÚOS ESPECIALES NO SE VEN EN LA BANDEJA DE ENTRADA NI SE PUEDEN GESTIONAR A TRAVÉS DEL SISTEMA
    /// </summary>
    public const string MSJ_SUBIRAVALUO_REGISTRADOCORRECTAMENTE = "El avalúo se ha registrado correctamente. </br></br> Nota:</br> Los avalúos especiales no se ven en la bandeja de entrada ni se pueden gestionar a través de sistema.";
    /// <summary>
    /// MENSAJE INFORMATIVO DE REGISTRO CORRECTO DEL AVALÚO
    /// </summary>
    public const string MSJ_SUBIRAVALUO_NORMAL_REGISTRADOCORRECTAMENTE = "El avalúo se ha registrado correctamente.";
    /// <summary>
    /// MENSAJE DE ERROR AL REGISTRAR EL INTENTO FALLIDO EN LA BD
    /// </summary>
    public const string MSJ_REGISTRAR_INTENTOFALLIDO = "Error al registrar el intento fallido en la BD";
    /// <summary>
    /// MENSAJE DE EXCESO DE TAMAÑO MÁXIMO PERMITIDO DEL FICHERO
    /// </summary>
    public const string MSJ_TAMANIO_FICHERO = "El fichero excede el tamaño máximo permitido";
    /// <summary>
    /// MENSAJE INFORMATIVO - NO SE HA SELECCIONADO NINGÚN FICHERO
    /// </summary>
    public const string MSJ_FICHERO_VACIO = "No se ha seleccionado ningún fichero";
    /// <summary>
    /// MENSAJE NÚMERO ÚNICO DE AVALÚO 
    /// </summary>
    public const string MSJ_NUM_UNICO = "Número único del avalúo: ";
    /// <summary>
    /// MENSAJE DE AVISO DOCUMENTO XML XON FORMATO ERRÓNEO
    /// </summary>
    public const string MSJ_XML_ERROR = "Documento XML con formato erróneo";
    /// <summary>
    /// MENSAJE DE ERROR - SE PRESENTÓ UN ERROR AL REALIZAR LA OPERACIÓN
    /// </summary>
    public const string MSJ_ERROR_OPERACION = "Se presentó un error al realizar la operación: ";
    /// <summary>
    /// MENSAJE DE ERROR FALTAN DATOS XML - NO SE DISPONE DEL DOCUMENTO DIGITAL DEL AVALÚO POR LO QUE ALGUNAS FUNCIONALIDADES NO ESTARÁN DISPONIBLES
    /// </summary>
    public const string MSJ_ERROR_FALTANDATOSXML = "No se dispone del documento digital del avalúo por lo que algunas funcionalidades no estarán disponibles";
    /// <summary>
    /// MENSAJE DE ERROR FALTAN DATOS XML NO DETALLE - NO SE DISPONE DEL DOCUMENTO DIGITAL DEL AVALÚO POR LO QUE NO SE PUEDE MOSTRAR EL DETALLE DEL AVALÚO
    /// </summary>
    public const string MSJ_ERROR_FALTANDATOSXML_NODETALLE = "No se dispone del documento digital del avalúo por lo que no se puede mostrar el detalle del avalúo";
    /// <summary>
    /// MENSAJE DE ERROR - EL FICHERO NO ES UN DOCUMENTO XML
    /// </summary>
    public const string MSJ_ERROR_NO_ES_DOCUMENTO_XML = "El fichero no es un documento XML";
    /// <summary>
    /// MENSAJE INFORMATIVO SOBRE EL NÚMERO DE AVALÚOS ENCONTRADOS
    /// </summary>
    public const string MSJ_NUN_AVALUOS_ENCONTRADOS = "Se ha encontrado un total de {0} avalúo(s)";

    /// <summary>
    /// MENSAJE INFORMATIVO BUSQUEDA INFORMACION DE MERCADO
    /// </summary>
    public const string MSJ_BUSQUEDA_INF_MERCADO = "No fue posible obtener la consulta, favor de especificar más los filtros de búsqueda o reducir el rango de fechas.";
    #endregion

    #region DDL

    /// <summary>
    /// DROPDOWNLIST ESTADO DESCRIPCIÓN
    /// </summary>
    public const string DDLESTADO_DESCRIPCION = "Descripcion";
    /// <summary>
    /// DROPDOWNLIST ESTADO CÓDIGO
    /// </summary>
    public const string DDLESTADO_COD = "Codigo";
    /// <summary>
    /// DROPDOWNLIST ESTADO VALOR ENVIADO NOTARIO
    /// </summary>
    public const string DDLESTADO_VALUE_ENVIADONOTARIO = "6";
    /// <summary>
    /// DROPDOWNLIST ESTADO VALOR TODOS
    /// </summary>
    public const string DDLESTADO_VALUE_TODOS = "0";
    /// <summary>
    /// DROPDOWNLIST ESTADO TEXTO TODOS
    /// </summary>
    public const string DDLESTADO_TEXT_TODOS = "Todos";
    
    /// <summary>
    /// DROPDOWNLIST BÚSQUEDA PERITOS
    /// </summary>
    public const string DDLBUSQUEDA_PERITOS = "perito";

    /// <summary>
    /// DROPDOWNLIST BÚSQUEDA SOCIEDADES
    /// </summary>
    public const string DDLBUSQUEDA_SOCIEDADES = "sociedad";
    #endregion

    #region CORREO NOTARIO
    /// <summary>
    /// PARÁMETRO NOMBRE COMPLETO EN EL ENVÍO DE CORREO AL NOTARIO
    /// </summary>
    public const string PAR_ENVNOT_NOMBRECOMPLETO = "<%NombreCompleto%>";
    /// <summary>
    /// PARÁMETRO TIPO PERSONA EN EL ENVÍO DE CORREO AL NOTARIO
    /// </summary>
    public const string PAR_ENVNOT_TIPOPERSONA = "<%TipoPersona%>";
    /// <summary>
    /// PARÁMETRO REGISTRO EN EL ENVÍO DE CORREO AL NOTARIO
    /// </summary>
    public const string PAR_ENVNOT_REGISTRO = "<%Registro%>";
    /// <summary>
    /// PARÁMETRO VALOR EN EL ENVÍO DE CORREO AL NOTARIO
    /// </summary>
    public const string PAR_ENVNOT_VALOR = "<%Valor%>";
    /// <summary>
    /// PARÁMETRO CUENTA CATASTRAL EN EL ENVÍO DE CORREO AL NOTARIO
    /// </summary>
    public const string PAR_ENVNOT_CUENTACAT = "<%CuentaCatastral%>";
    /// <summary>
    /// PARÁMETRO FECHA EN EL ENVÍO DE CORREO AL NOTARIO
    /// </summary>
    public const string PAR_ENVNOT_FECHA = "<%Fecha%>";
    /// <summary>
    /// PARÁMETRO ORIGEN EMAIL EN EL ENVÍO DE CORREO AL NOTARIO
    /// </summary>
    public const string PAR_ENVNOT_EMAIL_ORIGEN = "fromEmail";
    /// <summary>
    /// PARÁMETRO TEMPLATE FICHERO EN EL ENVÍO DE CORREO AL NOTARIO
    /// </summary>
    public const string PAR_ENVNOT_FICHERO_TEMPLATE = "/Template.htm";
    /// <summary>
    /// PARÁMETRO TEMPLATE RUTA EN EL ENVÍO DE CORREO AL NOTARIO
    /// </summary>
    public const string PAR_ENVNOT_RUTA_TEMPLATE = "rutaTemplateEmail";
    /// <summary>
    /// PARÁMETRO CONFIRMACIÓN ENVÍO EN EL ENVÍO DE CORREO AL NOTARIO
    /// </summary>
    public const string PAR_ENVNOT_CONFIRMACION_ENVIO = "Se ha enviado un correo al notario avisando de la asignación del avalúo realizada. Si el correo no aparece en la bandeja de entrada revisar la carpeta spam.";
    /// <summary>
    /// PARÁMETRO ERROR NO EMAIL EN EL ENVÍO DE CORREO AL NOTARIO
    /// </summary>
    public const string PAR_ENVNOT_ERROR_NOEMAIL = "No se ha podido enviar un correo por no tener el email.";
    /// <summary>
    /// PARÁMETRO ERROR DESCONOCIDO EN EL ENVÍO DE CORREO AL NOTARIO
    /// </summary>
    public const string PAR_ENVNOT_ERROR_DESCONOCIDO = "Error en el envio de correo al notario.";
    /// <summary>
    /// PARÁMETRO ERROR OTROS EN EL ENVÍO DE CORREO AL NOTARIO
    /// </summary>
    public const string PAR_ENVNOT_ERROR_OTROS = "No se ha enviado un correo al notario.\nError: ";
    /// <summary>
    /// PARÁMETRO ASUNTO EN EL ENVÍO DE CORREO AL NOTARIO
    /// </summary>
    public const string PAR_ENVNOT_SUBJECT = "Decisión avaluo ";

    #endregion

    #region SORT DIRECTION
    /// <summary>
    /// DIRECCIÓN DE ORDENACIÓN ASCENDENTE
    /// </summary>
    public const string SORTDIRECTION_ASCENDING = "Ascending";
    /// <summary>
    /// DIRECCIÓN DE ORDENACIÓN DESCENDENTE
    /// </summary>
    public const string SORTDIRECTION_DESCENDING = "Descending";
    /// <summary>
    /// DIRECCIÓN DE ORDENACIÓN DESCENDENTE FECHA ORDENACIÓN
    /// </summary>
    public const string SORTDIRECTION_DEFAULT_FECHAPRES = "FECHA_PRESENTACION DESC";
    /// <summary>
    /// DIRECCIÓN DE ORDENACIÓN DESCENDENTE REGISTRO PERITOS
    /// </summary>
    public const string SORTDIRECTION_DEFAULT_PERITOS = "REGISTRO DESC";
    #endregion

    #region SIMBOLOS
    /// <summary>
    /// CARÁCTER SEPARADOR CAMPOS FILTRO
    /// </summary>
    public const char CARAC_SEPARADOR_CAMPOSFILTRO = ';';
    /// <summary>
    /// SÍMBOLO TRES PUNTOS
    /// </summary>
    public const string SIMBOLO_TRESPUNTOS = " ...";
    /// <summary>
    /// SÍMBOLO ESPACIO EN BLANCO
    /// </summary>
    public const string ESPACIO_BLANCO = " ";
    /// <summary>
    /// SÍMBOLO UNIÓN CAMPOS CUENTA CATASTRAL
    /// </summary>
    public const string CUENTACAT_SIMBOLO_UNION_CAMPOS = "-";
    /// <summary>
    /// SÍMBOLO GUIÓN
    /// </summary>
    public const string SIMBOLO_GUION = "-";
    /// <summary>
    /// SÍMBOLO DOS PUNTOS
    /// </summary>
    public const string SIMBOLO_DOSPUNTOS = ": ";
    #endregion

    /// <summary>
    /// SETTINGS DE LA APLICACIÓN - CLAVE GRID TAMAÑO PAGINACIÓN
    /// </summary>
    public const string APPSETTINGS_KEY_GRID_PAGESIZE = "PageSize_BandejaEntrada";

    /// <summary>
    /// VALOR NO SELECCIONADO
    /// </summary>
    public const string VALUE_NO_SELECT = "-1";

    //TIPOS ERROR VALIDACIÓN VUS
    /// <summary>
    /// ERROR VALIDACIÓN VUS - CÓDIGO SIN VALORES
    /// </summary>
    public const string ERROR_VUS_COD_SINVALORES = "0";
    /// <summary>
    /// ERROR VALIDACIÓN VUS - CÓDIGO FUERA DE RANGO
    /// </summary>
    public const string ERROR_VUS_COD_FUERADERANGO = "1";

    #region nombre de los parámetros
    //NOMBRE DE LOS PARAMETROS
    /// <summary>
    /// PARÁMETRO PÁGINA ORIGEN
    /// </summary>
    public const string PAR_PAGINAORIGEN = "PaginaOrigen";
    /// <summary>
    /// PARÁMETRO OPERACIÓN
    /// </summary>
    public const string PAR_OPERACION = "Operacion";
    /// <summary>
    /// PARÁMETRO IDENTIFICADOR AVALÚO
    /// </summary>
    public const string PAR_IDAVALUO = "IdAvaluo";
    /// <summary>
    /// PARÁMETRO NÚMERO ÚNICO
    /// </summary>
    public const string PAR_NUMUNIAVALUO = "numeroUnico";
    /// <summary>
    /// PARÁMETRO IDENTIFICADOR AVALÚO PRÓXIMO
    /// </summary>
    public const string PAR_IDAVALUO_PROXIMO = "IdAvaluoProximo";
    /// <summary>
    /// PARÁMETRO CUENTA CATASTRAL
    /// </summary>
    public const string PAR_CUENTACAT = "CuentaCatastral";
    /// <summary>
    /// PARÁMETRO IDENTIFICADOR DOCUMENTO FOTO
    /// </summary>
    public const string PAR_IDDOCUMENTOFOTO = "IdDocumentoFoto";
    /// <summary>
    /// PARÁMETRO TAMAÑO FOTO
    /// </summary>
    public const string PAR_TAMANIOFOTO = "TamanioFoto";
    /// <summary>
    /// PARÁMETRO TIPO FOTO
    /// </summary>
    public const string PAR_TIPO_FOTO = "Tipo";
    /// <summary>
    /// PARÁMETRO FOTO DOCUMENTO DIGITAL
    /// </summary>
    public const string PAR_FOTO_DOCDIG = "DD";
    /// <summary>
    /// PARÁMETRO FOTO FICHERO
    /// </summary>
    public const string PAR_FOTO_FICHERO = "DF";
    #endregion 

    //NOMBRES DE LAS VARIABLES DE SESION
    /// <summary>
    /// VARIABLE DE SESIÓN DE FUENTES EXTERNAS
    /// </summary>
    public const string SES_SESIONFUENTESEXTERNAS = "SesionFuentesExternas";

    #region errores
    //ERRORES
    /// <summary>
    /// PARÁMETRO ERROR
    /// </summary>
    public const string PAR_ERROR = "error";
    /// <summary>
    /// PARÁMETRO ERROR CUENTA CATASTRAL INMUEBLE
    /// </summary>
    public const string PAR_ERROR_CC_INMUEBLE = "errorCcInmb";
    /// <summary>
    /// PARÁMETRO ERROR EXCEPCIÓN USUARIO
    /// </summary>
    public const string PAR_ERROR_USEREXCEPTION = "userExecp";
    /// <summary>
    /// PARÁMETRO ERROR TOKEN
    /// </summary>
    public const string PAR_ERROR_TOKEN = "errorToken";
    /// <summary>
    /// PARÁMETRO MENSAJE ERROR TOKEN
    /// </summary>
    public const string PAR_ERROR_MSG = "MsgToken";
    #endregion

    /// <summary>
    /// PARÁMETRO VALOR IDENTIFICADOR MARCAJE CUENTA DICTAMINADO
    /// </summary>
    public const string PAR_VAL_IDMARCAJECUENTA_DICTAMINANDO = "M0097";
    /// <summary>
    /// NOMBRE PREFIJO FICHERO AVALÚO
    /// </summary>
    public const string NOMBRE_FICHEROAV_PREFIJO = "Avaluo";
    //Dataset peritos
    /// <summary>
    /// COLUMNA NOMBRE COMPLETO DE DATASET DE PERITOS
    /// </summary>
    public const string NOMBRE_COMPLETO_COLUMN = "NombreCompleto"; //Si se cambia el nombre de la colunma cambiarlo en el gridview
}
