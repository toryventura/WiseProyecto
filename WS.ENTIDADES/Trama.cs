namespace WS.ENTIDADES
{
public class Trama { 
     public long ID { get; set;}
     public string IMEI { get; set;}
     public int EstadoGPS { get; set;}
     public float Velocidad { get; set;}
     public double Asimut { get; set;}
     public float Longitud { get; set;}
     public float Latitud { get; set;}
     public double Altitud { get; set;}
     public string TipoMensaje { get; set;}
     public string TipoRespuesta { get; set;}
     public int EstadoMotor { get; set;}
     public System.DateTime FechaGPS { get; set;}
     public System.DateTime FechaEnvio { get; set;}
     public System.DateTime FechaReg { get; set;}
     public string IDButton { get; set;}
     public bool IDAutorizado { get; set;}
     public float Kilometraje { get; set;}
     public double RPMMotor { get; set;}
     public double ConsComb { get; set;}
     public double NivelEntradaComb { get; set;}
     public float Temperatura { get; set;}
     public float VoltajeBateria { get; set;}
     public bool EstadoPuerta { get; set;}
     public int Procesado { get; set;}
     public string  EstadoMotor1 { get; set;}
     public int DifIgnicion { get; set;}
}
}