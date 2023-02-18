// See https://aka.ms/new-console-template for more information
using System.ComponentModel;
using System.Data.Common;

String columnName;
String sqlInsert;
String filePath;
String filePathtoOpen;
String fileNametoSave;

Console.WriteLine("File path of csv file?");
filePathtoOpen = Console.ReadLine();

Console.WriteLine("Table Name?");
String dbTableName = Console.ReadLine();
List<String> dbTableColumns = new List<String>();
Console.WriteLine("How many columns?");
int dbColumnCount = Int32.Parse(Console.ReadLine());
if (dbColumnCount > 0)
{
    for (int i = 0; i < dbColumnCount; i++)
    {
        Console.WriteLine("Name of column " + (i + 1) + "?");
        columnName = Console.ReadLine();
        dbTableColumns.Add(columnName);
    }
}
sqlInsert = "Insert into " + dbTableName + " (";

foreach (string cn in dbTableColumns)
{
    sqlInsert += cn + ",";
}

sqlInsert += "),\n";


foreach (string csvLine in File.ReadLines(filePathtoOpen))
{
    sqlInsert += "(" + csvLine + ")," + "\n";
}
sqlInsert = sqlInsert.Remove(sqlInsert.Length - 2) + ";";
sqlInsert = sqlInsert.Replace("\"", ""); 

Console.WriteLine(sqlInsert);
Console.WriteLine("What would you like to name the query?");
fileNametoSave = Console.ReadLine();
Console.WriteLine("Where would you like to save the query?");
filePath = Console.ReadLine();
File.WriteAllText(filePath + "\\" + fileNametoSave, sqlInsert);

