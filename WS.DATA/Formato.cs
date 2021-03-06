﻿using System.Globalization;

namespace WS.DATA
{
	public static class Formato
	{

		public static CultureInfo Formato1
		{
			get
			{
				CultureInfo cultureInfo = new CultureInfo("es-BO");
				//DateTimeFormatting
				cultureInfo.DateTimeFormat.DateSeparator = "/";
				cultureInfo.DateTimeFormat.FullDateTimePattern = "dd/MM/yyyy HH:mm:ss";
				cultureInfo.DateTimeFormat.TimeSeparator = ":";
				cultureInfo.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
				cultureInfo.DateTimeFormat.ShortTimePattern = "HH:mm";
				//NumberFormatting
				//cultureInfo.NumberFormat.NumberDecimalDigits = 2;
				cultureInfo.NumberFormat.NumberGroupSeparator = ".";
				cultureInfo.NumberFormat.NumberDecimalSeparator = ",";
				cultureInfo.NumberFormat.CurrencyDecimalSeparator = ",";
				cultureInfo.NumberFormat.CurrencyGroupSeparator = ".";
				cultureInfo.NumberFormat.PercentDecimalSeparator = ",";
				cultureInfo.NumberFormat.PercentGroupSeparator = ".";
				return cultureInfo;
			}
		}
	}
}
