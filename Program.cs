using System.Diagnostics;

Prog();




//Program itself
void Prog()
{
	Console.Write("Папка с файлами: ");
	string path = Console.ReadLine();

	if (Directory.Exists(path))
	{
		Console.WriteLine("Принято, запускаем файлы...");

		int num = 0;
		var files = Directory.GetFiles(path);

		foreach(var file in files) if (file.Contains(".exe")) num++;

		for (int i = 0; i < files.Length; i++)
		{
			if (files[i].Contains(".exe"))
			{
				string file = files[i];

				Thread thread = new Thread(new ThreadStart(() =>
				{
					string filename = file.Substring(path.Length, file.Length - path.Length);

					Process process = new Process();
					process.StartInfo = new ProcessStartInfo(file, "/verysilent");
					process.Start();

					Console.WriteLine($"Файл {filename} запущен");

					process.WaitForExit();

					Console.WriteLine($"Файл {filename} установлен");
				}));

				thread.Start();
			}
		}
	}
	else
	{
		Console.WriteLine("Папка не существует");
		Prog();
	}
}