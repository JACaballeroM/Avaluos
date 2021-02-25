using System;
using System.Data;


/// <summary>
/// Clase estatica con las constantes del servicio avalúos
/// </summary>
public static class Constantes
{
    #region TIPOS AVALUO
    
    /// <summary>
    /// Etiqueta xml para los avalúos de tipo catastral
    /// </summary>
    public const string XML_TIPO_CATASTRAL = "Catastral";
    
    /// <summary>
    /// Etiqueta xml para los avalúos de tipo comercial
    /// </summary>
    public const string XML_TIPO_COMERCIAL = "Comercial";

    /// <summary>
    /// ABREVIATURA AVALUO COMERCIAL MAYUSCULAS
    /// </summary>
    public const string PAR_AVALUO_COMERCIAL_SHORT_MAYUS = "COM";

    /// <summary>
    /// ABREVIATURA AVALUO CATASTRAL MAYUSCULAS
    /// </summary>
    public const string PAR_AVALUO_CATASTRAL_SHORT_MAYUS = "CAT";
    
    #endregion 


    #region COLUNMAS DESCRIPCIONES
    
    /// <summary>
    /// COLUMNA DESCRIPCIÓN CLASE VIDA
    /// </summary>
    public const string COL_DESC_CLASEVIDA = "DescClaseVida";
    /// <summary>
    /// COLUMNA DESCRIPCIÓN CONSERVACION
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
    /// COLUMNA DESCRIPCIÓN CLASE CONST
    /// </summary>
    public const string COL_DESC_CLASECONST = "DescClaseConst";
    /// <summary>
    /// COLUMNA DESCRIPCIÓN INSTALACIONES ESPECIALES
    /// </summary>
    public const string COL_DESC_INSTESPECIAL = "DescInstEsp";
    /// <summary>
    /// COLUMNA DESCRIPCIÓN COLONIA
    /// </summary>
    public const string COL_DESC_COLONIA = "DescColonia";
    /// <summary>
    /// COLUMNA DESCRIPCIÓN DELEGACIÓN
    /// </summary>
    public const string COL_DESC_DELEGACION = "DescDeleg";

    #endregion 
    
    #region CATALOGOS
    #region CATALOGO CATTIPOFUENTEINFO
    /// <summary>
    /// PARÁMETRO CÓDIGO TIPO FUENTE INFORMACIÓN ESCRITURA
    /// </summary>
    public const string PAR_COD_TIPOFUENTEINFO_ESCRITURA = "1";
    /// <summary>
    /// PARÁMETRO CÓDIGO TIPO FUENTE INFORMACIÓN SENTENCIA
    /// </summary>
    public const string PAR_COD_TIPOFUENTEINFO_SENTENCIA = "2";
    /// <summary>
    /// PARÁMETRO CÓDIGO TIPO FUENTE INFORMACIÓN CONTRATO
    /// </summary>
    public const string PAR_COD_TIPOFUENTEINFO_CONTRATO = "3";
    /// <summary>
    /// PARÁMETRO CÓDIGO TIPO FUENTE INFORMACIÓN ALINEAMIENTO
    /// </summary>
    public const string PAR_COD_TIPOFUENTEINFO_ALINEAMIENTO = "4";
    #endregion

    #region CATALOGO CATTIPOCOMPARABLE
    /// <summary>
    /// PARÁMETRO CÓDIGO TIPO COMPARABLE RENTA
    /// </summary>
    public const string PAR_COD_TIPOCOMPARABLE_RENTA = "R";
    /// <summary>
    /// PARÁMETRO CÓDIGO TIPO COMPARABLE VENTA
    /// </summary>
    public const string PAR_COD_TIPOCOMPARABLE_VENTA = "V";
    #endregion

    #region CATALOGO CATMODOCONSTRUCCION
    /// <summary>
    /// PARÁMETRO CÓDIGO MODO CONSTRUCCION RENTA
    /// </summary>
    public const string PAR_COD_MODOCONSTRUCCION_RENTA = "R";
    /// <summary>
    /// PARÁMETRO CÓDIGO MODO CONSTRUCCION VENTA
    /// </summary>
    public const string PAR_COD_MODOCONSTRUCCION_VENTA = "V";
    #endregion
    
    #region CATALOGO CATTIPOTRAMITE
    /// <summary>
    /// PARÁMETRO CÓDIGO TIPO TRÁMITE COMERCIAL
    /// </summary>
    public const string PAR_COD_TIPOTRAMITE_COMERCIAL = "1";
    /// <summary>
    /// PARÁMETRO CÓDIGO TIPO TRÁMITE CATASTRAL
    /// </summary>
    public const string PAR_COD_TIPOTRAMITE_CATASTRAL = "2";
    #endregion 
    #endregion

    #region TIPOS REGIMEN DE PROPIEDAD
    /// <summary>
    ///TIPO REGIMEN PRIVADA
    /// </summary>
    public const string REGIMEN_PRIVADA = "1";
    /// <summary>
    ///TIPO REGIMEN CONDOMINIAL
    /// </summary>
    public const string REGIMEN_CONDOMINIAL = "2";
    /// <summary>
    ///TIPO REGIMEN COPROPIEDAD
    /// </summary>
    public const string REGIMEN_COPROPIEDAD = "3";
    /// <summary>
    ///TIPO REGIMEN PÚBLICA
    /// </summary>
    public const string REGIMEN_PUBLICA = "4";
    #endregion

