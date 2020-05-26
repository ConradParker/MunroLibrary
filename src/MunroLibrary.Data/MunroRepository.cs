using MunroLibrary.Data.Extensions;
using MunroLibrary.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MunroLibrary.Data
{
    public class MunroRepository : IMunroRepository
    {
        // Hardcode file for initial period. 
        // Assume we will move to a db or other method later.
        private readonly string dataFile = $"{AppDomain.CurrentDomain.BaseDirectory}munrotab_v6.2.csv";

        public IEnumerable<Munro> GetData()
        {
            // Initialise return value
            var munros = new List<Munro>();

            // Get data and loop
            File.ReadLines(dataFile)
                .Skip(1)
                .ToList()
                .ForEach(row =>
                {
                    if(IsValidRow(row))
                    {
                        // Add valid items to our return list
                        munros.Add(new Munro(
                            GetByIndex<int>(row, DataIndex.Id),
                            GetByIndex<string>(row, DataIndex.Name),
                            GetByIndex<decimal>(row, DataIndex.HeightMeters),
                            GetByIndex<string>(row, DataIndex.GridRef),
                            GetByIndex<MunroType>(row, DataIndex.MunroType)));
                    }     
                });

            return munros;
        }

        /// <summary>
        /// Get columns from a row
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private static string[] GetRowColumns(string row)
        {
            return row.Split(',');
        }

        /// <summary>
        /// Get an item from the row by its index 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static T GetByIndex<T>(string row, int index)
        {
            var data = GetRowColumns(row);
            return data[index].ConvertFromString<T>();
        }

        /// <summary>
        /// Specify where we expect to find the data
        /// </summary>
        private static class DataIndex
        {
            public const int Id = 0;
            public const int Name = 5;
            public const int HeightMeters = 9;
            public const int GridRef = 13;
            public const int MunroType = 27;
        }

        /// <summary>
        /// Validate the Row Data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool IsValidRow(string row)
        {
            var data = row.Split(',');

            // Validate we have 30 columns
            if (!data.Length.Equals(30))
            {
                return false;
            }

            // Validate Munro Type
            if (!Enum.TryParse(data[DataIndex.MunroType], out MunroType _))
            {
                return false;
            }

            // Only return true if we pass all validations
            return true;
        }
    }
}
