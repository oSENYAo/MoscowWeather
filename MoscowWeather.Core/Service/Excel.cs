using MoscowWeather.Core.Managers;
using MoscowWeather.Data.Domain;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MoscowWeather.Core.Service
{
    public class Excel : Office
    {
        private readonly WeatherManager _weatherManager;

        public Excel(WeatherManager weatherManager)
        {
            _weatherManager = weatherManager;
        }
        public bool IsExcelFile(string path)
        {
            if (path.IndexOf(".xlsx") <= 0 || path.IndexOf(".xls") <= 0)
            {
                return false;
            }
            return true;
        }
        public async Task WriteToDbAsync(string filePath)
        {
            IWorkbook workbook = null;
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            if (filePath.IndexOf(".xlsx") > 0)
            {
                workbook = new XSSFWorkbook(fs);
            }
            else if (filePath.IndexOf(".xls") > 0)
            {
                workbook = new HSSFWorkbook(fs);
            }
            try
            {
                ISheet sheet = workbook.GetSheetAt(0);
                if (sheet != null)
                {
                    var date = new DateTime();
                    double time = 0.0;
                    int rowCount = sheet.LastRowNum;
                    for (int i = 4; i <= rowCount; i++)
                    {
                        IRow curRow = sheet.GetRow(i);
                        var date0 = Check(curRow.GetCell(0));
                        var time1 = Check(curRow.GetCell(1));
                        var airTemperature2 = Check(curRow.GetCell(2));
                        var Rel_humidity3 = Check(curRow.GetCell(3));
                        var DewPoint4 = Check(curRow.GetCell(4));
                        var Pressure5 = Check(curRow.GetCell(5));
                        var DirectionWind6 = Check(curRow.GetCell(6));
                        var SpeedWind7 = Check(curRow.GetCell(7));
                        var cloudy8 = Check(curRow.GetCell(8));
                        var CloudBase9 = Check(curRow.GetCell(9));
                        var HorizontalVisibility10 = Check(curRow.GetCell(10));
                        var WeatherConditions11 = Check(curRow.GetCell(11));
                        date = String.IsNullOrWhiteSpace(date0) ? DateTime.Today : Convert.ToDateTime(date0);
                        time = String.IsNullOrWhiteSpace(time1) ? 0.0 : Convert.ToDateTime(time1).TimeOfDay.TotalHours;
                        date = date.AddHours(time);
                        _weatherManager.AddWeatherAsync(new Weather
                        {
                            Date = date,
                            AirTemperature = String.IsNullOrWhiteSpace(airTemperature2) ? (float)0.0 : Convert.ToSingle(airTemperature2),
                            Rel_humidity = String.IsNullOrWhiteSpace(Rel_humidity3) ? (float)0 : Convert.ToSingle(Rel_humidity3),
                            DewPoint = String.IsNullOrWhiteSpace(DewPoint4) ? (float)0.0 : Convert.ToSingle(DewPoint4),
                            Pressure = String.IsNullOrWhiteSpace(Pressure5) ? (int)0 : int.Parse(Pressure5),
                            DirectionWind = DirectionWind6,
                            SpeedWind = SpeedWind7,
                            cloudy = String.IsNullOrWhiteSpace(cloudy8) ? (float)0 : Convert.ToSingle(cloudy8),
                            CloudBase = String.IsNullOrWhiteSpace(CloudBase9) ? (int)0 : int.Parse(CloudBase9),
                            HorizontalVisibility = String.IsNullOrWhiteSpace(HorizontalVisibility10) ? (int)0 : int.Parse(HorizontalVisibility10),
                            WeatherConditions = WeatherConditions11
                        });
                    }
                    _weatherManager.Save();
                }
            }
            catch (Exception)
            {
                throw new Exception("Wrong data in excel file");
            }
            finally
            {
                Remove(filePath);
            }
        }
        #region Util
        private string Check(NPOI.SS.UserModel.ICell cell)
        {
            if (cell != null)
            {
                switch (cell.CellType)
                {
                    case NPOI.SS.UserModel.CellType.Error:
                        break;
                    case NPOI.SS.UserModel.CellType.Unknown:
                        break;
                    case NPOI.SS.UserModel.CellType.Numeric:
                        return cell.NumericCellValue.ToString();
                    case NPOI.SS.UserModel.CellType.String:
                        return cell.StringCellValue;
                    case NPOI.SS.UserModel.CellType.Formula:
                        break;
                    case NPOI.SS.UserModel.CellType.Blank:
                        return null;
                    case NPOI.SS.UserModel.CellType.Boolean:
                        break;
                    default:
                        break;
                }
            }
            return null;
        }
        #endregion
    }
}