    #region TIPOS VALIDACIONES
    /// <summary>
    /// TIPO VALIDACIÓN TIPO DE CONDOMINIO
    /// </summary>
    public const string TIPO_VAL_TIPOCONDOMINIO = "TIPO DE CONDOMINIO";
    /// <summary>
    /// TIPO VALIDACIÓN PERITO Y SOCIEDAD
    /// </summary>
    public const string TIPO_VAL_PERITOSOCIEDAD = "PERITO Y SOCIEDAD";
    /// <summary>
    /// TIPO VALIDACIÓN IMAGEN
    /// </summary>
    public const string TIPO_VAL_IMAGEN = "IMAGEN";
    /// <summary>
    /// TIPO VALIDACIÓN CUENTA CATASTRAL
    /// </summary>
    public const string TIPO_VAL_CUENTACAT = "CUENTA CATASTRAL";
    /// <summary>
    /// TIPO VALIDACIÓN DESCRIPCIÓN INMUEBLE
    /// </summary>
    public const string TIPO_VAL_DESC_INMUEBLE = "DESCRIPCIÓN INMUEBLE";
    /// <summary>
    /// TIPO VALIDACIÓN ENFOQUE COSTOS
    /// </summary>
    public const string TIPO_VAL_COSTOS = "ENFOQUE COSTOS";
    /// <summary>
    /// TIPO VALIDACIÓN CARACTERÍSTICAS URBANAS
    /// </summary>
    public const string TIPO_VAL_CURBANAS = "CARACTERÍSTICAS URBANAS";
    /// <summary>
    /// TIPO VALIDACIÓN CAMPOS CALCULADOS
    /// </summary>
    public const string TIPO_VAL_CCALCULADOS = "CAMPOS CALCULADOS";
    /// <summary>
    /// TIPO VALIDACIÓN Nº AVALÚO
    /// </summary>
    public const string TIPO_VAL_NUM_AVALUO = "Nº AVALÚO";
    /// <summary>
    /// TIPO VALIDACIÓN ESQUEMA - DOCUMENTO NO VÁLIDO
    /// </summary>
    public const string TIPO_VAL_ERRORESQUEMA = "ESQUEMA - DOCUMENTO NO VÁLIDO";
    #endregion 

    #region TIPOS TERRENOS
    /// <summary>
    /// TIPO TIPOTERRENO RESIDUAL
    /// </summary>
    public const string TIPO_TIPOTERRENO_RESIDUAL = "R";
    /// <summary>
    /// TIPO TIPOTERRENO DIRECTO
    /// </summary>
    public const string TIPO_TIPOTERRENO_DIRECTO = "D";

    #endregion 

    #region MSJ_ERROR
    /// <summary>
    /// MENSAJE ERROR GENERAL OPERACIÓN 
    /// </summary>
     public const string MSJ_ERROR_OPERACION = "Se presentó un error al realizar la operación: ";
    /// <summary>
    /// MENSAJE ERROR TIMEOUT
    /// </summary>
     public const string PAR_ERROR_TIMEOUT =  "No se ha recibido respuesta de la aplicación en el tiempo establecido";
     #endregion

    #region MENSAJES DE ERROR DE LAS VALIDACIONES
    /// <summary>
    /// MENSAJE ERROR DÍGITO VERIFICADOR
    /// </summary>
    public const string PAR_ERROR_DIGITOVER = "El dígito verificador no es correcto";
    /// <summary>
    /// MENSAJE ERROR CUENTA CATASTRAL NO EXISTE
    /// </summary>
    public const string PAR_ERROR_CUENTACAT = "La cuenta catastral no existe";
    /// <summary>
    /// MENSAJE CLAVE ERROR VALIDACIÓN
    /// </summary>
    public const string CLAVE_ERRORVALIDACION = "@TIPO_ERROR:";
    /// <summary>
    /// MENSAJE ERROR FORMATO XML
    /// </summary>
    public const string PAR_VAL_ERRORFORMATOXML = "Error al leer archivo XML. Formato de fichero incorrecto";
    /// <summary>
    /// MENSAJE ERROR DIVISIÓN POR CERO
    /// </summary>
    public const string PAR_ERROR_DIVISIONPORCERO = " No se puede dividir por cero";
    /// <summary>
    /// MENSAJE ERROR TIPO DE DATO
    /// </summary>
    public const string PAR_ERROR_ERRORTIPODATO = " Tipo de dato erróneo";
    /// <summary>
    /// MENSAJE ERROR NÚMERO AVALÚO REPETIDO
    /// </summary>
    public const string PAR_ERROR_NUMAVALUO_REPETIDO = "Ya existe un avalúo registrado con nº avalúo ";
    /// <summary>
    /// MENSAJE ERROR TIPOPERSONA SOLICITANTE
    /// </summary>
    public const string PAR_ERROR_TIPOPERS_SOL_MORAL_P = "  b.1.10 - Error en el tipo de persona: La persona moral no puede tener apellido paterno (b.1.1)";

    /// <summary>
    /// MENSAJE ERROR TIPOPERSONA SOLICITANTE
    /// </summary>
    public const string PAR_ERROR_TIPOPERS_SOL_MORAL_M = "  b.1.10 - Error en el tipo de persona: La persona moral no puede tener apellido materno (b.1.2)";


