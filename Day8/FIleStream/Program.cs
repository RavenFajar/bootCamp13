using System.Text;
// FileStream fs3 = File.Create("readwrite1.txt");
// string content = "Hello, this is a sample string to store in a text file.";
// // fs3.Close();
// using (StreamWriter writer = new StreamWriter(fs3))
// {
//     writer.WriteLine(content);
// }

// using(FileStream fs4 = File.OpenRead("readwrite1.txt"))
// {
//     using (StreamReader reader = new StreamReader(fs4))
//     {
//         string readContent = reader.ReadToEnd();
//         Console.WriteLine(readContent);
//     }
// }
// FileStream fs4 = File.OpenRead("readwrite.txt");
// try
// {
//     using FileStream fs = new FileStream("readwritee.txt", FileMode.Open);
// }
// catch (Exception ex)
// {
//     Console.WriteLine($"An error occurred: {ex.Message}");
// }
// try
// {
//     using FileStream fs2 = new FileStream("readwritee.txt", FileMode.CreateNew);
//     // Perform file operations here
// }
// catch (Exception ex)
// {
//     Console.WriteLine($"An error occurred: {ex.Message}");
// }
// try
// {
//     using FileStream fs = new FileStream("output.txt", FileMode.Create);
//     // Jika file ada, kontennya akan dihapus dan dimulai dari awal
//     byte[] data = Encoding.UTF8.GetBytes("Data baru");
//     fs.Write(data, 0, data.Length);
// }
// catch (Exception ex)
// {
//     Console.WriteLine($"An error occurred: {ex.Message}");
// }
// try
// {
//     using FileStream fs = new FileStream("output.txt", FileMode.Append);
//     // Jika file ada, data baru akan ditambahkan di akhir file
//     byte[] data = Encoding.UTF8.GetBytes(" Tambah");
//     fs.Write(data, 0, data.Length);
// }
// catch (Exception ex)
// {
//     Console.WriteLine($"An error occurred: {ex.Message}");
// }
// try
// {
//     using FileStream fs = new FileStream("output2.txt", FileMode.OpenOrCreate);
//     // Membaca data dari file
//     // byte[] data = new byte[fs.Length];
//     // fs.Read(data, 0, data.Length);
//     // string content = Encoding.UTF8.GetString(data);
//     // Console.WriteLine(content);

//     byte[] newData = Encoding.UTF8.GetBytes(" | +1|");
//     fs.Write(newData, 0, newData.Length);
// }
// catch (Exception ex)
// {
//     Console.WriteLine($"An error occurred: {ex.Message}");
// }
try
{
    using FileStream fs = new FileStream("TestBytes.txt", FileMode.Create);
    // Jika file ada, kontennya akan dihapus dan dimulai dari awal
    byte[] data = Encoding.UTF8.GetBytes("Data baru");
    int count = data.Length - 3;
    await fs.WriteAsync(data, 3, count);
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");  
}