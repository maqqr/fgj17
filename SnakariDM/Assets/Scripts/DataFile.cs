public class DataFile
{
    public string content;
    public string name;
    public string extension;

    public DataFile()
    {
    }

    public DataFile(string name, string content)
    {
        this.content = content;
        this.name = name;
    }

    public DataFile(string name, string content, string extension)
    {
        this.name = name;
        this.content = content;
        this.extension = extension;
    }

    public DataFile(string content)
    {
        this.content = content;
    }
}