    /// <summary>
    /// MENSAJE ERROR TIPOPERSONA SOLICITANTE
    /// </summary>
    public const string PAR_ERROR_TIPOPERS_SOL_FISICA = "  b.1.10 - Error en el tipo de persona: La persona física debe contener el apellido paterno (b.1.1)";
   
    
    /// <summary>
    /// MENSAJE ERROR TIPOPERSONA SOLICITANTE
    /// </summary>
    public const string PAR_ERROR_TIPOPERS_PROP_MORAL_P = "  b.2.10 - Error en el tipo de persona: La persona moral no puede tener apellido paterno (b.2.1)";

    /// <summary>
    /// MENSAJE ERROR TIPOPERSONA SOLICITANTE
    /// </summary>
    public const string PAR_ERROR_TIPOPERS_PROP_MORAL_M = "  b.2.10 - Error en el tipo de persona: La persona moral no puede tener apellido materno (b.2.2)";


    /// <summary>
    /// MENSAJE ERROR TIPOPERSONA SOLICITANTE
    /// </summary>
    public const string PAR_ERROR_TIPOPERS_PROP_FISICA = "  b.2.10 - Error en el tipo de persona: La persona física debe contener el apellido paterno (b.2.1)";
    

    /// MENSAJE ERROR VALOR DIFERIDO
    /// </summary>
    public const string PAR_ERROR_VALORREFERIDO = "Valor referido ";
    /// <summary>
    /// MENSAJE ERROR FALTAN DATOS PARA VALIDACIÓN DE VALOR UNITARIO
    /// </summary>
    public const string PAR_ERROR_VUS_FALTADATOS = "No ha sido posible hacer la validación de valor unitario de suelo por falta de datos. ¿Desea continuar?";
    /// <summary>
    /// MENSAJE ERROR VUS FUERA DE RANGO
    /// </summary>
    public const string PAR_ERROR_VUS_FUERARANGO = "El valor unitario de suelo del avalúo no se encuentra entre en el rango del valor mínimo y máximo de VUS en base a la media para el área de valor. ¿Desea continuar?";
    /// <summary>
    /// MENSAJE ERROR DELEGACIÓN COLONIA
    /// </summary>
    public const string PAR_ERROR_DELEGACIONCOLONIA = "La colonia no pertenece a la delegación especificada";
    /// <summary>
    /// MENSAJE ERROR VALIDAR CÁLCULO
    /// </summary>
    public const string PAR_ERROR_VALIDARCALCULO = " Error al validar el cálculo.";
   
     /// <summary>
    /// MENSAJE IDENTIFICACIÓN INSTALACIÓN
    /// </summary>
    public const string PAR_CLAVEINST = " En la instalación con clave ";

    /// <summary>
    /// MENSAJE IDENTIFICACIÓN OBRA
    /// </summary>
    public const string PAR_CLAVEOBRA = " En la obra con clave ";

    /// <summary>
    /// MENSAJE IDENTIFICACIÓN ELEMTO
    /// </summary>
    public const string PAR_CLAVEELEMTO = " En la elemento con clave ";

    #region Peritos/sociedades
    /// <summary>
    /// PARÁMETRO ERROR PERITO LOGEADO NO VIGENTE
    /// </summary>
    public const string PAR_ERROR_PERITO_LOGEADO_NOVIGENTE = "El perito identificado en la aplicación no se encuentra vigente, no puede cargar el fichero avalúo.";
    /// <summary>
    /// PARÁMETRO ERROR PERITO ERRÓNEO
    /// </summary>
    public const string PAR_ERROR_PERITO_ERRONEO = "Un perito no puede subir avalúos en nombre de otro perito.";
    /// <summary>
    /// PARÁMETRO ERROR PERITO NO EXISTE
    /// </summary>
    public const string PAR_ERROR_PERITO_NOEXISTE = "El perito valuador del avalúo no existe.";
    /// <summary>
    /// PARÁMETRO ERROR PERITO XML NO VIGENTE
    /// </summary>
    public const string PAR_ERROR_PERITO_XML_NOVIGENTE = "El perito valuador del avaluo no se encuentra vigente.";
    /// <summary>
    /// PARÁMETRO ERROR SOCIEDAD NO VIGENTE 
    /// </summary>
    public const string PAR_ERROR_SOCIEDAD_NOVIGENTE = "La sociedad de valuación no se encuentra vigente";
    /// <summary>
    /// PARÁMETRO ERROR PERITO ERRONEA
    /// </summary>
    public const string PAR_ERROR_PERITO_ERRONEA = "Una sociedad no puede subir avalúos en nombre de otra sociedad";
    /// <summary>
    /// PARÁMETRO ERROR CLAVE SOCIEDAD NO CORRESPONDE CON LA ASOCIADA AL PERITO
    /// </summary>
    public const string PAR_ERROR_PERITO_SOCI = "La clave de Sociedad no corresponde con la asociada al perito";
    /// <summary>
    /// PARÁMETRO ERROR PERITO NO PUEDE SUBIR AVALÚOS EN NOMBRE DE SOCIEDAD 
    /// </summary>
    public const string PAR_ERROR_TIPOVALUADOR = "Un perito no puede subir avalúos en nombre de una sociedad.";
    #endregion

    #region C: Caracterísiticas urbanas
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO C.6.4 - COEFICIENTE DE USO DEL SUELO
    /// </summary>
    public const string PAR_ERROR_VALORCAL_C_6_4 = "c.6.4 - Coeficiente de uso del suelo.";
    #endregion

