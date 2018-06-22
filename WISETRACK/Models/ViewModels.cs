using System;
using System.Collections.Generic;

namespace WISETRACK.Models
{
	public class AlarmaDetalle
	{
		public string razonSocial { get; set; }
		public int CodAlarma { get; set; }
		public string NombreAlarma { get; set; }
		public string TipoAlarma { get; set; }
		public string Estado { get; set; }
		public string UsuaReg { get; set; }
		public DateTime FechaReg { get; set; }
	}

	[Serializable]
	public class AlarmaRptDet
	{
		public int Codigo { get; set; }
		public string Nombre { get; set; }
		public string Tipo { get; set; }
		public string Categoria { get; set; }
		public string Vehiculo { get; set; }
		public DateTime FechaInicio { get; set; }
		public DateTime FechaFin { get; set; }

		public string ValorPerm { get; set; }
		public string ValorReg { get; set; }
		public string Ubicacion { get; set; }
		public double Longitud { get; set; }
		public double Latitud { get; set; }
	}
	public class IDButtonDetalle
	{
		public int ID { get; set; }
		public string Keys { get; set; }
		public string Alias { get; set; }
		public string Rsocial { get; set; }
		public DateTime? FechaReg { get; set; }
		public string Estado { get; set; }
	}

	public class keyDetalles
	{
		public string IMEI { get; set; }
		public string modelo { get; set; }
		public List<IDButtonDetalle> IdButtuns { get; set; }
	}
	public class EmpresaModel
	{

		public string NIT { get; set; }
		public string RazonSocial { get; set; }
		public string email { get; set; }
		public DateTime FechaReg { get; set; }

	}

	public class ConductorAsiganadoDet
	{
		public int ID { get; set; }
		public string nombreConductor { set; get; }
		public string nroplaca { get; set; }
		public string keys { get; set; }
		public DateTime fecha { get; set; }
	}
	public class DestinatarioDetalle
	{
		public string CI { get; set; }
		public string NombreCompleto { get; set; }
		public string Email { get; set; }
		public string Telefono { get; set; }
	}

	public class GeocercaDetalle
	{
		public int CodigoGEO { get; set; }
		public string Descripcion { get; set; }
		public string Tipo { get; set; }
	}

	public class GeocercaCboDet
	{

		public int CodigoGEO { get; set; }
		public string Descripcion { get; set; }
	}


	public class TipoGeocercaDetalle
	{
		public string RazonSocial { get; set; }
		public int CodTipoGEO { get; set; }
		public string Descripcion { get; set; }
	}

	public class VehConductorDetalle
	{
		public string NroPlaca { get; set; }
		public string Tipo { get; set; }
		public string Marca { get; set; }
		public string Conductor { get; set; }
	}

	public class PersonaDetalle
	{
		public string CI { get; set; }
		public string Nombre { get; set; }
		public string ApellidoP { get; set; }
		public string ApellidoM { get; set; }
		public string Direccion { get; set; }
		public string Telefono { get; set; }
		public string Email { get; set; }
		public System.DateTime FechaReg { get; set; }
		public string UserName { get; set; }
		public string IdEmpresa { get; set; }
		public bool Estado { get; set; }
	}

	public class VelocidadDetalle
	{
		public string Placa { get; set; }
		public string Conductor { get; set; }
		public System.DateTime Fecha { get; set; }
		public double Velocidad { get; set; }
		public double Longitud { get; set; }
		public double Latitud { get; set; }
	}

	public class UsuarioDetalle
	{
		public string Id { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string UserRole { get; set; }
		public string Persona { get; set; }
	}

	public class PersonaCboDet
	{
		public string CI { get; set; }
		public string NombreCompleto { get; set; }
	}
	//public class VehiculoCboDet
	//{
	//    public string nroplaca { get; set; }
	//    public string NombreCompleto { get; set; }
	//}
	public class VehiculoDetalle
	{
		public string RazonSocial { get; set; }
		public string NroPlaca { get; set; }
		public string NroChasis { get; set; }
		public string Modelo { get; set; }
		public int Año { get; set; }
		public string Estado { get; set; }
		public string Conductor { get; set; }
		public bool EstadoVehiculo { get; set; }
		public bool EstadoEmpresaVehiculo { get; set; }
		public string tipo { get; set; }
		public string marca { get; set; }
	}

	public class VehiculoCboDet
	{
		public string Id { get; set; }
		public string NroPlaca { get; set; }
	}

	public class DetEncendidoApagado
	{
		public string Nroplaca { get; set; }
		public string FHoraIgnicion { set; get; }
		public string FHMEncendido { set; get; }
		public string FHApagado { set; get; }
		public int DMinIgnicion { set; get; }
		public int DMMEncendido { set; get; }
	}

	public class TipoAlarmaCboDet
	{
		public int CodTipoAlarma { get; set; }
		public string Descripcion { get; set; }
	}

	public class CategAlarmaCboDet
	{
		public int CodCategoria { get; set; }
		public string Descripcion { get; set; }
	}

	[Serializable]
	public class EstadoPuertaRptDet
	{
		public Int64 IDReporte { get; set; }
		public string Vehiculo { get; set; }
		public string Conductor { get; set; }
		public string Geocerca { get; set; }
		public string FechaApertura { get; set; }
		public string UbicacionA { get; set; }
		public string LongitudA { get; set; }
		public string LatitudA { get; set; }
		public string FechaCierre { get; set; }
		public string Tiempo { get; set; }
	}

	[Serializable]
	public class EntradaSalidaRptDet
	{
		public Int64 IDReporte { get; set; }
		public string Vehiculo { get; set; }
		public string Conductor { get; set; }
		public string Geocerca { get; set; }
		public string FechaEntrada { get; set; }
		public string UbicacionE { get; set; }
		public string LongitudE { get; set; }
		public string LatitudE { get; set; }
		public string FechaSalida { get; set; }
		public string Tiempo { get; set; }
	}

	[Serializable]
	public class DetencionRptDet
	{
		public Int64 IDReporte { get; set; }
		public string Vehiculo { get; set; }
		public string Conductor { get; set; }
		public string Geocerca { get; set; }
		public string FechaInicio { get; set; }
		public string Ubicacion { get; set; }
		public string Longitud { get; set; }
		public string Latitud { get; set; }
		public string FechaFin { get; set; }
		public int Tiempo { get; set; }
	}

	[Serializable]
	public class VelocidadRptDet
	{
		public Int64 IDReporte { get; set; }
		public string Vehiculo { get; set; }
		public string Conductor { get; set; }
		public string Geocerca { get; set; }
		public string Fecha { get; set; }
		public string Ubicacion { get; set; }
		public string Longitud { get; set; }
		public string Latitud { get; set; }
		public double Velocidad { get; set; }
	}

	[Serializable]
	public class KilometrajeRptDet
	{
		public Int64 IDReporte { get; set; }
		public string Vehiculo { get; set; }
		public string Conductor { get; set; }
		public string Geocerca { get; set; }
		public string FechaInicio { get; set; }
		public string Ubicacion { get; set; }
		public string Longitud { get; set; }
		public string Latitud { get; set; }
		public string FechaFin { get; set; }
		public double Kilometraje { get; set; }
	}

	[Serializable]
	public class KilometrajeRptDetOptimizado
	{

		public string Vehiculo { get; set; }
		public string FechaInicio { get; set; }
		public string FechaFin { get; set; }
		public string Kilometraje { get; set; }
	}


	public class Geocerca_Delete
	{
		public int CodigoGEO { get; set; }
		public string Descripcion { get; set; }
		public string Zona { get; set; }
		public string NIT { get; set; }
	}
}