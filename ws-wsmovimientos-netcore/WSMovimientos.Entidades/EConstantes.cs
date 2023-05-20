namespace WSMovimientos.Entidades
{
    /// <summary>
    /// 
    /// </summary>
    public class EConstantes
    {
        #region TAGS DE CATALGOS
        /// <summary>
        /// 
        /// </summary>
        public const string ApiVersion = "1";

        /// <summary>
        /// 
        /// </summary>
        public const string TagBackendOpenShift = "OPENSHIFT";

        /// <summary>
        /// 
        /// </summary>
        public const string HealthChecks = "HealthChecks";

        #endregion TAGS DE CATALGOS


        #region TAG CODIGO ERROR

        /// <summary>
        /// 
        /// </summary>
        public const string ValExpCodigo = "9999";

        /// <summary>
        ///     Código que representa el header nulo.
        /// </summary>
        public const string HeaderInNullCode = "0001";

        /// <summary>
        ///     Descripción que representa el header vacio.
        /// </summary>
        public const string HeaderInNullDescription = "LA CABECERA ES NULA O NO EXISTE";

        /// <summary>
        /// Device Hash takes it from claims
        /// </summary>
        public const string DispositivoNullCode = "0002";

        /// <summary>
        /// Device Hash takes it from claims
        /// </summary>
        public const string DispositivoNullDescription = "DISPOSITIVO NULO O VACÍO";

        /// <summary>
        /// Company code 0010 by default
        /// </summary>
        public const string EmpresaNullCode = "0003";

        /// <summary>
        /// Company code 0010 by default
        /// </summary>
        public const string EmpresaNullDescription = "EMPRESA NULA O VACÍA";

        /// <summary>
        /// Code channel defined by business architecture WSO2 will send in the Claim
        /// </summary>
        public const string CanalNullCode = "0004";

        /// <summary>
        /// Code channel defined by business architecture WSO2 will send in the Claim
        /// </summary>
        public const string CanalNullDescription = "CANAL NULO O VACÍO";

        /// <summary>
        /// Medium code defined by business architecture WSO2 will send in the Claim
        /// </summary>
        public const string MedioNullCode = "0005";

        /// <summary>
        /// Medium code defined by business architecture WSO2 will send in the Claim
        /// </summary>
        public const string MedioNullDescription = "MEDIO NULO O VACÍO";

        /// <summary>
        /// Application code defined by business architecture WSO2 will send in the Claim
        /// </summary>
        public const string AplicacionNullCode = "0006";

        /// <summary>
        /// Application code defined by business architecture WSO2 will send in the Claim
        /// </summary>
        public const string AplicacionNullDescription = "APLICACIÓN NULA O VACÍA";

        /// <summary>
        /// Code of the agency where the user is registered
        /// </summary>
        public const string AgenciaNullCode = "0007";

        /// <summary>
        /// Code of the agency where the user is registered
        /// </summary>
        public const string AgenciaNullDescription = "AGENCIA NULA O VACÍA";

        /// <summary>
        /// Application code defined by enterprise architecture each application must place this value is the transactional log transaction type.
        /// </summary>
        public const string TipoTransaccionNullCode = "0008";

        /// <summary>
        /// Application code defined by enterprise architecture each application must place this value is the transactional log transaction type.
        /// </summary>
        public const string TipoTransaccionNullDescription = "TIPO TRANSACCION NULA O VACÍA";

        /// <summary>
        /// Field describes the geolocation
        /// </summary>
        public const string GeolocalizacionNullCode = "0009";

        /// <summary>
        /// Field describes the geolocation
        /// </summary>
        public const string GeolocalizacionNullDescription = "GEOLOCALIZACION NULA O VACÍA";

        /// <summary>
        /// generic user of the default transaction
        /// </summary>
        public const string UsuarioNullCode = "0010";

        /// <summary>
        /// generic user of the default transaction
        /// </summary>
        public const string UsuarioNullDescription = "USUARIO NULO O VACÍO";

        /// <summary>
        /// HASH of the transaction component is generated with the uniqueness component
        /// </summary>
        public const string UnicidadNullCode = "0011";

        /// <summary>
        /// HASH of the transaction component is generated with the uniqueness component
        /// </summary>
        public const string UnicidadNullDescription = "UNICIDAD NULA O VACÍA";

        /// <summary>
        /// Unique identifier of the transaction
        /// </summary>
        public const string GuidNullCode = "0012";

        /// <summary>
        /// Unique identifier of the transaction
        /// </summary>
        public const string GuidNullDescription = "GUID NULO O VACÍO";

        /// <summary>
        /// date and time of transaction yyyyMMddhhmmssSSSS
        /// </summary>
        public const string FechaHoraNullCode = "0013";

        /// <summary>
        /// date and time of transaction yyyyMMddhhmmssSSSS
        /// </summary>
        public const string FechaHoraNullDescription = "FECHA-HORA NULA O VACÍA";

        /// <summary>
        /// Language WSO2 will send on the Claim
        /// </summary>
        public const string IdiomaNullCode = "0014";

        /// <summary>
        /// Language WSO2 will send on the Claim
        /// </summary>
        public const string IdiomaNullDescription = "IDIOMA NULO O VACÍO";

        /// <summary>
        /// Session generated in WSO2 is sent in the claim
        /// </summary>
        public const string SesionNullCode = "0015";

        /// <summary>
        /// Session generated in WSO2 is sent in the claim
        /// </summary>
        public const string SesionNullDescription = "SESION NULA O VACÍA";

        /// <summary>
        /// Client IP address
        /// </summary>
        public const string IpNullCode = "0016";

        /// <summary>
        /// Client IP address
        /// </summary>
        public const string IpNullDescription = "IP NULA O VACÍA";

        /// <summary>
        ///     Código que representa el body nulo.
        /// </summary>
        public const string BodyNullCode = "0017";

        /// <summary>
        ///     Descripción que representa el body vacio.
        /// </summary>
        public const string BodyNullDescription = "EL CUERPO DEL SERVICIO ES NULO O NO EXISTE";

        /// <summary>
        /// 
        /// </summary> 
        public const string ErrorCode1 = "1";

        /// <summary>
        /// 
        /// </summary>
        public const string ErrorCode2 = "2";

        /// <summary>
        /// 
        /// </summary>
        public const string ErrorCode3 = "3";

        /// <summary>
        /// 
        /// </summary>
        public const string ErrorCode4 = "4";

        /// <summary>
        /// 
        /// </summary>
        public const string ErrorCode4Descripcion = "No posee registros registrados";


        /// <summary>
        /// 
        /// </summary>
        public const string ErrorGenericoDescripcion = "No es posible procesar su transaccion";

        /// <summary>
        /// OK
        /// </summary>
        public const string OKGenericoDescripcion = "Transaccion exitosa";

        /// <summary>
        /// No puede ser nulo o vacio 
        /// </summary>
        public const string ErrorCode1DescripcionNuloVacio = "El campo '{0}' nulo o vacío";

        /// <summary>
        /// Fuera de rango
        /// </summary>
        public const string ErrorCode2DescripcionFueraRango = "La longitud del campo '{0}' fuera del rango permitido";

        /// <summary>
        /// solo se permite numeros
        /// </summary>
        public const string identificacionCode9Expresion = @"^[\d]+$";

        /// <summary>
        /// Solo se permite  numeros
        /// </summary>
        public const string ErrorCode9SoloNumero = "El campo '{0}' solo permite numeros";


        /// <summary>
        /// Solo Permite DEP - RET
        /// </summary>
        public const string ErrorCodeTipoDescripcion = "'{0}' No Valido";

        /// <summary>
        /// Error Tipo
        /// </summary>
        public const string ErrorCodeTipo = "10";

        /// <summary>
        /// Error Saldo no disponible
        /// </summary>
        public const string ErrorSaldoCodigo = "11";

        /// <summary>
        /// 
        /// </summary>
        public const string ErrorSaldo = "Saldo No Disponible";

        /// <summary>
        /// Error Saldo no disponible
        /// </summary>
        public const string ErrorCuentaCodigo = "12";

        /// <summary>
        /// 
        /// </summary>
        public const string ErrorCuenta = "Cuenta no existe";

        #endregion

        #region CONECCION BASE
        /// <summary>
        /// 
        /// </summary>
        public const string ConeccionBdd = "Server=DANILO-LAPTOP;Database=BDD_MOVIMIENTOS;Trusted_Connection=True;";

        #endregion CONECCION BASE

        #region BACKEND
        /// <summary>
        /// 
        /// </summary>
        public const string movimientos = "movimientos";
        /// <summary>
        /// 
        /// </summary>
        public const string consultar = "consultar";
        /// <summary>
        /// 
        /// </summary>
        public const string crear = "crear";
        /// <summary>
        /// 
        /// </summary>
        public const string actualizar = "actualizar";
        /// <summary>
        /// 
        /// </summary>
        public const string eliminar = "eliminar";

        /// <summary>
        /// 
        /// </summary>
        public const string consultaMovimientos = "consultar.movimientos";

        /// <summary>
        /// 
        /// </summary>
        public const bool EstadoActivo = true;

        /// <summary>
        /// 
        /// </summary>
        public const string ErrorCrearCode = "5";

        /// <summary>
        /// 
        /// </summary>
        public const string ErrorCrearDescripcion = "No fue posible crear el registro";

        /// <summary>
        /// 
        /// </summary>
        public const string ErrorActualizarCode = "6";

        /// <summary>
        /// 
        /// </summary>
        public const string ErrorActualizarDescripcion = "No fue posible actualizar el registro";

        /// <summary>
        /// 
        /// </summary>
        public const string ErrorEliminarCode = "7";

        /// <summary>
        /// 
        /// </summary>
        public const string ErrorEliminarDescripcion = "No fue posible eliminar el registro";

        /// <summary>
        /// 
        /// </summary>
        public const string ErrorCode9 = "9";


        #endregion
    }
}