    #region E: Descripción del inmueble
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO E.2.1.N.9 - VIDA ÚTIL REMANENTE CONTRUCCIONES PRIVATIVAS
    /// </summary>
    public const string PAR_ERROR_VALORCAL_E_2_1_N_9 = "e.2.1.n.9 - Vida útil remanente contrucciones privativas.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO E.2.5.N.9 - VIDA ÚTIL REMANENTE CONSTRUCCIONES COMUNES
    /// </summary>
    public const string PAR_ERROR_VALORCAL_E_2_5_N_9 = "e.2.5.n.9 - Vida útil remanente construcciones comunes.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO E.3 - VIDA ÚTIL TOTAL PROMEDIO DEL INMUEBLE
    /// </summary>
    public const string PAR_ERROR_VALORCAL_E_3 = "e.3 - Vida útil total promedio del inmueble.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO E.4 - EDAD PROMEDIO DEL INMUEBLE
    /// </summary>
    public const string PAR_ERROR_VALORCAL_E_4 = "e.4 - Edad promedio del inmueble.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO E.5 - VIDA ÚTIL REMANENTE PROMEDIO DEL INMUEBLE
    /// </summary>
    public const string PAR_ERROR_VALORCAL_E_5 = "e.5 - Vida útil remanente promedio del inmueble.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO E.2.1.N.8 - LA VIDA ÚTIL ESPECIFICADA NO ES CORRECTA PARA LA CLASE Y EL USO ESPECIFICADOS
    /// </summary>
    public const string PAR_ERROR_VALORCAL_E_2_1_N_8 = "e.2.1.n.8 - La vida útil especificada no es correctapara la clase y el uso especificados";
    /// PARÁMETRO ERROR VALOR CALCULADO E.2.5.N.8 - LA VIDA ÚTIL ESPECIFICADA NO ES CORRECTA PARA LA CLASE Y EL USO ESPECIFICADOS
    /// </summary>
    public const string PAR_ERROR_VALORCAL_E_2_5_N_8 = "e.2.5.n.8 - La vida útil especificada no es correcta para la clase y el uso especificados";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO H.1.1.N.17 - FRE
    /// </summary>
    public const string PAR_ERROR_VALORCAL_H_1_1_N_17 = "h.1.1.n.17 - Fre.";
    /// <summary>
    /// JACM Se da de baja el campo 2021-02-04
    /// PARÁMETRO ERROR VALOR CALCULADO E.2.1.N.10 - CLAVE CONSERVACION
    /// </summary>
    /// JACM Se da de baja el campo 2021-02-04
    //public const string PAR_ERROR_VALORCAL_E_2_1_N_10 = "e.2.1.n.10 - Clave de Conservación.";
    /// PARÁMETRO ERROR VALOR CALCULADO E.2.5.N.10 - CLAVE CONSERVACION
    /// </summary>
    // JACM Se da de baja el campo 2021-02-04
    //public const string PAR_ERROR_VALORCAL_E_2_5_N_10 = "e.2.5.n.10 - Clave de Conservación.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO E.2.1.N.13 - FACTOR DE EDAD
    /// </summary>
    public const string PAR_ERROR_VALORCAL_E_2_1_N_13 = "e.2.1.n.13 - Indice del costo remanente.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO E.2.1.N.14 - FACTOR RESULTANTE
    /// </summary>
    /// // JACM Se da de baja el campo 2021-02-04
    //public const string PAR_ERROR_VALORCAL_E_2_1_N_14 = "e.2.1.n.14 - Factor resultante.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO E.2.1.N.15 - VALOR DE LA FRACCIÓN N
    /// </summary>
    public const string PAR_ERROR_VALORCAL_E_2_1_N_15 = "e.2.1.n.15 - Valor de la fracción n.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO E.2.2 - SUPERFICIE TOTAL DE CONSTRUCCIONES PRIVATIVAS
    /// </summary>
    public const string PAR_ERROR_VALORCAL_E_2_2 = "e.2.2 - Superficie total de construcciones PRIVATIVAS.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO E.2.3 - VALOR TOTAL DE CONSTRUCCIONES PRIVATIVAS
    /// </summary>
    public const string PAR_ERROR_VALORCAL_E_2_3 = "e.2.3 - Valor total de construcciones PRIVATIVAS.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO E.2.5.N.13 - FACTOR DE EDAD
    /// </summary>
    public const string PAR_ERROR_VALORCAL_E_2_5_N_13 = "e.2.5.n.13 - Indice del costo remanente.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO E.2.5.N.14 - FACTOR RESULTANTE
    /// </summary>
    // JACM Se da de baja el campo 2021-02-04
    //public const string PAR_ERROR_VALORCAL_E_2_5_N_14 = "e.2.5.n.14 - Factor resultante.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO E.2.5.N.15 - VALOR DE LA FRACCIÓN N
    /// </summary>
    public const string PAR_ERROR_VALORCAL_E_2_5_N_15 = "e.2.5.n.15 - Valor de la fracción n.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO E.2.6 - SUPERFICIE TOTAL DE CONSTRUCCIONES COMUNES
    /// </summary>
    public const string PAR_ERROR_VALORCAL_E_2_6 = "e.2.6 - Superficie total de construcciones COMUNES.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO E.2.7 - VALOR TOTAL DE CONSTRUCCIONES COMUNES
    /// </summary>
    public const string PAR_ERROR_VALORCAL_E_2_7 = "e.2.7 - Valor total de construcciones COMUNES.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO E.2.4 - VALOR TOTAL DE LAS CONSTRUCCIONES PRIVATIVAS POR INDIVISO
    /// </summary>
    public const string PAR_ERROR_VALORCAL_E_2_4 = "e.2.4 - Valor total de las construcciones privativas por INDIVISO.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO E.2.8 - VALOR TOTAL DE LAS CONSTRUCCIONES POR INDIVISO
    /// </summary>
    public const string PAR_ERROR_VALORCAL_E_2_8 = "e.2.8 - Valor total de las construcciones por INDIVISO.";

    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO e.2.5.n.18 - Porcentaje de indiviso
    /// </summary>
    public const string PAR_ERROR_VALORCAL_E_2_5_n_18 = "e.2.5.n.18 -Porcentaje de indiviso.";

    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO f.9.2.n.10 - Porcentaje de indiviso
    /// </summary>
    public const string PAR_ERROR_VALORCAL_F_9_2_N_10 = "f.9.2.n.10 -Porcentaje de indiviso.";

    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO f.10.2.n.10 - Porcentaje de indiviso
    /// </summary>
    public const string PAR_ERROR_VALORCAL_F_10_2_N_10 = "f.10.2.n.10 -Porcentaje de indiviso.";

    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO f.11.2.n.10 - Porcentaje de indiviso
    /// </summary>
    public const string PAR_ERROR_VALORCAL_F_11_2_N_10 = "f.11.2.n.10 -Porcentaje de indiviso.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO E.2.1.N.17 - DEPRECIACIÓN POR EDAD
    /// </summary>
    public const string PAR_ERROR_VALORCAL_E_2_1_N_17 = "e.2.1.n.17 - Depreciación por edad.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO E.2.5.N.17 - DEPRECIACIÓN POR EDAD
    /// </summary>
    public const string PAR_ERROR_VALORCAL_E_2_5_N_17 = "e.2.5.n.17 - Depreciación por edad.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO NO INFERIOR A 0.6
    /// </summary>
    public const string PAR_ERROR_MINVALOR = "El valor no debe ser inferior a 0.6.";
    #endregion

