using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace MoscowWeather.Core.Service
{
    public class Excel : Office
    {
        public override void Read(string filePath)
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

            ISheet sheet = workbook.GetSheetAt(0);
            if (sheet != null)
            {
                var listString = new List<List<string>>();
                int rowCount = sheet.LastRowNum;
                for (int i = 4; i <= rowCount; i++)
                {
                    IRow curRow = sheet.GetRow(i);
                    var cellValue0 = Check(curRow.GetCell(0));
                    var cellValue1 = Check(curRow.GetCell(1));
                    var cellValue2 = Check(curRow.GetCell(2));
                    var cellValue3 = Check(curRow.GetCell(3));
                    var cellValue4 = Check(curRow.GetCell(4));
                    var cellValue5 = Check(curRow.GetCell(5));
                    var cellValue6 = Check(curRow.GetCell(6));
                    var cellValue7 = Check(curRow.GetCell(7));
                    var cellValue8 = Check(curRow.GetCell(8));
                    var cellValue9 = Check(curRow.GetCell(9));
                    var cellValue10 = Check(curRow.GetCell(10));
                    var cellValue11 = Check(curRow.GetCell(11));
                    listString.Add(new List<string>
                    {
                        cellValue0,
                        cellValue1,
                        Convert.ToString(cellValue2),
                        Convert.ToString(cellValue3),
                        Convert.ToString(cellValue4),
                        Convert.ToString(cellValue5),
                        cellValue6,
                        Convert.ToString(cellValue7),
                        Convert.ToString(cellValue8),
                        Convert.ToString(cellValue9),
                        Convert.ToString(cellValue10),
                        cellValue11
                    });
                }

                return ;
            }
            return ;
        }
        public override void Remove(string filePath)
        {
            //    Add real. Remove file
        }
        #region
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
