# Information
Implementación de la infraestructura para llamar al repositorio.


	WSMovimientos.Repositorio					
						|
						Configuraciones
						Proxy (proxy generado vstudio)
						Service
							|
							ServicesName WsMovimientos
								|
								Soap


# Documentation
ServicesName : nombre del servicio Externo. Ejemplo: Pagueya, Sipecom, RegistroCivil, LinkSolutions,
Proxy: Clases generadas por wizard de referencia de servicios (Visual Studio)

Configuraciones: Api - Centralizada, configuraciones predeterminadas de centralizada (No se requiere de modificación)

Soap: Definimos métodos de consulta de servicio externos generadas por el proxy.


# Build 
PersonaRepositorio.cs: Lógica de consulta de servicio. 


# Nuget Packages
1.  MethodBoundaryAspect.Fody(2.0.145)
