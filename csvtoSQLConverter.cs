public class csvtoSQLConverter
{
    String sqlInsert = "Insert into ";
    static String filePath = "C:\\Users\\Jake\\Documents\\sqlOutput";
    static String filePathtoOpen = "C:\\Users\\Jake\\Documents\\test.csv";
    String dbTableName = "Table";
    Boolean validFileName = false;
    Boolean validFilePath = false;


    public static void Main()
    {
        csvtoSQLConverter converter = new csvtoSQLConverter();
        converter.convertCSVtoSQLFile(filePathtoOpen, filePath);
        
    }

    public void convertCSVtoSQLFile(String filePathtoOpen, String filePathtoSave)
    {
        do
        {
            try
            {
                if (filePathtoOpen == String.Empty || filePathtoOpen == null)
                {
                    Console.WriteLine("File path of csv file? (including name of file)");
                    filePathtoOpen = Console.ReadLine() ?? String.Empty;
                }

                if (filePathtoOpen == null)
                {
                    throw new Exception("File path is null");
                }

                string firstLine = File.ReadLines(filePathtoOpen).First();

                validFilePath = true;

                Console.WriteLine("Table Name?");
                dbTableName = Console.ReadLine() ?? String.Empty;

                sqlInsert = "Insert into " + dbTableName + " (" + firstLine + "),\n";

                foreach (string csvLine in File.ReadLines(filePathtoOpen).Skip(0))
                {
                    sqlInsert += "(" + csvLine + ")," + "\n";
                }
                sqlInsert = sqlInsert.Remove(sqlInsert.Length - 2) + ";";
                sqlInsert = sqlInsert.Replace("\"", "");

                Console.WriteLine(sqlInsert);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Please enter a valid file path.");
                filePathtoOpen = String.Empty;
            }

        } while (!validFilePath);

        do
        {
            try
            {
                if (filePathtoSave == String.Empty || filePath == null)
                {
                    Console.WriteLine("Where would you like to save the query? (file name inclusive)");

                    filePathtoSave = Console.ReadLine() ?? String.Empty;
                }

                if (filePathtoSave == null)
                {
                    throw new Exception("File path is null");
                }
                File.WriteAllText(filePathtoSave, sqlInsert);
                validFileName = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("You must give a valid file path for the query to be saved.");
                filePathtoSave = String.Empty;
            }

        } while (!validFileName);

        Console.WriteLine("File saved to " + filePathtoSave);
    }

}