    #region F: Elementos de la construcción
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO F.9.1.N.8 -  FACTOR DE EDAD INSTALACIÓN ESPECIAL PRIVATIVAS
    /// </summary>
    public const string PAR_ERROR_VALORCAL_F_9_1_N_8 = "f.9.1.n.8 -  Factor de edad instalación especial PRIVATIVAS";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO F.9.1.N.9 - IMPORTE INSTALACIÓN ESPECIAL PRIVATIVAS
    /// </summary>
    public const string PAR_ERROR_VALORCAL_F_9_1_N_9 = "f.9.1.n.9 - Importe instalación especial PRIVATIVAS.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO F.9.2.N.8 - FACTOR DE EDAD INSTALACIÓN ESPECIAL COMUNES
    /// </summary>
    public const string PAR_ERROR_VALORCAL_F_9_2_N_8 = "f.9.2.n.8 - Factor de edad instalación especial COMUNES.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO F.9.2.N.9 - IMPORTE INSTALACIÓN ESPECIAL COMUNES
    /// </summary>
    public const string PAR_ERROR_VALORCAL_F_9_2_N_9 = "f.9.2.n.9 - Importe instalación especial COMUNES.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO F.9.3 - IMPORTE TOTAL INSTALACIONES ESPECIALES PRIVATIVAS
    /// </summary>
    public const string PAR_ERROR_VALORCAL_F_9_3 = "f.9.3 - Importe total instalaciones especiales PRIVATIVAS.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO F.9.4 - IMPORTE TOTAL INSTALACIONES ESPECIALES COMUNES
    /// </summary>
    public const string PAR_ERROR_VALORCAL_F_9_4 = "f.9.4 - Importe total instalaciones especiales COMUNES.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO F.10.1.N.8 - FACTOR DE EDAD ELEMENTO ACCESORIO PRIVATIVAS
    /// </summary>
    public const string PAR_ERROR_VALORCAL_F_10_1_N_8 = "f.10.1.n.8 - Factor de edad elemento accesorio PRIVATIVAS.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO F.10.1.N.9 - IMPORTE ELEMENTO ACCESORIO PRIVATIVAS
    /// </summary>
    public const string PAR_ERROR_VALORCAL_F_10_1_N_9 = "f.10.1.n.9 - Importe elemento accesorio PRIVATIVAS.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO F.10.2.N.8 -  FACTOR DE EDAD ELEMENTO ACCESORIO COMUNES
    /// </summary>
    public const string PAR_ERROR_VALORCAL_F_10_2_N_8 = "f.10.2.n.8 -  Factor de edad elemento accesorio COMUNES.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO F.10.2.N.9 - IMPORTE ELEMENTO ACCESORIO COMUNES
    /// </summary>
    public const string PAR_ERROR_VALORCAL_F_10_2_N_9 = "f.10.2.n.9 - Importe elemento accesorio COMUNES.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO F.10.3 - IMPORTE TOTAL ELEMENTOS ACCESORIOS PRIVATIVAS
    /// </summary>
    public const string PAR_ERROR_VALORCAL_F_10_3 = "f.10.3 - Importe total elementos accesorios PRIVATIVAS.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO F.10.4 - IMPORTE TOTAL ELEMENTOS ACCESORIOS COMUNES
    /// </summary>
    public const string PAR_ERROR_VALORCAL_F_10_4 = "f.10.4 - Importe total elementos accesorios COMUNES.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO F.11.1.N.9 - IMPORTE OBRA COMPLEMENTARIA PRIVATIVAS
    /// </summary>
    public const string PAR_ERROR_VALORCAL_F_11_1_N_9 = "f.11.1.n.9 - Importe obra complementaria PRIVATIVAS.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO F.11.2.N.9 - IMPORTE OBRA COMPLEMENTARIA COMUNES
    /// </summary>
    public const string PAR_ERROR_VALORCAL_F_11_2_N_9 = "f.11.2.n.9 - Importe obra complementaria COMUNES.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO F.11.1.N.8 - FACTOR DE EDAD OBRA COMPLEMENTARIA PRIVATIVAS
    /// </summary>
    public const string PAR_ERROR_VALORCAL_F_11_1_N_8 = "f.11.1.n.8 - Factor de edad obra complementaria PRIVATIVAS.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO F.11.2.N.8 - FACTOR DE EDAD OBRA COMPLEMENTARIA COMUNES
    /// </summary>
    public const string PAR_ERROR_VALORCAL_F_11_2_N_8 = "f.11.2.n.8 - Factor de edad obra complementaria COMUNES.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO F.11.3 - IMPORTE TOTAL OBRAS COMPLEMENTARIAS PRIVATIVAS
    /// </summary>
    public const string PAR_ERROR_VALORCAL_F_11_3 = "f.11.3 - Importe total obras complementarias PRIVATIVAS.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO F.11.4 - IMPORTE TOTAL OBRAS COMPLEMENTARIAS COMUNES
    /// </summary>
    public const string PAR_ERROR_VALORCAL_F_11_4 = "f.11.4 - Importe total obras complementarias COMUNES.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO F.12 - Sumatoria Total Instalaciones Especiales Obras Complementarias Y Elementos Accesorios PRIVATIVAS
    /// </summary>
    public const string PAR_ERROR_VALORCAL_F_12 = "f.12 - Sumatoria Total Instalaciones Especiales Obras Complementarias Y Elementos Accesorios PRIVATIVAS.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO F.13 - Sumatoria Total Instalaciones Especiales Obras Complementarias Y Elementos Accesorios COMUNES
    /// </summary>
    public const string PAR_ERROR_VALORCAL_F_13 = "f.13 - Sumatoria Total Instalaciones Especiales Obras Complementarias Y Elementos Accesorios COMUNES.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO F.14 - IMPORTE INDIVISO INSTALACIONES ESPECIALES, OBRAS COMPLEMENTARIAS Y ELEMENTOS ACCESORIOS COMUNES
    /// </summary>
    public const string PAR_ERROR_VALORCAL_F_14 = "f.14 - Importe INDIVISO instalaciones especiales, obras complementarias y elementos accesorios COMUNES.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO F.15 - IMPORTE INDIVISO INSTALACIONES ESPECIALES, OBRAS COMPLEMENTARIAS Y ELEMENTOS ACCESORIOS PRIVATIVAS
    /// </summary>
    public const string PAR_ERROR_VALORCAL_F_15 = "f.15 - Importe INDIVISO instalaciones especiales, obras complementarias y elementos accesorios PRIVATIVAS.";
    #endregion

