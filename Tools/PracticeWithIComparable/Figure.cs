using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeWithIComparable
{
	public class Figure : IComparable<Figure>
	{
		private int length;
		private int square;
		public Figure() { }
		public Figure(int length)
		{
			Length = length;
			square = length * length;
		}

		public int Square
		{
			get => square;
		}

		public int Length
		{
			get => length;
			set => length = value;
		}
		public int CompareTo(Figure? other)
		{
			if (other is null)
			{
				throw new ArgumentNullException("Other in not Figure object");
			}
			else
				return this.Length.CompareTo(other.Length);
		}
	}
}
