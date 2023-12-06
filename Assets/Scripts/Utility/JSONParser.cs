using System;
using System.Collections;
using System.Collections.Generic;
using TestTask.Data;
using UnityEngine;
using UnityEngine.Networking;

namespace TestTask.Utility
{
	public class JSONParser : MonoBehaviour
	{
		private const string URL = "https://drive.google.com/uc?export=download&id=1roXB1nz3cVcJpPhsJuYsRACLsVRk-VaV";
		private CardsData _data;

		public CardsData Data => _data;

		public IEnumerator GetDataRoutine()
		{
			UnityWebRequest request = UnityWebRequest.Get(URL);
			yield return request.SendWebRequest();
			if (request.result == UnityWebRequest.Result.ConnectionError)
			{
				throw new Exception("ConnectionError");
			}
			ImageData data = JsonUtility.FromJson<ImageData>(request.downloadHandler.text);
			request.Dispose();
			yield return GetImagesRoutine(data);
		}

		private IEnumerator GetImagesRoutine(ImageData data)
		{
			List<Sprite> faces = new List<Sprite>(data.faces.Length);
			Sprite back;
			foreach (string url in data.faces)
			{
				UnityWebRequest faceRequest = UnityWebRequestTexture.GetTexture(url);

				yield return faceRequest.SendWebRequest();
				if (faceRequest.result == UnityWebRequest.Result.ConnectionError)
				{
					throw new Exception("Connection error!");
				}
				Texture2D faceTexture = ((DownloadHandlerTexture)faceRequest.downloadHandler).texture;
				Sprite face = Sprite.Create(faceTexture, new Rect(0, 0, faceTexture.width, faceTexture.height), new Vector2(0, 0));
				faces.Add(face);

			}

			UnityWebRequest backRequest = UnityWebRequestTexture.GetTexture(data.back);
			yield return backRequest.SendWebRequest();
			if (backRequest.result == UnityWebRequest.Result.ConnectionError)
			{
				throw new Exception("Connection error!");
			}
			Texture2D backTexture = ((DownloadHandlerTexture)backRequest.downloadHandler).texture;
			back = Sprite.Create(backTexture, new Rect(0, 0, backTexture.width, backTexture.height), new Vector2(0, 0));
			_data = new CardsData(faces, back);
		}
	}
}