    #region I: Enfoque de costos (avalúo comercial)
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO I.6 - IMPORTE TOTAL DEL ENFOQUE DE COSTOS
    /// </summary>
    public const string PAR_ERROR_VALORCAL_I_6 = "i.6 - Importe total del enfoque de costos.";
    #endregion

    #region D: Terreno
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO D.13 - VALOR TOTAL DEL TERRENO PROPORCIONAL
    /// </summary>
    public const string PAR_ERROR_VALORCAL_D_13 = "d.13 - Valor total del terreno proporcional.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO D.11 - SUPERFICIE TOTAL DEL TERRENO
    /// </summary>
    public const string PAR_ERROR_VALORCAL_D_11 = "d.11 - Superficie total del terreno.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO D.12 - VALOR TOTAL DEL TERRENO
    /// </summary>
    public const string PAR_ERROR_VALORCAL_D_12 = "d.12 - Valor total del terreno.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO D.5.1.N.10 - FRE
    /// </summary>
    // JACM Se da de baja el campo 2021-02-04
    //public const string PAR_ERROR_VALORCAL_D_5_1_N_10 = "d.5.1.n.10 - Fre.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO D.5.2.N.10 - FRE
    /// </summary>
    // JACM Se da de baja el campo 2021-02-04
    //public const string PAR_ERROR_VALORCAL_D_5_2_N_10 = "d.5.2.n.10 - Fre.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO D.5.1.N.11 - VALOR DE LA FRACCIÓN N
    /// </summary>
    public const string PAR_ERROR_VALORCAL_D_5_1_N_11 = "d.5.1.n.11 - Valor de la fracción n.";

    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO D.5.2.N.11 - VALOR DE LA FRACCIÓN N
    /// </summary>
    public const string PAR_ERROR_VALORCAL_D_5_2_N_11 = "d.5.2.n.11 - Valor de la fracción n.";

    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO H.1.1.N.18.1 - VALOR DE LA FRACCIÓN N
    /// </summary>
    // JACM Se da de baja el campo 2021-02-04
    //public const string PAR_ERROR_VALORCAL_H_1_1_N_18_1 = "h.1.1.n.18.1 - Valor de la fracción n.";

