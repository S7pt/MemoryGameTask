using System.Collections.Generic;
using UnityEngine;

namespace TestTask.Data
{
	public class CardsData
	{
		private List<Sprite> _faces;
		private Sprite _back;

		public List<Sprite> Faces => _faces;
		public Sprite Back => _back;

		public CardsData(IEnumerable<Sprite> faces, Sprite back)
		{
			_faces = new List<Sprite>(faces);
			_back = back;
		}
	}
}