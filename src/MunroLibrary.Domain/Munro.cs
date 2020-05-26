using System;

namespace MunroLibrary.Domain
{
    public class Munro
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal HeightMeters { get; private set; }
        public string GridRef { get; private set; }
        public MunroType MunroType { get; private set; }

        // Hide default constructor to promote encapsulation.
        private Munro() { }

        public Munro(
            int id,
            string name,
            decimal heightMeters,
            string gridRef,
            MunroType munroType)
        {
            // Let's go defensive
            if (id.Equals(0))
            {
                throw new ArgumentException("id must be greater than 0!");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (heightMeters.Equals(0)) 
            {
                throw new ArgumentException("heightMeters must be greater than 0!");
            }
            if (string.IsNullOrWhiteSpace(gridRef))
            {
                throw new ArgumentNullException(nameof(gridRef));
            }

            Id = id;
            Name = name;
            HeightMeters = heightMeters;
            GridRef = gridRef;
            MunroType = munroType;
        }
    }
}
