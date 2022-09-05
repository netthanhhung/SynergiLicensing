using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Synergi.Licensing.Common
{
    public static partial class Utilities
    {
        #region Conversion Methods

        [System.Diagnostics.DebuggerStepThrough()]
        public static string ToString(object input)
        {
            return (input == null || input == DBNull.Value) ? string.Empty : input.ToString();
        }

        [System.Diagnostics.DebuggerStepThrough()]
        public static byte ToByte(object input)
        {
            return ToNByte(input) ?? 0;
        }

        [System.Diagnostics.DebuggerStepThrough()]
        public static byte? ToNByte(object input)
        {
            if (input == null || input == DBNull.Value || input.ToString().Length == 0) return null;
            return Convert.ToByte(input, CultureInfo.InvariantCulture);
        }

        [System.Diagnostics.DebuggerStepThrough()]
        public static int ToInt(object input)
        {
            return ToNInt(input) ?? 0;
        }

        [System.Diagnostics.DebuggerStepThrough()]
        public static int? ToNInt(object input)
        {
            if (input == null || input == DBNull.Value || input.ToString().Length == 0) return null;
            return Convert.ToInt32(input, CultureInfo.InvariantCulture);
        }

        [System.Diagnostics.DebuggerStepThrough()]
        public static long ToLong(object input)
        {
            return ToNInt(input) ?? 0;
        }

        [System.Diagnostics.DebuggerStepThrough()]
        public static long? ToNLong(object input)
        {
            if (input == null || input == DBNull.Value || input.ToString().Length == 0) return null;
            return Convert.ToInt64(input, CultureInfo.InvariantCulture);
        }

        [System.Diagnostics.DebuggerStepThrough()]
        public static DateTime ToDateTime(object input)
        {
            return ToNDateTime(input) ?? DateTime.MinValue;
        }

        [System.Diagnostics.DebuggerStepThrough()]
        public static DateTime? ToNDateTime(object input)
        {
            if (input == null || input == DBNull.Value || input.ToString().Length == 0) return null;
            return Convert.ToDateTime(input, CultureInfo.CurrentCulture);
        }

        [System.Diagnostics.DebuggerStepThrough()]
        public static bool ToBool(object input)
        {
            return ToNBool(input) ?? false;
        }

        [System.Diagnostics.DebuggerStepThrough()]
        public static bool? ToNBool(object input)
        {
            if (input == null || input == DBNull.Value || input.ToString().Length == 0) return null;
            return Convert.ToBoolean(input, CultureInfo.InvariantCulture);
        }

        [System.Diagnostics.DebuggerStepThrough()]
        public static byte[] ToByteArray(object input)
        {
            return (input == null || input == DBNull.Value) ? null : (byte[])input;
        }

        [System.Diagnostics.DebuggerStepThrough()]
        public static System.Guid ToGuid(object input)
        {
            return (input == null || input == DBNull.Value || input.ToString().Length == 0) ? System.Guid.Empty : (System.Guid)input;
        }

        [System.Diagnostics.DebuggerStepThrough()]
        public static System.Guid? ToNGuid(object input)
        {
            if (input == null || input == DBNull.Value || input.ToString().Length == 0) return null;
            return (System.Guid)input;
        }

        [System.Diagnostics.DebuggerStepThrough()]
        public static double ToDouble(object input)
        {
            return ToNDouble(input) ?? 0.0;
        }

        [System.Diagnostics.DebuggerStepThrough()]
        public static double? ToNDouble(object input)
        {
            if (input == null || input == DBNull.Value || input.ToString().Length == 0) return null;
            return Convert.ToDouble(input, System.Globalization.CultureInfo.InvariantCulture);
        }

        [System.Diagnostics.DebuggerStepThrough()]
        public static decimal ToDecimal(object input)
        {
            return ToNDecimal(input) ?? (decimal)0.0;
        }

        [System.Diagnostics.DebuggerStepThrough()]
        public static decimal? ToNDecimal(object input)
        {
            if (input == null || input == DBNull.Value || input.ToString().Length == 0) return null;
            return Convert.ToDecimal(input, System.Globalization.CultureInfo.InvariantCulture);
        }

        [System.Diagnostics.DebuggerStepThrough()]
        public static string ToMoney(decimal input)
        {
            return input.ToString("0.0000");
        }

        public static string ToCommaSeparatedStringList(IEnumerable<string> strings)
        {
            return ToSeparatedStringList(",", strings);
        }

        private static string ToSeparatedStringList(string separator, IEnumerable<string> strings)
        {
            string result = (strings == null ? null : String.Join(separator, strings.ToArray()));
            return String.IsNullOrEmpty(result) ? null : result;
        }

        public static string StringOrNull(string s)
        {
            return String.IsNullOrEmpty(s) ? null : s;
        }

		[System.Diagnostics.DebuggerStepThrough()]
		public static int? ToNIntParam(object input)
		{
			if (input == null || input == DBNull.Value || input.ToString().Length == 0 ||
				Convert.ToInt32(input, CultureInfo.InvariantCulture) <= 0)
			{
				return null;
			}
			return Convert.ToInt32(input, CultureInfo.InvariantCulture);
		}

        #endregion

        private static object ParseObject<T>(T value)
        {
            // Check for string first...
            Type inputType = typeof(T);

            if (inputType == typeof(string))
                return (value as string) ?? string.Empty;

            if (inputType == typeof(bool?))
            {                
                if (value == null) return System.DBNull.Value;
                bool bRet = bool.Parse(value.ToString());
                return (bool)(bRet) ? 1 : 0;
            }

            if (inputType == typeof(byte[]))
            {
                if (value == null || (value as byte[]).Length == 0) return System.DBNull.Value;
                return value;
            }

            if (inputType == typeof(System.Guid))
            {
                if (value == null || value.Equals(System.Guid.Empty)) return System.DBNull.Value;
                return value;
            }

            // All other types that don't have the above exception cases...
            return (object)value ?? System.DBNull.Value;
        }

        public static DateTime GetWeekStartingDate(DateTime date)
        {
            DateTime weekStarting = new DateTime();

            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    weekStarting = date;
                    break;
                case DayOfWeek.Tuesday:
                    weekStarting = date.AddDays(-1);
                    break;

                case DayOfWeek.Wednesday:
                    weekStarting = date.AddDays(-2);
                    break;

                case DayOfWeek.Thursday:
                    weekStarting = date.AddDays(-3);
                    break;

                case DayOfWeek.Friday:
                    weekStarting = date.AddDays(-4);
                    break;

                case DayOfWeek.Saturday:
                    weekStarting = date.AddDays(-5);
                    break;

                case DayOfWeek.Sunday:
                    weekStarting = date.AddDays(-6);
                    break;

            }

            return weekStarting;
        }

        public static DateTime GetWeekEndingDate(DateTime date)
        {
            DateTime weekEnding = new DateTime();

            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    weekEnding = date.AddDays(6);
                    break;
                case DayOfWeek.Tuesday:
                    weekEnding = date.AddDays(5);
                    break;

                case DayOfWeek.Wednesday:
                    weekEnding = date.AddDays(4);
                    break;

                case DayOfWeek.Thursday:
                    weekEnding = date.AddDays(3);
                    break;

                case DayOfWeek.Friday:
                    weekEnding = date.AddDays(2);
                    break;

                case DayOfWeek.Saturday:
                    weekEnding = date.AddDays(1);
                    break;

                case DayOfWeek.Sunday:
                    weekEnding = date;
                    break;

            }

            return weekEnding;
        }

        public static int RoundTo(int value, int roundTo)
        {
            double d = Math.Round((double)value / roundTo, 0);
            return (int)(d * roundTo);
            //int result = value;

            //if ((value % roundTo) != 0)
            //{
            //    result = ((value + 5) / 10) * 10;
            //}

            // Round Up
            //int result = value + roundTo - 1;
            //return (result - (result % roundTo));
        }

        //public bool RangeInsersectNullEnd(DateTime dateStart1, DateTime? dateEnd1, DateTime dateStart2, DateTime? dateEnd2)
        //{
        //    bool result = false;

        //    if (!dateEnd1.HasValue && !dateEnd2.HasValue)
        //    {
        //        result = true;
        //    }
        //    else if (dateEnd1.HasValue && dateEnd2.HasValue)
        //    {
        //        result = ((dateStart2 <= dateEnd1) && (dateEnd2 >= dateStart1));
        //    }
        //    else if (dateEnd1.HasValue)
        //    {
        //        result = (dateStart2 <= dateEnd1);
        //    }
        //    else if (dateEnd2.HasValue)
        //    {
        //        result = (dateEnd2 >= dateStart1);
        //    }

        //    return result;

        //    //ALTER FUNCTION [dbo].[ufnRangeIntersectNullEnd]
        //    //(
        //    //    @DateStart1 smalldatetime
        //    //    , @DateEnd1 smalldatetime
        //    //    , @DateStart2 smalldatetime
        //    //    , @DateEnd2 smalldatetime
        //    //)
        //    //RETURNS BIT
        //    //AS BEGIN
        //    //Declare @result bit
        //    //SELECT @result =
        //    //    CASE WHEN @DateEnd1 IS NULL AND @DateEnd2 IS NULL THEN 1
        //    //    ELSE
        //    //        CASE WHEN @DateEnd1 IS NOT NULL AND @DateEnd2 IS NOT NULL THEN
        //    //            CASE WHEN @DateStart2 <= @DateEnd1 AND @DateEnd2 >= @DateStart1 THEN 1
        //    //            ELSE 0
        //    //            END
        //    //        ELSE
        //    //            CASE WHEN @DateEnd1 IS NOT NULL THEN
        //    //                CASE WHEN @DateStart2 <= @DateEnd1 THEN 1
        //    //                ELSE 0
        //    //                END
        //    //            ELSE 
        //    //                CASE WHEN @DateEnd2 >= @DateStart1 THEN 1
        //    //                ELSE 0 
        //    //                END
        //    //            END
        //    //        END
        //    //    END
        //    //RETURN @result
        //    //END
        //}

        public enum RangeEdit
        {
            _init = 0,
            Delete = 1,
            MoveStartAfter = 2,
            MoveEndBefore = 3,
            MoveEndBeforePlusAddAfter = 4
        }

        public static RangeEdit RangeIntersectWithNullableEnds(DateTime dateStart1, DateTime? dateEnd1, DateTime dateStart2, DateTime? dateEnd2)
        {
            RangeEdit result = RangeEdit._init;

            //bool dateEnd1NullOrGreater = dateEnd2.HasValue && (!dateEnd1.HasValue || (dateEnd1.HasValue && dateEnd1 >= dateEnd2));
            //bool dateEnd2NullOrGreater = dateEnd1.HasValue && (!dateEnd2.HasValue || (dateEnd2.HasValue && dateEnd2 >= dateEnd1));
            //bool dateStart1Greater = (dateStart1 >= dateStart2);

            //if (dateStart1Greater && dateEnd1NullOrGreater)
            //{
            //    result = RangeEdit.MoveStartAfter;
            //}
            //else if (dateStart1Greater && dateEnd2NullOrGreater)
            //{
            //    result = RangeEdit.Delete;
            //}
            //else if (!dateStart1Greater && dateEnd1NullOrGreater)
            //{
            //    result = RangeEdit.MoveEndBeforePlusAddAfter;
            //}
            //else if (!dateStart1Greater && dateEnd2NullOrGreater)
            //{
            //    result = RangeEdit.MoveEndBefore;
            //}
            if ((dateStart1 >= dateStart2) && (!dateEnd2.HasValue || (dateEnd1.HasValue && dateEnd2.HasValue && dateEnd1 <= dateEnd2)))
            {
                result = RangeEdit.Delete;
            }
            else if ((dateStart1 >= dateStart2) && dateEnd2.HasValue && (!dateEnd1.HasValue || (dateEnd1.HasValue && dateEnd1 >= dateEnd2)))
            {
                result = RangeEdit.MoveStartAfter;
            }
            else if ((dateStart1 <= dateStart2) && (!dateEnd2.HasValue || (dateEnd2.HasValue && dateEnd1.HasValue && dateEnd1 >= dateEnd2)))
            {
                result = RangeEdit.MoveEndBefore;
            }
            else if ((dateStart1 <= dateStart2) && dateEnd2.HasValue && (!dateEnd1.HasValue || (dateEnd1.HasValue && dateEnd1 <= dateEnd2)))
            {
                result = RangeEdit.MoveEndBeforePlusAddAfter;
            }

            return result;
        }

        public static int GetWeekNumber(DateTime date)
        {
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            int weekNum = cultureInfo.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }

        public static bool IsFullMonths(DateTime startDate, DateTime endDate)
        {
            return (startDate.Day == 1 && endDate.AddDays(1).Day == 1);
        }

        /// <summary>
        /// Return the nearest time which has been round up.
        /// Ex: 9:03 -> 9;9:20 -> 9:30
        /// </summary>
        /// <param name="meetingTime"></param>
        /// <returns></returns>
        public static DateTime GetRoundUpMeetingTime(DateTime meetingTime)
        {
            var minute = meetingTime.Minute;
            var second = meetingTime.Second;
            var miliSecond = meetingTime.Millisecond;
            DateTime result;

            if (minute > 0 && minute < 15)
            {
                result = meetingTime.Subtract(new TimeSpan(0, 0, minute, second, miliSecond));
            }
            else if (minute >= 15 && minute < 30)
            {
                result = meetingTime.Add(new TimeSpan(0, 0, 30 - minute, 0, 0));
                result = result.Subtract(new TimeSpan(0, 0, 0, second, miliSecond));
            }
            else if (minute > 30 && minute < 45)
            {
                result = meetingTime.Subtract(new TimeSpan(0, 0, minute - 30, second, miliSecond));
            }
            else
            {
                result = meetingTime.Add(new TimeSpan(0, 0, 60 - minute, 0, 0));
                result = result.Subtract(new TimeSpan(0, 0, 0, second, miliSecond));
            }

            return result;
        }

		[System.Diagnostics.DebuggerStepThrough()]
		public static int? ToNIntDefaulted(object input)
		{
			if (input == null || input == DBNull.Value || input.ToString().Length == 0) return null;
			int? result = Convert.ToInt32(input, CultureInfo.InvariantCulture);
			if (result <= 0)
			{
				result = null;
			}

			return result;
		}

		public static decimal ToRoundNDecimal(object input, int roundNumber)
		{
			decimal result = ToNDecimal(input) ?? (decimal)0.0;
			result = Decimal.Round(result, roundNumber);
			return result;
		}

		public static double DegreeToRadian(double angle)
		{
			return angle * Math.PI / 180.0;
		}

		public static double RadianToDegree(double angle)
		{
			return angle * (180.0 / Math.PI);
		}

		internal static int MonthToQuarter(int month)
		{
			if (month >= 1 && month <= 3)
			{
				return 1;
			}
			else if (month >= 4 && month <= 6)
			{
				return 2;
			}
			else if (month >= 7 && month <= 9)
			{
				return 3;
			}
			else
				return 4;
		}

		internal static string PeriodValueToMonth(int p)
		{
			string result = "N/A";
			switch (p)
			{
				case 1:
					result = "Jan";
					break;
				case 2:
					result = "Feb";
					break;
				case 3:
					result = "Mar";
					break;
				case 4:
					result = "Apr";
					break;
				case 5:
					result = "May";
					break;
				case 6:
					result = "June";
					break;
				case 7:
					result = "Jul";
					break;
				case 8:
					result = "Aug";
					break;
				case 9:
					result = "Sep";
					break;
				case 10:
					result = "Oct";
					break;
				case 11:
					result = "Nov";
					break;
				case 12:
					result = "Dec";
					break;
			}
			return result;
		}

		internal static string PeriodValueToQuarter(int p)
		{
			string result = "N/A";
			switch (p)
			{
				case 1:
					result = "Mar";
					break;
				case 2:
					result = "June";
					break;
				case 3:
					result = "Sep";
					break;
				case 4:
					result = "Dec";
					break;
			}
			return result;
		}

		public static int MonthStringToPeriodValue(string month)
		{
			switch (month)
			{
				case "Jan":
					return 1;
				case "Feb":
					return 2;
				case "Mar":
					return 3;
				case "Apr":
					return 4;
				case "May":
					return 5;
				case "Jun":
					return 6;
				case "Jul":
					return 7;
				case "Aug":
					return 8;
				case "Sep":
					return 9;
				case "Oct":
					return 10;
				case "Nov":
					return 11;
				case "Dec":
					return 12;
				default:
					return 0;
			}
		}

		public static int QuarterStringToPeriodValue(string quarter)
		{
			switch (quarter)
			{
				case "Mar":
					return 1;
				case "Jun":
					return 2;
				case "Sep":
					return 3;
				case "Dec":
					return 4;
				default:
					return 0;
			}
		}

		public static string PeriodValueToString(int period, int year, PeriodType periodType)
		{
			string result = string.Empty;
			switch (periodType)
			{
				case PeriodType.Monthly:
					result = string.Format("{0} {1}", PeriodValueToMonth(period), year);
					break;
				case PeriodType.Quarterly:
					result = string.Format("{0} {1}", PeriodValueToQuarter(period), year);
					break;
				case PeriodType.Annually:
					result = string.Format("{0}", year);
					break;
			}
			return result;
		}

		public static List<String> PeriodByString(DateTime dateStart, DateTime dateEnd, PeriodType periodType)
		{
			List<String> result = new List<string>();

			DateTime currentDateTime = new DateTime(dateStart.Year, dateStart.Month, 1);
			int period, year;

			switch (periodType)
			{
				case PeriodType.Monthly:
					while (currentDateTime < dateEnd)
					{
						period = currentDateTime.Month;
						year = currentDateTime.Year;
						result.Add(PeriodValueToString(period, year, periodType));
						currentDateTime = currentDateTime.AddMonths(1);
					}
					break;
				case PeriodType.Quarterly:
					int startingQuarter = MonthToQuarter(currentDateTime.Month);
					if (startingQuarter == 1)
					{
						currentDateTime = new DateTime(dateStart.Year, 1, 1);
					}
					else if (startingQuarter == 2)
					{
						currentDateTime = new DateTime(dateStart.Year, 4, 1);
					}
					else if (startingQuarter == 3)
					{
						currentDateTime = new DateTime(dateStart.Year, 7, 1);
					}
					else
					{
						currentDateTime = new DateTime(dateStart.Year, 10, 1);
					}

					while (currentDateTime < dateEnd)
					{
						period = MonthToQuarter(currentDateTime.Month);
						year = currentDateTime.Year;
						result.Add(PeriodValueToString(period, year, periodType));
						currentDateTime = currentDateTime.AddMonths(3);
					}
					break;
				case PeriodType.Annually:
					currentDateTime = new DateTime(dateStart.Year, 1, 1);
					while (currentDateTime <= dateEnd)
					{
						year = currentDateTime.Year;
						period = year;
						result.Add(PeriodValueToString(period, year, periodType));
						currentDateTime = currentDateTime.AddYears(1);
					}
					break;
			}

			return result;
		}

		public static int[] GetValuesOfWeekDayEnum()
		{
			//NOTE: value of Weekday.Day enum
			return new int[8] { 0, 1, 2, 3, 4, 5, 6, 7 };
		}

		public static List<string> GetEnumNames(Type type)
		{
			List<string> result = new List<string>();
			Type enumType = type;
			FieldInfo[] enumDetail = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);
			foreach (FieldInfo item in enumDetail)
			{
				result.Add(item.Name);
			}

			return result;
		}

		public static int GetDayLastYearOffset(DateTime date)
		{
			DateTime dateLastYear = date.AddYears(-1);

			for (int i = 0; i < 7; i++)
			{
				if (date.DayOfWeek == dateLastYear.AddDays(i).DayOfWeek)
				{
					return i;
				}
			}

			return 0;
		}
	}
}