    #endregion

    #region J: Enfoque de costos (avalúo catastral)

    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO J.4 - IMPORTE INSTALACIONES ESPECIALES
    /// </summary>
    public const string PAR_ERROR_VALORCAL_J_4 = "j.4 - Importe instalaciones especiales.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO J.5 - IMPORTE TOTAL VALOR CATASTRAL
    /// </summary>
    public const string PAR_ERROR_VALORCAL_J_5 = "j.5 - Importe total valor catastral.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO J.7 - IMPORTE TOTAL VALOR CATASTRAL OBRA EN PROCESO
    /// </summary>
    public const string PAR_ERROR_VALORCAL_J_7 = "j.7 - Importe total valor catastral obra en proceso.";
  
    #endregion

    #region K: Enfoque de ingresos (capitalización)
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO K.1 - RENTA BRUTA MENSUAL
    /// </summary>
    public const string PAR_ERROR_VALORCAL_K_1 = "k.1 - Renta bruta mensual.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO K.2.12 - DEDUCCIONES MENSUALES
    /// </summary>
    public const string PAR_ERROR_VALORCAL_K_2_12 = "k.2.12 - Deducciones mensuales.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO K.2.13 - % DEDUCCIONES MENSUALES
    /// </summary>
    public const string PAR_ERROR_VALORCAL_K_2_13 = "k.2.13 - % Deducciones mensuales.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO K.3 - % PRODUCTO LÍQUIDO ANUAL
    /// </summary>
    public const string PAR_ERROR_VALORCAL_K_3 = "k.3 - % Producto Líquido anual.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO K.5 - IMPORTE ENFOQUE DE INGRESOS
    /// </summary>
    public const string PAR_ERROR_VALORCAL_K_5 = "k.5 - Importe enfoque de ingresos.";
    #endregion

    #region P: Valor referido (avalúo comercial)
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO P.4 - FACTOR DE CONVERSIÓN
    /// </summary>
    public const string PAR_ERROR_VALORCAL_P_4 = "p.4 - Factor de conversión.";
    /// <summary>
    /// PARÁMETRO ERROR VALOR CALCULADO P.5 - VALOR REFERIDO
    /// </summary>
    public const string PAR_ERROR_VALORCAL_P_5 = "p.5 - Valor referido.";
    #endregion
    #region Q
    /// <summary>
    /// PARÁMETRO ERROR q.2 - Comparable Rentas
    /// </summary>
    public const string PAR_ERROR_VALOR_q_2 = "q.2 - Comparable rentas";

    /// <summary>
    /// PARÁMETRO ERROR q.3 - Comparable Ventas
    /// </summary>
    public const string PAR_ERROR_VALOR_q_3 = "q.3 - Comparable ventas.";
    #endregion
    #endregion

    #region PERFILES USUARIOS

    /// <summary>
    /// PERFIL PERITO
    /// </summary>
    public const string PERFIL_PERITO = "FunPerito";

    /// <summary>
    /// PERFIL SOCIEDAD
    /// </summary>
    public const string PERFIL_SOCIEDAD = "FunSociedad";
    #endregion 

    #region MENSAJES VALIDACION IMAGENES

    /// <summary>
    /// MENSAJE VALIDACIÓN IMAGEN
    /// </summary>
    public const string MSJ_LAIMAGEN = "La imagen ";
    #endregion 
   
    #region NOMBRES IMAGENES/FOTOS

    /// <summary>
    /// NOMBRE IMAGENES POSTFIJO
    /// </summary>
    public const string NOMBRE_IMAGENES_POSTFIJO = "_pos";

    /// <summary>
    /// NOMBRE FOTOS PREFIJO
    /// </summary>
    public const string NOMBRE_FOTOS_PREFIJO = "Foto_";

    /// <summary>
    /// IMAGEN CROQUIS MICROLOCALIZACION
    /// </summary>
    public const string IMG_CROQUISMICRO = "CroquisMicroLocalizacion";

    /// <summary>
    /// IMGEN CROQUIS MACROLOCALIZACION
    /// </summary>
    public const string IMG_CROQUISMACRO = "CroquisMacroLocalizacion";
    #endregion 

    #region CUENTA CATASTRAL

    /// <summary>
    /// REGION DE LA CUENTA CATASTRAL 
    /// </summary>
    public const string REGION = " REGION ";
    /// <summary>
    ///MANZANA  DE LA  CUENTA CATASTRAL 
    /// </summary>
    public const string MANZANA = " MANZANA ";
    /// <summary>
    /// LOTE DE LA CUENTA CATASTRAL
    /// </summary>
    public const string LOTE = " LOTE ";
    /// <summary>
    ///UNIDADPRIVATIVA DE LA CUENTA CATASTRAL 
    /// </summary>
    public const string UNIDADPRIV = " UNIDADPRIVATIVA ";
    /// <summary>
    /// SIMBOLO UNION ENTRE CAMPOS DE LA CUENTA CATASTRAL  (-)
    /// </summary>
    public const string CUENTACAT_SIMBOLO_UNION_CAMPOS = "-";
    #endregion 

