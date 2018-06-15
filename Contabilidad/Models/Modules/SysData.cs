using System;

public static class SysData
{
    public static string NumberToField(object Value)
    {
        long Out;

        if (long.TryParse(Convert.ToString(Value), out Out))
        {
            return Convert.ToString(Out);
        }
        else
        {
            return Convert.ToString(0);
        }
    }

    public static string DecimalToField(object Value)
    {
        decimal Out;

        if (decimal.TryParse(Convert.ToString(Value), out Out))
        {
            return Convert.ToString(Math.Round(Out, 2)).Replace(",", ".");
        }
        else
        {
            return Convert.ToString(0.0);
        }

    }

    public static string StringToField(object Value)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Value).Trim()))
        {
            return "'" + Convert.ToString(Value).Trim() + "'";
        }
        else
        {
            return "''";
        }
    }

    public static string DateToField(object Value)
    {
        DateTime Out;

        if (DateTime.TryParse(Convert.ToString(Value), out Out))
        {
            return "'" + String.Format(Out.ToString(), "MM/dd/yyyy") + "'";
        }
        else
        {
            return "Null";
        }
    }





    public static byte ToByte(object Value)
    {
        byte Out;

        if (byte.TryParse(Convert.ToString(Value), out Out))
        {
            return Out;
        }
        else
        {
            return (byte)0;
        }
    }

    public static short ToShort(object Value)
    {
        short Out;

        if (short.TryParse(Convert.ToString(Value), out Out))
        {
            return Out;
        }
        else
        {
            return (short)0;
        }
    }

    public static int ToInteger(object Value)
    {
        int Out;

        if (int.TryParse(Convert.ToString(Value), out Out))
        {
            return Out;
        }
        else
        {
            return (int)0;
        }
    }

    public static long ToLong(object Value)
    {
        long Out;

        if (long.TryParse(Convert.ToString(Value), out Out))
        {
            return Out;
        }
        else
        {
            return (long)0;
        }
    }

    public static double ToDouble(object Value)
    {
        double Out;

        if (double.TryParse(Convert.ToString(Value), out Out))
        {
            return Out;
        }
        else
        {
            return (double)0;
        }
    }

    public static decimal ToDecimal(object Value)
    {
        decimal Out;

        if (decimal.TryParse(Convert.ToString(Value), out Out))
        {
            return Math.Round(Out, 2);
        }
        else
        {
            return (decimal)0.0;
        }
    }

    public static decimal ToDecimalUno(object Value)
    {
        decimal Out;

        if (decimal.TryParse(Convert.ToString(Value), out Out))
        {
            return Math.Round(Out, 1);
        }
        else
        {
            return (decimal)0.0;
        }
    }

    public static decimal ToDecimalDos(object Value)
    {
        decimal Out;

        if (decimal.TryParse(Convert.ToString(Value), out Out))
        {
            return Math.Round(Out, 2);
        }
        else
        {
            return (decimal)0.0;
        }
    }

    public static decimal ToDecimalCinco(object Value)
    {
        decimal Out;

        if (decimal.TryParse(Convert.ToString(Value), out Out))
        {
            return Math.Round(Out, 5);
        }
        else
        {
            return (decimal)0.0;
        }
    }

    public static decimal ToDecimalQuince(object Value)
    {
        decimal Out;

        if (decimal.TryParse(Convert.ToString(Value), out Out))
        {
            return Math.Round(Out, 15);
        }
        else
        {
            return (decimal)0.0;
        }
    }

    public static bool ToBoolean(object Value)
    {
        bool Out;

        if (Boolean.TryParse(Convert.ToString(Value), out Out))
        {
            return Out;
        }
        if (Convert.ToString(Value) == "0")
        {
            return false;
        }
        if (Convert.ToString(Value) == "1")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static string ToStr(object Value)
    {
        if (Convert.ToString(Value) != string.Empty)
        {
            return Convert.ToString(Value).Trim();
        }
        else
        {
            return string.Empty;
        }
    }

    public static string ToDecStr(object Value, int NroDecimal)
    {
        decimal Out;
        string strValue = "";

        if (decimal.TryParse(Convert.ToString(Value), out Out))
        {
            switch (NroDecimal)
            {
                case 1:
                    strValue = String.Format("##,##0.0", Out);
                    break;
                case 2:
                    strValue = String.Format("##,##0.00", Out);
                    break;
                case 3:
                    strValue = String.Format("##,##0.000", Out);
                    break;
                case 4:
                    strValue = String.Format("##,##0.0000", Out);
                    break;
                case 5:
                    strValue = String.Format("##,##0.00000", Out);
                    break;
            }
        }
        else
        {
            strValue = "0.0";
        }

        return strValue;
    }

    public static DateTime ToDate(object Value)
    {
        DateTime Out;

        if (DateTime.TryParse(Convert.ToString(Value), out Out))
        {
            return Out.Date;
        }
        else
        {
            return DateTime.Today;
        }
    }

    public static DateTime ToDateTime(object Value)
    {
        DateTime Out;

        if (DateTime.TryParse(Convert.ToString(Value), out Out))
        {
            return Out;
        }
        else
        {
            return DateTime.Now;
        }
    }

    public static string ToDateDDMMYYYY(object Value)
    {
        DateTime Out;

        if (DateTime.TryParse(Convert.ToString(Value), out Out))
        {
            return String.Format(Out.ToString(), "dd/MM/yyyy");
        }
        else
        {
            return DateTime.Today.ToString();
        }
    }

    public static string ToDateMMDDYYYY(object Value)
    {
        DateTime Out;

        if (DateTime.TryParse(Convert.ToString(Value), out Out))
        {
            return String.Format(Out.ToString(), "MM/dd/yyyy");
        }
        else
        {
            return DateTime.Today.ToString();
        }
    }

    public static string ToDateYYYYMMDD(object Value)
    {
        DateTime Out;

        if (DateTime.TryParse(Convert.ToString(Value), out Out))
        {
            return String.Format(Out.ToString(), "yyyy-MM-dd");
        }
        else
        {
            return DateTime.Today.ToString();
        }
    }

    public static string ConvertDMY(string strDate)
    {
        string returnValue = "";
        string strFechaNormal = "";

        strFechaNormal = strDate.Substring(3, 2);
        strFechaNormal += "/";
        strFechaNormal += strDate.Substring(0, 2);
        strFechaNormal += "/";
        strFechaNormal += strDate.Substring(6, 4);

        returnValue = strFechaNormal;

        return returnValue;
    }

    public static string DecimalMask(int NroDecimal)
    {
        string strMasK = "";

        switch (NroDecimal)
        {
            case 1:
                strMasK = "##,##0.0";
                break;
            case 2:
                strMasK = "##,##0.00";
                break;
            case 3:
                strMasK = "##,##0.000";
                break;
            case 4:
                strMasK = "##,##0.0000";
                break;
            case 5:
                strMasK = "##,##0.00000";
                break;
        }

        return strMasK;
    }

    public static decimal toDivByCero(object Value)
    {
        decimal Out;

        if (decimal.TryParse(Convert.ToString(Value), out Out))
        {
            if (ToDecimal(Value) != 0)
            {
                return Math.Round(Out, 2);
            }
            else
            {
                return 1;
            }
        }
        else
        {
            return 1;
        }
    }

}