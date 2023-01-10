using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
	// Where directory we want to save
	private string dataDirPath = "";
	// The file name
	private string dataFileName = "";
	public FileDataHandler(string dataDirPath, string dataFileName)
	{
		this.dataDirPath = dataDirPath;
		this.dataFileName = dataFileName;
	}
	public GameData Load()
	{
		string fullPath = Path.Combine(dataDirPath, dataFileName);
		GameData loadedData = null;
		if (File.Exists(fullPath))
		{
			try
			{
				// Load the serialized data from the file
				string dataToLoad = "";
				using (FileStream stream = new FileStream(fullPath, FileMode.Open))
				{
					using (StreamReader reader = new StreamReader(stream))
					{
						dataToLoad = reader.ReadToEnd();
					}
				}
				// deserialize the data from Json back into C# object
				loadedData  = JsonUtility.FromJson<GameData>(dataToLoad);
			}
			catch (Exception e)
			{
				Debug.LogError("Error when trying to save data to file: " + fullPath + "\n" + e);
			}
		}
		return loadedData;
	}
	public void Save(GameData data)
	{
		// Path.Combine to account for different OS's having different path separators
		string fullPath = Path.Combine(dataDirPath, dataFileName);
		try
		{
			// we create the directory the file will be written to if it doesn't already exist
			Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

			// serialize the C# game data object into json
			string dataToStore = JsonUtility.ToJson(data, true);

			// write the serialized data to the file
			using (FileStream stream = new FileStream(fullPath, FileMode.Create))
			{
				using (StreamWriter writer = new StreamWriter(stream))
				{
					writer.Write(dataToStore);
				}
			}
		}
		catch (Exception e)
		{
			Debug.LogError("Error when trying to save data to file: " + fullPath + "\n" + e);
		}
	}
}