    #region OPERADORES Y SIMBOLOS

    /// <summary>
    /// OPERADOR AND
    /// </summary>
    public const string OPERADOR_AND = " AND ";

    /// <summary>
    /// SIMBOLO IGUAL A
    /// </summary>
    public const string SIMBOLO_IGUAL_A = " = ";

    /// <summary>
    /// SIMBOLO GUION
    /// </summary>
    public const string SIMBOLO_GUION = "-";
    #endregion 

    #region TIPO CONSTRUCCIÓN

    /// <summary>
    /// CÓDIGO TIPO CONSTRUCCIÓN PRIVATIVA
    /// </summary>
    public const string CODTIPOCONSTRUCCION_PRIVATIVA = "P";

    /// <summary>
    /// CÓDIGO TIPO CONSTRUCCIÓN COMÚN
    /// </summary>
    public const string CODTIPOCONSTRUCCION_COMUN = "C";
    #endregion 

    #region TIPO ELEMENTOS EXTRA

    /// <summary>
    /// CÓDIGO TIPO ELEMENTOS EXTRA PRIVATIVA
    /// </summary>
    public const string CODTIPO_ELEMEXTRA_PRIVATIVA = "P";

    /// <summary>
    /// CÓDIGO TIPO ELEMENTOS EXTRA COMUN
    /// </summary>
    public const string CODTIPO_ELEMEXTRA_COMUN = "C";

    /// <summary>
    /// CÓDIGO TIPO ELEMENTOS EXTRA OBRA COMPLEMENTARIA
    /// </summary>
    public const string CODTIPO_ELEMEXTRA_OBRACOMPLEMENTARIA = "O";

    /// <summary>
    /// CÓDIGO TIPO ELEMENTOS EXTRA INSTALACIÓN ESPECIAL
    /// </summary>
    public const string CODTIPO_ELEMEXTRA_INSTALACIONESPECIAL = "I";

    /// <summary>
    /// CÓDIGO TIPO ELEMENTOS EXTRA ELEMENTOS ACCESORIOS
    /// </summary>
    public const string CODTIPO_ELEMEXTRA_ELEMENTOSACCESORIOS = "E";
    #endregion 

    #region FILE EXTENSIONS
    /// <summary>
    /// EXTENSION FICHERO JPG  
    /// </summary>
    public const string JPG_FILE_EXTENSION = ".jpg";
    /// <summary>
    /// EXTENSION FICHERO XML
    /// </summary>
    public const string XML_FILE_EXTENSION = ".xml";
    #endregion 

    #region TIPOS PERSONAS

    /// <summary>
    /// CÓDIGO PERSONA PROPIETARIO
    /// </summary>
    public const string COD_PERSONA_PROPIETARIO = "P";

    /// <summary>
    /// CÓDIGO PERSONA SOLICITANTE
    /// </summary>
    public const string COD_PERSONA_SOLICITANTE = "S";

    /// <summary>
    /// CÓDIGO PERSONA FíSICA
    /// </summary>
    public const string COD_PERSONA_FISICA = "F";

    /// <summary>
    /// CÓDIGO PERSONA MORAL
    /// </summary>
    public const string COD_PERSONA_MORAL = "M";

    /// <summary>
    /// PERSONA SOLICITANTE
    /// </summary>
    public const string PERSONA_SOLICITANTE = "S";

    /// <summary>
    /// PERSONA PROPIETARIO
    /// </summary>
    public const string PERSONA_PROPIETARIO = "P";
    #endregion 

    #region OTROS

    /// <summary>
    /// CONFIRMACIÓN ENVIO MENSAJE
    /// </summary>
    public const string CONFIRMACION_ENVIO_MSJ = "Mensaje enviado satisfactoriamente";

    /// <summary>
    /// CULTURE INFO, PARA INDICAR EL IDIOMA DE LA APLICACIÓN
    /// </summary>
    public const string CULTURE_INFO = "es-MX";

    /// <summary>
    /// PARÁMETRO ERROR SUMA
    /// </summary>
    public const int PAR_ERRORSUMA = -9999;

    /// <summary>
    /// ESPACIO BLANCO
    /// </summary>
    public const string ESPACIO_BLANCO = " ";

    /// <summary>
    /// XML IDENTIFICADOR ELEMENTOS
    /// </summary>
    public const string XML_IDENTIFICADOR_ELEMENTOS = "id";

    /// <summary>
    /// DATASET DELEGACIÓN CON CLAVE
    /// </summary>
    public const string DSEDELEG_COL_CLAVE= "CLAVE";

    /// <summary>
    /// FACTOR EDAD
    /// </summary>
    public const string FACTOREDAD = "FactorEdad";
    #endregion

    #region PREFIJOS NOMBRES

    /// <summary>
    /// PREFIJO DEL NÚMERO UNICO AVALÚO
    /// </summary>
    public const string NUM_UNICOAV_PREFIJO = "A-";

    /// <summary>
    /// PREFIJO DEL NOMBRE DE LOS AVALÚOS
    /// </summary>
    public const string NOMBRE_FICHEROAV_PREFIJO = "Avaluo-";

    /// <summary>
    ///  PREFIJO PARA LA DESCRIPCIÓN DE LOS FICHEROS DE AVALÚO
    /// </summary>
    public const string DESC_FICHEROAV_PREFIJO = "Avaluo_";
    #endregion 
}
