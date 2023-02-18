// See https://aka.ms/new-console-template for more information
using System.ComponentModel;
using System.Data.Common;

String sqlInsert;
String filePath = "C:\\Users\\Jake\\Documents";
String filePathtoOpen = "C:\\Users\\Jake\\Documents\\TestCSv.csv";
String fileNametoSave;
String dbTableName;

//Console.WriteLine("File path of csv file?");
//filePathtoOpen = Console.ReadLine();

Console.WriteLine("Table Name?");
dbTableName = Console.ReadLine();

string firstLine = File.ReadLines(filePathtoOpen).First();
sqlInsert = "Insert into " + dbTableName + " (" + firstLine + "),\n";

foreach (string csvLine in File.ReadLines(filePathtoOpen).Skip(0))
{
    sqlInsert += "(" + csvLine + ")," + "\n";
}
sqlInsert = sqlInsert.Remove(sqlInsert.Length - 2) + ";";
sqlInsert = sqlInsert.Replace("\"", "");

Console.WriteLine(sqlInsert);
Console.WriteLine("What would you like to name the query?");
fileNametoSave = Console.ReadLine();
File.WriteAllText(filePath + "\\" + fileNametoSave, sqlInsert);

