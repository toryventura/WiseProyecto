using System;
namespace WS.ENTIDADES
{
public class GPSTrama { 
     public long ID { get; set;}
     public string IMEI { get; set;}
     public long IDInicio { get; set;}
     public long IDFin { get; set;}
     public System.DateTime FechaReg { get; set;}
     public bool Procesado { get; set;}
     public int PID { get; set;}
     public int Intervalo { get; set;}
}
}