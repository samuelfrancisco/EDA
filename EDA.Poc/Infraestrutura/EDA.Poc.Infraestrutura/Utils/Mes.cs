using System;

namespace EDA.Poc.Infraestrutura.Utils
{
    public class Mes
    {
        public int Month { get; private set; }
        public int Year { get; private set; }

        private Mes()
        {
            
        }

        public Mes(DateTime dateTime)
        {
            Month = dateTime.Month;
            Year = dateTime.Year;
        }

        public Mes(int month, int year)
        {
            if (month < 1 || month > 12) throw new ArgumentOutOfRangeException("month");
            if (year < 0) throw new ArgumentOutOfRangeException("year");

            Month = month;
            Year = year;
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}", Month, Year);
        }

        public override int GetHashCode()
        {
            return (Month - Year).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            var mes = obj as Mes;

            if (mes == null) return false;

            return Month.Equals(mes.Month) && Year.Equals(mes.Year);
        }

        public static bool operator ==(Mes mes1, Mes mes2)
        {
            if (Equals(mes1, null) && Equals(mes2, null)) return true;

            if (Equals(mes1, null) || Equals(mes2, null)) return false;

            return mes1.Equals(mes2);
        }

        public static bool operator !=(Mes mes1, Mes mes2)
        {
            return !(mes1 == mes2);
        }

        public static implicit operator Mes(DateTime dateTime)
        {
            return new Mes(dateTime);
        }

        public static explicit operator DateTime(Mes mes)
        {
            return new DateTime(mes.Year, mes.Month, 1);
        }
    }
}